namespace MapNetDrive.Model
{
    public class MapInfo
    {
        public string Department { get; set; }

        public string NetUseString { get; set; }

        /// <summary>
        /// return net use drive letter
        /// </summary>
        public string NetworkDriveLetter
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NetUseString)) return null;

                var index = NetUseString.IndexOf(':');
                if (index <= 0) return null;

                var drive = NetUseString.Substring(index - 1, 2);
                return drive;
            }
        }
    }
}
