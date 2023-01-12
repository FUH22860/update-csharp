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
        string targetPath = getHomePath() + "/backup/";
        
        string[] systemFilesToCopy = {"/etc/fstab", "/etc/makepkg.conf"};
        List<string> filesToCopy = new List<string>(systemFilesToCopy);

        string pacmanPackageListBeforeUpdate = getHomePath() + "/pacman-pre.txt";
        filesToCopy.Add(pacmanPackageListBeforeUpdate);
        string flatpakListBeforeUpdate = getHomePath() + "/flatpak-pre.txt";
        filesToCopy.Add(flatpakListBeforeUpdate);

        if (!Directory.Exists(targetPath)) {
            Directory.CreateDirectory(targetPath);
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
    public static string copyEverthingFromBackupLocationToFinalDestination() {
        string targetPath = "/artemis/test/";

        if (!Directory.Exists(targetPath)) {
            Console.ForegroundColor = ConsoleColor.Red;
            return $"Backup location does not exist! Please specify one in the config.";
        }

        if (targetPath is not null) {
            string sourcePath = getHomePath() + "/backup/";
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
}