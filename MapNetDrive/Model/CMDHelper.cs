using System.Diagnostics;

namespace MapNetDrive.Model
{
    public class CMDHelper
    {
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

            return error;
        }

        /// <summary>
        /// Run Net Use cmd
        /// </summary>
        public string ExcuteNetUseCmd(LoginUser user, MapInfo info)
        {
            var input = $"{info.NetUseString} /user:{user.UserName} {user.Password}";
            var error = RunCmd(input);

            return error;
        }

        /// <summary>
        /// delete net drive letter
        /// </summary>
        public void DeleteNetworkDrive(MapInfo info)
        {
            if (info.NetworkDriveLetter == null) return;

            //Process.Start("cmd.exe", $" /c net use {drive} /d /y");
            var input = $"net use {info.NetworkDriveLetter} /d /y";
            RunCmd(input);
        }

        /// <summary>
        /// open net drive on explorer
        /// </summary>
        public void OpenDrive(MapInfo info)
        {
            if (info.NetworkDriveLetter == null) return;
            var input = $"explorer.exe {info.NetworkDriveLetter}";
            RunCmd(input);
        }
    }
}
