using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MapNetDrive.ViewModel;

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
           //ProgressBarConnecting = progressBar;
        }

        //public ProgressBar ProgressBarConnecting { get; private set; }

        private void MainBinding()
        {
            var mainViewModel = new MainViewModel(this);

            this.DataContext = mainViewModel;
            
            this.comboDept.ItemsSource = mainViewModel.MapInfos;
            this.comboDept.DisplayMemberPath = "Department";

            var selectMapInfoBinding = new Binding("SelectedMapInfo");
            this.comboDept.SetBinding(Selector.SelectedItemProperty, selectMapInfoBinding);

            this.btnConnect.AddHandler(Button.ClickEvent, new RoutedEventHandler(mainViewModel.btnConnect_Click));
            this.AddHandler(Keyboard.KeyDownEvent, new KeyEventHandler(mainViewModel.windowsKeyDown_Enter));
        }
    }
}
