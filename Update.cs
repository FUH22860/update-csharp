using System.Diagnostics;
using System.IO;
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

    public static string copyEverthingFromBackupLocationToFinalDestination() {
        string targetPath = "/artemis/test/";
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
        return $"Copied everything successfully to {targetPath}";
    }

// Commented code below was written by ChatGPT

// using System;
// using System.Diagnostics;

// namespace UpdateLinux
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Updating Linux system...");

//             // Use the Process class to run the "apt-get update" command
//             var process = new Process()
//             {
//                 StartInfo = new ProcessStartInfo
//                 {
//                     FileName = "yay",
//                     UseShellExecute = false,
//                     RedirectStandardOutput = true,
//                     RedirectStandardError = true
//                 }
//             };
//             process.Start();

//             // Read the output of the "apt-get update" command
//             string output = process.StandardOutput.ReadToEnd();
//             string error = process.StandardError.ReadToEnd();
//             process.WaitForExit();

//             Console.WriteLine(output);
//             Console.WriteLine(error);

//             Console.WriteLine("Linux system updated!");
//         }
//     }
// }
}