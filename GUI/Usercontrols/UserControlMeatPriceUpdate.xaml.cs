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
using Repository;

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlMeatPriceUpdate.xaml
    /// </summary>
    public partial class UserControlMeatPriceUpdate : UserControl
    {
        ClassBIZ biz;
        Grid gridLeft;
        Grid gridRight;
        public UserControlMeatPriceUpdate(ClassBIZ inBIZ, Grid inGridLeft, Grid inGridRight)
        {
            InitializeComponent();
            biz = inBIZ;
            gridLeft = inGridLeft;
            gridRight = inGridRight;
            MainGrid.DataContext = biz;
        }

        private void PrisGris_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrisKalv_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrisOkse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrisKylling_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrisKalkun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrisHest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonExitUpdate_Click(object sender, RoutedEventArgs e)
        {
            gridRight.Children.Remove(this);
            biz.isEnabledLeft = true;
        }
    }
}
