using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using System.Windows.Shapes;

namespace Barbershop
{
    public partial class AddEditServiceWindow : Window
    {
        private Service _tempService;
        public AddEditServiceWindow(Service service)
        {
            InitializeComponent();
            _tempService = service;
            categoryComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                           group _service
                                           by _service.Category;
            if (service == null)
            {
                headerWindow.Text = "Добавление";
                saveAddButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                headerWindow.Text = "Редактирование";
                saveAddButton.Visibility = Visibility.Collapsed;
                saveEditButton.Visibility = Visibility.Visible;
                categoryComboBox.SelectedValue = service.Category;
                nameTextBox.Text = service.Name;
                durationTextBox.Text = service.Duration.ToString();
                priceTextBox.Text = service.Price.ToString();
            }
        }

        private void saveEditButton_Click(object sender, RoutedEventArgs e)
        {
            _tempService.Name = nameTextBox.Text;
            _tempService.Category = (string)categoryComboBox.SelectedValue;
            _tempService.Duration = Convert.ToInt32(durationTextBox.Text);
            _tempService.Price = Convert.ToDecimal(priceTextBox.Text);
            DatabaseControl.UpdateService(_tempService);
            Close();
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl.AddService(new Service
            {
                Name = nameTextBox.Text,
                Category = (string)categoryComboBox.SelectedValue,
                Duration = Convert.ToInt32(durationTextBox.Text),
                Price = Convert.ToDecimal(priceTextBox.Text)
            });
            Close();
        }
    }
}
