using System.Diagnostics;


using System.Threading.Tasks;

namespace Structure
{
    class ProcessCaller
    {
        internal string GitCall(string WorkDir, string args)
        {
            Process gitProcess = new Process();
            var processStartInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = WorkDir,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = $"/c git {args}"
            };
            gitProcess.StartInfo = processStartInfo;
            gitProcess.Start();
            string output = gitProcess.StandardOutput.ReadToEnd();
            gitProcess.WaitForExit();
            return output;
        }
    }
}
