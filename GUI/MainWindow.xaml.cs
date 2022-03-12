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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BIZ;
using GUI.Usercontrols;
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassBIZ biz;
        UserControlCustomer UCCustomer;
        UserControlOrderMeat UCOrderMeat;

        public MainWindow()
        {
            InitializeComponent();
            biz = new ClassBIZ();
            MainGrid.DataContext = biz;

            UCCustomer = new UserControlCustomer(biz, LeftGrid, RightGrid);
            UCOrderMeat = new UserControlOrderMeat(biz, LeftGrid, RightGrid);

            LeftGrid.Children.Add(UCCustomer);
            RightGrid.Children.Add(UCOrderMeat);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            biz.apiRates = await biz.GetApiRates();     
        }
    }
}
