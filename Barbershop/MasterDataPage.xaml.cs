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

namespace Barbershop
{
    public partial class MasterDataPage : Page
    {
        public MasterDataPage()
        {
            InitializeComponent();
            employeesDataGridView.ItemsSource = DatabaseControl.GetEmployees();
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            employeesDataGridView.ItemsSource = from employee in DatabaseControl.GetEmployees()
                                                where employee.LastName.ToString().ToUpper().Contains(searchTextBox.Text.ToUpper())
                                                select employee;
        }

        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }
    }
}
