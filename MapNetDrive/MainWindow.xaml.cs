using MapNetDrive.ViewModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace MapNetDrive
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainBinding();
            this.comboDept.SelectedIndex = 0;
            this.comboDept.Focus();
        }

        private MainViewModel _mainVM;

        private void MainBinding()
        {
            _mainVM = new MainViewModel();

            this.comboDept.DataContext = _mainVM;

            this.comboDept.ItemsSource = _mainVM.MapInfos;
            this.comboDept.DisplayMemberPath = "Department";

            var selectMapInfoBinding = new Binding("SelectedMapInfo");
            this.comboDept.SetBinding(Selector.SelectedItemProperty, selectMapInfoBinding);

            //this.btnConnect.AddHandler(Button.ClickEvent, new RoutedEventHandler(mainViewModel.btnConnect_Click));
            //this.AddHandler(Keyboard.KeyDownEvent, new KeyEventHandler(mainViewModel.windowsKeyDown_Enter));
        }

        #region Execute CMD 
        private void ExecuteNetUseCMD()
        {
            var login = new LoginUser
            {
                UserName = this.txtUserName.Text.Trim(),
                Password = this.txtPassword.Password.Trim()
            };


            Task.Factory.StartNew(() =>
            {
                this.Dispatcher.BeginInvoke(new Action(() => ShowProgressBar()));

                var cmd = new CMDHelper();
                cmd.DeleteNetworkDrive(_mainVM.SelectedMapInfo);
                

                var result = cmd.RunNetUseCmd(login, _mainVM.SelectedMapInfo).Trim();

                if (!string.IsNullOrEmpty(result))
                {
                    this.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(result, "Error")));
                }
                else
                {
                    cmd.RunOpenDrive(_mainVM.SelectedMapInfo);
                    this.Dispatcher.BeginInvoke(new Action(() => this.Close()));
                }

                this.Dispatcher.BeginInvoke(new Action(() => CloseProgressBar()));
            });
        }
        #endregion

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            ExecuteNetUseCMD();
            e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnConnect_Click(sender, e);
            }
        }

        #region Show or Close ProgressBar
        private void ShowProgressBar()
        {
            if (progressBar != null)
            {
                progressBar.Visibility = Visibility.Visible;
                this.IsEnabled = false;
            }
        }

        private void CloseProgressBar()
        {
            if (progressBar != null)
            {
                progressBar.Visibility = Visibility.Hidden;
                this.IsEnabled = true;
                this.btnConnect.Focus();
            }
        }
        #endregion
    }
}
