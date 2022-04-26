using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace BatPackager
{
    public static class Packager
    {
        public static bool Generate(string batchFile, string outputExe, string BitmapFile)
        {
            bool retValue = false;

            
            if (File.Exists(Path.GetFileName(outputExe)) == true)
            {
                File.Delete(Path.GetFileName(outputExe));
            }

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters comParms = new CompilerParameters();

            comParms.GenerateExecutable = true;
            comParms.GenerateInMemory = false;
            comParms.IncludeDebugInformation = false;
            comParms.MainClass = "GenericConsole.Program";
            comParms.CompilerOptions = "/optimize " + "/win32icon:" + BitmapFile;
            comParms.OutputAssembly = Path.GetFileName(outputExe);
            comParms.TreatWarningsAsErrors = false;

            comParms.ReferencedAssemblies.AddRange(new string[] { "mscorlib.dll", "System.dll", "System.Data.dll", "System.Xml.dll" });

            
            string source = CreateSourceCode(batchFile);
           
            CompilerResults comRes = compiler.CompileAssemblyFromSource(comParms, source);

            
            if (comRes.Errors != null && comRes.Errors.Count == 0)
            {
                

                if (File.Exists(outputExe) == true)
                {
                    File.Delete(outputExe);
                }

                File.Move(Path.GetFileName(outputExe), outputExe);
                retValue = true;
            }

            return retValue;
        }

        private static string CreateSourceCode(string batchFile)
        {
            StringBuilder sourceCode = new StringBuilder(GetGenericSource());
            string batchContent = File.ReadAllText(batchFile);
            batchContent = batchContent.Replace("\"", "$&$");

            sourceCode.Replace("batchFileContents = \"\";", "batchFileContents = " + "@" + "\"" + batchContent + "\";");

            return sourceCode.ToString();
        }

        private static string GetGenericSource()
        {
            return Properties.Resources.PackageApp;
        }
    }
}
