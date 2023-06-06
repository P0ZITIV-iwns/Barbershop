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
            using (DbAppContext ctx = new DbAppContext())
            {
                employeesDataGridView.ItemsSource = ctx.Employee.Where(item => item.Post != "Администратор").ToList();
            }
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                employeesDataGridView.ItemsSource = ctx.Employee.Where(item => item.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.LastName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.Patronymic.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.Post.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.Login.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.Phone.Contains(searchTextBox.Text)).ToList();
            }
        }

        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }
    }
}
