using System.Diagnostics;

Process process = new Process();

ProcessStartInfo processStartInfo = new ProcessStartInfo();
processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
processStartInfo.FileName = @"ls";
processStartInfo.WorkingDirectory = @"/home/andre";
processStartInfo.Arguments = "--color";
processStartInfo.RedirectStandardOutput = false;
processStartInfo.RedirectStandardError = false;
processStartInfo.UseShellExecute = true;

process.StartInfo = processStartInfo;
process.Start();