using MapNetDrive.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MapNetDrive;
using System.Windows.Controls;
using System.Threading;

namespace MapNetDrive.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(MainWindow window)
        {
            _mainWindow = window;
            _progressBar = window.progressBar;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginUser LoginUser;

        private Window _mainWindow;

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
                if (str.Length < 3) continue;

                var str1 = str[0].Trim();
                var str2 = str[1].Trim();
                var str3 = str[2].Trim();

                if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3)) continue;

                var map = new MapInfo();
                map.Department = str1;
                map.Drive = str2;
                map.SharePath = str3;

                list.Add(map);
            }

            return list;
        }
        public ObservableCollection<MapInfo> MapInfos => GetMapInfos();

        /// <summary>
        /// Selected MapInfo
        /// </summary>
        private MapInfo _selectedMapInfo;
        public MapInfo SelectedMapInfo {
            get => _selectedMapInfo;
            set
            {
                if (value != _selectedMapInfo)
                {
                    _selectedMapInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        #region MainWindow Connect Event
        public void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            var element = e.Source as FrameworkElement;
            var main = element.DataContext as MainViewModel;
            if (main == null) return;
            if (main.SelectedMapInfo == null) return;

            if (OpenLoginWindow())
            {
                this._mainWindow.Dispatcher.BeginInvoke((Action)delegate () { ExecuteNetUseCMD(); });               
            }
        }

        public void windowsKeyDown_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnConnect_Click(sender, e);
            }
        }
        #endregion

        #region LoginWindow and Set LoginUser value
        private bool OpenLoginWindow()
        {
            var loginWin = new LoginWindow();
            loginWin.Owner = _mainWindow;
            loginWin.ShowDialog();
            if (loginWin.ContinueToConnect)
            {
                this.LoginUser = loginWin.LoginUser;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Execute CMD 
        private void ExecuteNetUseCMD()
        {
            var cmd = new CMDHelper();
            Task.Factory.StartNew(() =>
            {
                this._mainWindow.Dispatcher.BeginInvoke((Action)delegate () { ShowProgressBar(); });

                cmd.RunNetUseDeleteCmd(_selectedMapInfo);
                Thread.Sleep(2000);

                var result = cmd.RunNetUseCmd(LoginUser, _selectedMapInfo).Trim();

                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show(result);
                }
                else
                {
                    cmd.RunOpenDrive(_selectedMapInfo);
                }

                this._mainWindow.Dispatcher.BeginInvoke((Action)delegate () { CloseProgressBar(); });
            });
        }
        #endregion

        #region Show or Close ProgressBar
        private ProgressBar _progressBar;
        private void ShowProgressBar()
        {
            if (_progressBar != null)
            {
                _progressBar.Visibility = Visibility.Visible;
            }            
        }

        private void CloseProgressBar()
        {
            if (_progressBar != null)
            {
                _progressBar.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}
