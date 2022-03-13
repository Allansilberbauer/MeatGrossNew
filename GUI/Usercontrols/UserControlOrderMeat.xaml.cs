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
namespace GUI.Usercontrols
{
    /// <summary>
    /// Interaction logic for UserControlOrderMeat.xaml
    /// </summary>
    public partial class UserControlOrderMeat : UserControl
    {
        ClassBIZ biz;
        Grid gridLeft;
        Grid gridRight;
        UserControlMeatPriceUpdate UCMPU;
        public UserControlOrderMeat(ClassBIZ inBIZ, Grid inGridLeft, Grid inGritRight)
        {
            InitializeComponent();

            biz = inBIZ;
            gridLeft = inGridLeft;
            gridRight = inGritRight;
            MainGrid.DataContext = biz;

            
        }

        private void buttonSellToCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
     
        private void UpdateMeatPriceAndStock_Click(object sender, RoutedEventArgs e)
        {
            UCMPU = new UserControlMeatPriceUpdate(biz, gridLeft, gridRight);
            gridRight.Children.Add(UCMPU);
            biz.isEnabledLeft = false;
        }
        private void SaveCustomerData()
        {

        }
    }
}
