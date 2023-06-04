using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;

namespace Barbershop
{
    public partial class AddEntryWindow : Window
    {
        public AddEntryWindow(Entry entry)
        {
            InitializeComponent();
            if (entry != null)
            {
                firstNameTextBox.Text = entry.ClientEntity.FirstName;
                lastNameTextBox.Text = entry.ClientEntity.LastName;
                phoneTextBox.Text = entry.ClientEntity.Phone;
            }
            serviceCategoryComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  group _service
                                                  by _service.Category;
            serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                              group _service
                                              by _service.Name;
            employeeNameComboBox.ItemsSource = from _emloyee in DatabaseControl.GetEmployees()
                                               where _emloyee.Post == "Парикмахер"
                                               group _emloyee
                                               by _emloyee.Phone;

            ComboBox comboBox = serviceCategoryComboBox;
            ComboBoxItem comboBoxItem = comboBox.ItemContainerGenerator.ContainerFromItem(comboBox.SelectedItem) as ComboBoxItem;
            if (comboBoxItem == null)
            {
                return;
            }
            TextBlock textBlock = FindVisualChildByName<TextBlock>(comboBoxItem, "categoryTextBlock");

            if (textBlock.Text == "Мужская")
            {
                serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  where _service.Category == textBlock.Text
                                                  group _service
                                                  by _service.Name;

            }
            else
            {
                serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  where _service.Category != textBlock.Text
                                                  group _service
                                                  by _service.Name;
            }

        }

        private void serviceCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem comboBoxItem = comboBox.ItemContainerGenerator.ContainerFromItem(comboBox.SelectedItem) as ComboBoxItem;
            if (comboBoxItem == null)
            {
                return;
            }
            TextBlock textBlock = FindVisualChildByName<TextBlock>(comboBoxItem, "categoryTextBlock");

            if (textBlock.Text == "Мужская")
            {
                serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  where _service.Category == textBlock.Text
                                                  group _service
                                                  by _service.Name;

            }
            else
            {
                serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  where _service.Category != textBlock.Text
                                                  group _service
                                                  by _service.Name;
            }
        }

        // Поиск контента у ComboBox в TextBlock
        private static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                T result = FindVisualChildByName<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            var cultureInfo = new CultureInfo("ru-RU");

            if ((from client in DatabaseControl.GetClients() where client.Phone.ToString() == phoneTextBox.Text select client.Phone) == null)
            {
                DatabaseControl.AddClient(new Client
                {
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    Phone = phoneTextBox.Text
                });
            }
            

            //DatabaseControl.AddEntry(new Entry
            //{
            //    DateTime = Convert.ToDateTime(dateTimeTextBox.Text).ToString("dd/MM/yyyy hh:G")
            //});
            Close();
        }
    }
}

