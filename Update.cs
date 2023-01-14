using System.IO.Compression;

public class Update {
    public static string getHomePath() {
        string homePath = string.Empty;

        if(Environment.OSVersion.Platform == PlatformID.Unix) {
            homePath = Environment.GetEnvironmentVariable("HOME");
            return homePath;
        } else {
            throw new ApplicationException("This script doesn't support your operating system.");
        }
    }
    public static string copyEverthingBeforeUpdateToBackupLocation() {
        string targetPath = getHomePath() + "/backup/uncompressed/";

        if (File.Exists(targetPath + "/pacman-after.txt") && File.Exists(targetPath + "/flatpak-after.txt")) {
            File.Delete(targetPath + "/pacman-after.txt");
            File.Delete(targetPath + "/flatpak-after.txt");
        }
        
        string[] systemFilesToCopy = {"/etc/fstab", "/etc/makepkg.conf"};
        List<string> filesToCopy = new List<string>(systemFilesToCopy);

        string pacmanPackageListBeforeUpdate = getHomePath() + "/pacman-pre.txt";
        filesToCopy.Add(pacmanPackageListBeforeUpdate);
        string flatpakListBeforeUpdate = getHomePath() + "/flatpak-pre.txt";
        filesToCopy.Add(flatpakListBeforeUpdate);

        if (!Directory.Exists(targetPath)) {
            Directory.CreateDirectory(targetPath);
            copyEverthingBeforeUpdateToBackupLocation();
        } else {
            foreach (string file in filesToCopy) {
                FileInfo info = new FileInfo(file);
                string destFile = Path.Combine(targetPath, info.Name);
                File.Copy(info.FullName, destFile, true);
            }
        }

        string copiedFiles = string.Join(", ", filesToCopy);

        Console.ForegroundColor = ConsoleColor.Green;
        return $"Copied {copiedFiles} to {targetPath}";
    }

    public static string copyEverthingAfterUpdateToBackupLocation() {
        string targetPath = getHomePath() + "/backup/uncompressed/"; // Use /tmp to zip and then move into /backup/compressed/

        if (File.Exists(targetPath + "/pacman-pre.txt") && File.Exists(targetPath + "/flatpak-pre.txt")) {
            File.Delete(targetPath + "/pacman-pre.txt");
            File.Delete(targetPath + "/flatpak-pre.txt");
            File.Delete(targetPath + "/fstab");
            File.Delete(targetPath + "/makepkg.conf");
        }

        List<string> filesToCopy = new List<string>();
        string pacmanPackageListBeforeUpdate = getHomePath() + "/pacman-after.txt";
        filesToCopy.Add(pacmanPackageListBeforeUpdate);
        string flatpakListBeforeUpdate = getHomePath() + "/flatpak-after.txt";
        filesToCopy.Add(flatpakListBeforeUpdate);

        if (!Directory.Exists(targetPath)) {
            Directory.CreateDirectory(targetPath);
            copyEverthingAfterUpdateToBackupLocation();
        } else {
            foreach (string file in filesToCopy) {
                FileInfo info = new FileInfo(file);
                string destFile = Path.Combine(targetPath, info.Name);
                File.Copy(info.FullName, destFile, true);
            }
        }

        string copiedFiles = string.Join(", ", filesToCopy);

        Console.ForegroundColor = ConsoleColor.Green;
        return $"Copied {copiedFiles} to {targetPath}";
    }

