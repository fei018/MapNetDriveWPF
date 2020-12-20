using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapNetDrive.Model;

namespace MapNetDrive.ViewModel
{
    public class CMDHelper
    {
        /// <summary>
        /// Get net use cmd string
        /// </summary>
        private string GetNetUseCmdString(LoginUser user, MapInfo info)
        {
            var net = $@"net use {info.Drive} {info.SharePath}";
            return $"{net} /user:{user.UserName} {user.Password}";
        }

        private string RunCmd(string cmdString)
        {
            if (string.IsNullOrWhiteSpace(cmdString))
            {
                return null;
            }

            var start = new ProcessStartInfo();
            start.FileName = "cmd.exe";
            start.Arguments = cmdString;
            start.UseShellExecute = false;
            start.WorkingDirectory = @"c:\windows\system32";
            start.CreateNoWindow = true;
            start.RedirectStandardError = true;
            start.RedirectStandardInput = true;
            start.RedirectStandardOutput = true;

            using Process p = new Process();
            p.StartInfo = start;
            p.Start();

            p.StandardInput.WriteLine(cmdString);
            p.StandardInput.WriteLine("exit");

            var error = p.StandardError.ReadToEnd();
            //var output = p.StandardOutput.ReadToEnd(); // dont export because will show password

            p.WaitForExit(3000);

            return error;
        }

        /// <summary>
        /// Run Net Use cmd
        /// </summary>
        public string RunNetUseCmd(LoginUser user, MapInfo info)
        {
            var input = GetNetUseCmdString(user,info);
            var error = RunCmd(input);

            return error;
        }

        /// <summary>
        /// delete net drive letter
        /// </summary>
        public void RunNetUseDeleteCmd(MapInfo info)
        {
            var drive = info.Drive;
            if (drive == null) return;

            //Process.Start("cmd.exe", $" /c net use {drive} /d /y");
            var input = $"net use {drive} /d /y";
            RunCmd(input);
        }

        /// <summary>
        /// open net drive
        /// </summary>
        public void RunOpenDrive(MapInfo info)
        {
            var drive = info.Drive;
            if (drive == null) return;
            var input = $"explorer.exe {drive}";
            RunCmd(input);
        }
    }
}
