using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNetDrive.Model
{
    public class MapInfo
    {
        public string Department { get; set; }

        private string _drive;

        public string Drive
        {
            get { return _drive; }
            set
            {
                if (!value.Trim().EndsWith(":"))
                {
                    throw new Exception("Drive name error");
                }
                _drive = value;
            }
        }


        public string SharePath { get; set; }

        public string DetailHeard => "Department:\r\nDrive:\r\nSharePath:";

        public string DetailContent => $"{Department}\r\n{Drive}\r\n{SharePath}";
    }
}
