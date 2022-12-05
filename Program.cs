using System.Diagnostics;

    string homePath;
    if(Environment.OSVersion.Platform == PlatformID.Unix) {
        homePath = Environment.GetEnvironmentVariable("HOME");
    } else {
        Console.WriteLine("This script doesn't support your operating system.");
    }

    Process process = new Process();

    ProcessStartInfo processStartInfo = new ProcessStartInfo();
    //processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
    processStartInfo.FileName = @"yay";
    //processStartInfo.WorkingDirectory = homePath;
    //processStartInfo.Arguments = "--color";
    processStartInfo.RedirectStandardOutput = false;
    processStartInfo.RedirectStandardError = false;
    processStartInfo.UseShellExecute = true;

    process.StartInfo = processStartInfo;
    process.Start();