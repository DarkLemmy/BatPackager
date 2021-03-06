using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace GenericConsole
{
    class Program
    {
        private static string batchFileContents = "exit";
        private static bool redirectStandardOutput = true;

        static void Main(string[] args)
        {
            batchFileContents = "";
            batchFileContents += "\r\n";

            batchFileContents = batchFileContents.Replace("$&$", "\"");

            File.WriteAllText(Path.GetTempPath() + "\\temp.bat", batchFileContents);

            Process prc = new Process();

            prc.StartInfo = new ProcessStartInfo(Path.Combine(Path.GetTempPath() + "\\temp.bat"));

            if (redirectStandardOutput == true)
            {
                prc.StartInfo.UseShellExecute = false;
            }
            else
            {
                prc.StartInfo.UseShellExecute = true;
            }

            prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            prc.StartInfo.RedirectStandardOutput = redirectStandardOutput;

            prc.Start();
            StreamReader sr = null;

            if (redirectStandardOutput == true)
            {
                sr = prc.StandardOutput;
            }
            else
            {
            }

            while (prc.HasExited == false)
            {
                if (sr != null)
                {
                    string cmdOutput = sr.ReadLine();

                    if (cmdOutput != null && cmdOutput.Length != 0)
                    {
                        Console.WriteLine(cmdOutput);
                    }
                }
            }

            File.Delete(Path.Combine(Path.GetTempPath() + "\\temp.bat"));
        }
    }
}
