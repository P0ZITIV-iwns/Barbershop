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

            servicesDataGridView.ItemsSource = from service in DatabaseControl.GetServices()
                                               where service.Name.ToString().ToUpper().Contains(searchTextBox.Text.ToUpper())
                                               select service;
        }

        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Service service = servicesDataGridView.SelectedItem as Service;
            if (service != null)
            {
                AddEditServiceWindow win = new AddEditServiceWindow(service);
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }
        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = null;
            AddEditServiceWindow win = new AddEditServiceWindow(service);
            win.ShowDialog();
        }
    }
}
