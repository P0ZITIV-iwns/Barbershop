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
    }
}
