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
    public partial class ServicePage : Page
    {
        //List<Service> services;
        public ServicePage()
        {
            InitializeComponent();
            servicesManListBox.ItemsSource = from service in DatabaseControl.GetServices() where service.Category == "Мужская" select service;
            servicesWomanListBox.ItemsSource = from service in DatabaseControl.GetServices() where service.Category == "Женская" select service;
        }
        private void ToggleManButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)manToogle.IsChecked)
            {
                servicesManListBox.Visibility = Visibility.Visible;
                servicesWomanListBox.Visibility = Visibility.Collapsed;
                womanToogle.IsChecked = false;
            }
            else
            {
                servicesManListBox.Visibility = Visibility.Collapsed;
            }
        }
        private void ToggleWomanButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)womanToogle.IsChecked)
            {
                servicesManListBox.Visibility = Visibility.Collapsed;
                servicesWomanListBox.Visibility = Visibility.Visible;
                manToogle.IsChecked = false;
            }
            else
            {
                servicesWomanListBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}