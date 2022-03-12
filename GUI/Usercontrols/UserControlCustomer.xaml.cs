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
using GUI.Usercontrols;

namespace GUI.Usercontrols
{
    /// <summary>
    /// Interaction logic for UserControlCustomer.xaml
    /// </summary>
    public partial class UserControlCustomer : UserControl
    {
        ClassBIZ biz;
        Grid gridLeft;
        Grid gridRight;
        UserControlCustomerEdit UCCE;

        public UserControlCustomer(ClassBIZ inBIZ, Grid inGridLeft, Grid inGritRight)
        {
            InitializeComponent();
            biz = inBIZ;
            gridLeft = inGridLeft;
            gridRight = inGritRight;
            MainGrid.DataContext = biz;
        }

        private void buttonEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (biz.SelectedCustomer.id > 0)
            {
                biz.editOrnewCustomer = new ClassCustomer(biz.SelectedCustomer);
                UCCE = new UserControlCustomerEdit(biz, gridLeft, gridRight);
                gridLeft.Children.Add(UCCE); 
            }

        }
        private void buttonNewCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
