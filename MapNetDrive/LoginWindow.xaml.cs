using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MapNetDrive.ViewModel;

namespace MapNetDrive
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.textUserName.Focus();
        }

        public bool ContinueToConnect = false;

        public LoginUser LoginUser { get; set; }

        private void SetLoginUser()
        {
            ContinueToConnect = false;

            if (string.IsNullOrWhiteSpace(this.textUserName.Text))
            {
                MessageBox.Show("User Name Null", "Error");
                return;
            }

            LoginUser = new LoginUser();
            LoginUser.UserName = this.textUserName.Text.Trim();
            LoginUser.Password = this.textPassword.Password.Trim();

            this.ContinueToConnect = true;
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SetLoginUser();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetLoginUser();
            }
        }
    }
}
