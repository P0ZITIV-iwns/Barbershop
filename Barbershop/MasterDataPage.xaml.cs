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


        // добавление сотрудника
        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = null;
            AddEditEmployeeWindow win = new AddEditEmployeeWindow(employee);
            win.ShowDialog();
            RefreshTable();
        }
        // редактирование сотрудника
        private void editEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = employeesDataGridView.SelectedItem as Employee;
            if (employee != null)
            {
                AddEditEmployeeWindow win = new AddEditEmployeeWindow(employee);
                win.ShowDialog();
                RefreshTable();
            }
        }
        // удаление сотрудника
        private void deleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = employeesDataGridView.SelectedItem as Employee;
            if (employee != null)
            {
                DatabaseControl.RemoveEmployee(employee);
                RefreshTable();
            }
        }
        // обновление таблиц с данными сотрудников
        public void RefreshTable()
        {
            employeesDataGridView.ItemsSource = null;
            employeesDataGridView.ItemsSource = DatabaseControl.GetEmployees();
        }
    }
}
