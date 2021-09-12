namespace MapNetDrive.Model
{
    public class MapInfo
    {
        public string Department { get; set; }

        private string _netUserString;
        public string NetUseString 
        {
            get => _netUserString;

            set
            {
                _netUserString = "net use " + value;
            }
        }

        /// <summary>
        /// net use drive letter
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
