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

    public static void startUpdate() {
        Process process = new Process();

        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        //processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        processStartInfo.FileName = @"yay";
        processStartInfo.WorkingDirectory = getHomePath();
        //processStartInfo.Arguments = "--color";
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.RedirectStandardError = true;
        processStartInfo.UseShellExecute = false;

        process.StartInfo = processStartInfo;
        process.Start();

        // Read the output of the "yay" command
        string output = process.StandardOutput.ReadLine();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine(output);
        Console.WriteLine(error);

    }

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

    public static string copyEverthingToBackup() {
        string fileName = "fstab";
        string sourcePath = @"/etc/";
        string targetPath =  getHomePath();

        string backupPath = targetPath + "/backup/";

        string sourceFile = Path.Combine(sourcePath, fileName);
        string destFile = Path.Combine(backupPath, fileName);

        Directory.CreateDirectory(backupPath);

        File.Copy(sourceFile, destFile, true);

        return $"Copied {fileName} to {backupPath}";
    }
}