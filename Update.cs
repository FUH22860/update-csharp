using System.Diagnostics;
public class Update {
    public string homePath { get; set; } = string.Empty;
    public void getHomePath() {
        if(Environment.OSVersion.Platform == PlatformID.Unix) {
            homePath = Environment.GetEnvironmentVariable("HOME");
        } else {
            Console.WriteLine("This script doesn't support your operating system.");
        }
    }

    public void startUpdate() {
        Process process = new Process();

        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        //processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        processStartInfo.FileName = @"yay";
        processStartInfo.WorkingDirectory = homePath;
        //processStartInfo.Arguments = "--color";
        processStartInfo.RedirectStandardOutput = false;
        processStartInfo.RedirectStandardError = false;
        processStartInfo.UseShellExecute = true;

        process.StartInfo = processStartInfo;
        process.Start();
    }
}