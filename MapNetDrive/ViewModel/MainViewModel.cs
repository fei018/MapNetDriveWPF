using MapNetDrive.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace MapNetDrive.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginUser LoginUser;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<MapInfo> GetMapInfos()
        {
            var list = new ObservableCollection<MapInfo>();

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MapNetDrive.txt");
            if (!File.Exists(file))
            {
                return list;
            }

            var allLines = File.ReadAllLines(file);

            if (allLines.Length <= 0) return list;

            foreach (var t in allLines)
            {
                if (string.IsNullOrWhiteSpace(t)) continue;

                var str = t.Split('|');
                if (str.Length < 2) continue;

                var dept = str[0].Trim();
                var netuse = str[1].Trim();

                if (string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(netuse)) continue;

                var map = new MapInfo
                {
                    Department = dept,
                    NetUseString = netuse
                };

                list.Add(map);
            }

            return list;
        }
        public ObservableCollection<MapInfo> MapInfos => GetMapInfos();

        /// <summary>
        /// Selected MapInfo
        /// </summary>
        private MapInfo _selectedMapInfo;
        public MapInfo SelectedMapInfo
        {
            get => _selectedMapInfo;
            set
            {
                if (_selectedMapInfo != value)
                {
                    _selectedMapInfo = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
