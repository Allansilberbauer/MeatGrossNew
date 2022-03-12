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
    /// Interaction logic for UserControlCustomerEdit.xaml
    /// </summary>
    public partial class UserControlCustomerEdit : UserControl
    {
        ClassBIZ biz;
        Grid gridLeft;
        Grid gridRight;
        public UserControlCustomerEdit(ClassBIZ inBIZ, Grid inGridLeft, Grid inGridRight)
        {
            InitializeComponent();

            biz = inBIZ;
            gridLeft = inGridLeft;
            gridRight = inGridRight;
            MainGrid.DataContext = biz;
        }

        private void buttonRegret_Click(object sender, RoutedEventArgs e)
        {
            biz.editOrnewCustomer = new ClassCustomer();
            gridLeft.Children.Remove(this);
        }
        private void buttonSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (biz.editOrnewCustomer.id > 0)
            {
                biz.UpdateCustomer();
            }
            else
            {

            }
            gridLeft.Children.Remove(this);
        }

        private void SaveCustomerData()
        {

        }
    }
}
