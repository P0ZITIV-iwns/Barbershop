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
    public partial class ServiceDataPage : Page
    {
        public ServiceDataPage()
        {
            InitializeComponent();
            servicesDataGridView.ItemsSource = DatabaseControl.GetServices();

        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                servicesDataGridView.ItemsSource = ctx.Service.Where(item => item.Name.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.Category.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                                           item.Duration.ToString().Contains(searchTextBox.Text.ToLower())).ToList();
            }
        }

        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }
        // добавление услуги
        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = null;
            AddEditServiceWindow win = new AddEditServiceWindow(service);
            win.ShowDialog();
            RefreshTable();
        }
        // редактирование услуги
        private void editServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = servicesDataGridView.SelectedItem as Service;
            if (service != null)
            {
                AddEditServiceWindow win = new AddEditServiceWindow(service);
                win.ShowDialog();
                RefreshTable();
            }
        }
        // удаление услуги
        private void deleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = servicesDataGridView.SelectedItem as Service;
            if (service != null)
            {
                DatabaseControl.RemoveService(service);
                RefreshTable();
            }
        }
        // обновление таблиц с данными услуг
        public void RefreshTable()
        {
            servicesDataGridView.ItemsSource = null;
            servicesDataGridView.ItemsSource = DatabaseControl.GetServices();
        }

    }
}
