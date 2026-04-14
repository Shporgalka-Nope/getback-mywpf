using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getback_mywpf.data.Helpers
{
    public class Helper
    {
        /// <summary>
        /// Internal directory of the CLI.
        /// </summary>
        protected static readonly string BASE_DIR = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Directory in which the helper will execute commands.
        /// </summary>
        protected string _workingDir;

        /// <summary>
        /// Output of the last executed command. Equals RedirectStandardError = true.
        /// </summary>
        protected string ErrOutput { get; set; } = null;
        /// <summary>
        /// Output of last executed command. Equals RedirectStandardOutput = true.
        /// </summary>
        protected string LogOutput { get; set; } = null;

        public Helper(string workingDir)
        {
            _workingDir = workingDir;
        }
        /// <summary>
        /// Executes a dotnet command with given arguments.
        /// </summary>
        /// <param name="arguments">The arguments to pass to the dotnet command.</param>
        /// <returns>True if the command executed successfully; otherwise, false.</returns>
        protected bool Execute(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                WorkingDirectory = _workingDir,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            int exitCode;
            using (Process process = Process.Start(startInfo))
            {
                ErrOutput = process.StandardError.ReadToEnd();
                LogOutput = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                exitCode = process.ExitCode;
            }

            return exitCode == 0 ? true : false;
        }

        /// <summary>
        /// Writes the contents of the error and standard log outputs to the console if they are not empty.
        /// </summary>
        /// <remarks>Outputs the error log first if available, followed by the standard log. Each log is
        /// prefixed with an informational label. This method does not throw exceptions if the log outputs are null or
        /// whitespace.</remarks>
        protected void PrintLogs()
        {
            if (!string.IsNullOrWhiteSpace(ErrOutput)) Console.WriteLine($"[INFO] Error log:\n{ErrOutput}");
            if (!string.IsNullOrWhiteSpace(LogOutput)) Console.WriteLine($"[INFO] Standard log:\n{LogOutput}");
        }
    }
}
