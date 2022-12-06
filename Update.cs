using System.Diagnostics;
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
        processStartInfo.RedirectStandardOutput = false;
        processStartInfo.RedirectStandardError = false;
        processStartInfo.UseShellExecute = true;

        process.StartInfo = processStartInfo;
        process.Start();

        process.StandardInput.WriteLine(" ");

        //shellStream.WriteLine("passwd fadwa");
        //shellStream.Expect("Enter new password:");
        //shellStream.WriteLine("fadwa");
        //shellStream.Expect("Retype new password:");
        //shellStream.WriteLine("fadwa");

    }

    public static string copyEverthingToBackup() {
        string fileName = "fstab";
        string sourcePath = @"/etc/";
        string targetPath =  getHomePath();

        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
        string destFile = System.IO.Path.Combine(targetPath, fileName);

        System.IO.Directory.CreateDirectory(targetPath);

        System.IO.File.Copy(sourceFile, destFile, true);

        return $"Copied {fileName} to {targetPath}";
    }
}