    /// <summary>
    /// Method <c>copyEverthingFromBackupLocationToFinalDestination</c> copies everything to second Backup Location which should be a external drive or a network share. Offsite/Second Backup.
    /// </summary>
    public static string copyEverthingFromBackupLocationToFinalDestination(string finalBackupLocation) {
        string targetPath = finalBackupLocation;

        if (!Directory.Exists(targetPath)) {
            Console.ForegroundColor = ConsoleColor.Red;
            return $"Backup location does not exist! Please specify one in the config.";
        }

        if (targetPath is not null) {
            string sourcePath = getHomePath() + "/backup/compressed/";
            string[] intermediateBackupLocation = Directory.GetFiles(sourcePath);        

            if (!Directory.Exists(targetPath)) {
                throw new DirectoryNotFoundException("Target directory not found");
            } else {
                foreach (string file in intermediateBackupLocation) {
                    FileInfo info = new FileInfo(file);
                    string destFile = Path.Combine(targetPath, info.Name);
                    File.Copy(info.FullName, destFile, true);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            return $"Copied everything successfully to {targetPath}";
        } else {
            Console.ForegroundColor = ConsoleColor.Red;
            return "You have not configured a backup location!";
        }
    }

    public static bool zipAllContentInBackupLocation(string finalZipName) {
        string targetPath = getHomePath() + "/backup/compressed/";
        if (!Directory.Exists(targetPath)) {
            Directory.CreateDirectory(targetPath);
        }
        
        string sourcePath = getHomePath() + "/backup/uncompressed/"; // Moved to /tmp
        string targetZip = getHomePath() + "/backup/compressed/" + finalZipName;

        if (!Directory.Exists("/tmp/backup/")) {
            Directory.CreateDirectory("/tmp/backup/");
        }
        string newFinalZip = "/tmp/backup/" + finalZipName;
        File.Delete(newFinalZip); // Delete residual zip's in tmp
        ZipFile.CreateFromDirectory(sourcePath, newFinalZip);

        if (File.Exists(targetZip)) {
            // ToDo verify if current zip matched old zip. If yes don't create the zip and leave the old one. If it doesn't match delete the old one.
            if (!checkForIdenticalFile(targetZip, newFinalZip)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{finalZipName} is outdated");
                File.Delete(targetZip);
                if (File.Exists(newFinalZip)) {
                    File.Delete(newFinalZip);
                    zipAllContentInBackupLocation(finalZipName);
                } else {
                    File.Move(newFinalZip, targetZip);
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{finalZipName} is up to date");
            }
        } else {
            ZipFile.CreateFromDirectory(sourcePath, targetZip);
        }
        
        if (File.Exists(targetZip)) {
            return true;
        } else {
            return false;
        }
    }

    public static bool zipPacmanDatabase() {
        string pacmanDatabaseLocation = "/var/lib/pacman/local/";
        string pacmanDatabaseZip = getHomePath() + "/backup/compressed/pacman-database.zip";
        
        if (!Directory.Exists("/tmp/backup/")) {
            Directory.CreateDirectory("/tmp/backup/");
        }
        string newPacmanDatabaseZip = "/tmp/backup/pacman-database.zip";
        File.Delete(newPacmanDatabaseZip); // Delete residual Pacman Database in tmp
        ZipFile.CreateFromDirectory(pacmanDatabaseLocation, newPacmanDatabaseZip);

        if (File.Exists(pacmanDatabaseZip)) {
            if (!checkForIdenticalFile(pacmanDatabaseZip, newPacmanDatabaseZip)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pacman Database is outdated");
                File.Delete(pacmanDatabaseZip);
                if (File.Exists(newPacmanDatabaseZip)) {
                    File.Delete(newPacmanDatabaseZip);
                    zipPacmanDatabase();
                } else {
                    File.Move(newPacmanDatabaseZip, pacmanDatabaseZip);
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Pacman Database is up to date");
            }
        } else {
            ZipFile.CreateFromDirectory(pacmanDatabaseLocation, pacmanDatabaseZip);
        }

        if (File.Exists(pacmanDatabaseZip)) {
            return true;
        } else {
            return false;
        }
    }

    public static bool checkForIdenticalFile(string existingFilePath, string newFilePath) {
        byte[] existingFile = File.ReadAllBytes(existingFilePath);
        byte[] newFile = File.ReadAllBytes(newFilePath);

        if (existingFile.Length == newFile.Length) {
            for (int i=0; i < existingFile.Length; i++) {
                if (existingFile[i] != newFile[i]) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}