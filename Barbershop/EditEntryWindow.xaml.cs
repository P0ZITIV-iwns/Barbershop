using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class EditEntryWindow : Window
    {
        private Entry _tempEntry;
        public EditEntryWindow(Entry entry)
        {
            InitializeComponent();
            _tempEntry = entry;

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
            statusComboBox.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                         group _entry
                                         by _entry.Status;

            firstNameTextBox.Text = entry.ClientEntity.FirstName;
            lastNameTextBox.Text = entry.ClientEntity.LastName;
            phoneTextBox.Text = entry.ClientEntity.Phone;
            serviceCategoryComboBox.SelectedValue = entry.ServiceEntity.Category;
            employeeNameComboBox.SelectedValue = entry.EmployeeEntity.LastName;
            dateTimeTextBox.Text = entry.DateTime.ToString();
            statusComboBox.SelectedValue = entry.Status;
            if (entry.ServiceEntity.Category == "Мужская")
            {
                serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  where _service.Category == "Мужская"
                                                  group _service
                                                  by _service.Name;
            }
            else
            {
                serviceNameComboBox.ItemsSource = from _service in DatabaseControl.GetServices()
                                                  where _service.Category == "Женская"
                                                  group _service
                                                  by _service.Name;
            }
            serviceNameComboBox.SelectedValue = entry.ServiceEntity.Name;
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                _tempEntry.ClientEntity.FirstName = firstNameTextBox.Text;
                _tempEntry.ClientEntity.LastName = lastNameTextBox.Text;
                _tempEntry.ClientEntity.Phone = phoneTextBox.Text;
                _tempEntry.ServiceEntity.Category = (string)serviceCategoryComboBox.SelectedValue;
                _tempEntry.ServiceEntity.Name = (string)serviceNameComboBox.SelectedValue;
                _tempEntry.EmployeeEntity.LastName = (string)employeeNameComboBox.SelectedValue;
                _tempEntry.DateTime = DateTime.SpecifyKind(Convert.ToDateTime(dateTimeTextBox.Text), DateTimeKind.Utc);
                _tempEntry.Status = (string)statusComboBox.SelectedValue;
                DatabaseControl.UpdateEntry(_tempEntry);
                Close();
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
                                                  where _service.Category == "Женская"
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

        // проверка валидности
        private bool Check()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                string firstName = firstNameTextBox.Text.Trim();
                string lastName = lastNameTextBox.Text.Trim();
                string phoneNumber = phoneTextBox.Text.Trim();
                string category = serviceCategoryComboBox.Text;
                string service = serviceNameComboBox.Text;
                string employee = employeeNameComboBox.Text;
                string dateTime = dateTimeTextBox.Text;

                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(phoneNumber) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(service) && string.IsNullOrEmpty(employee) && string.IsNullOrEmpty(dateTime))
                {
                    MessageBox.Show("Поля для ввода имени, телефона, категории, услуги, мастера, даты и времени обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(firstName))
                {
                    MessageBox.Show("Поле для ввода имени обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Поле для ввода номера телефона обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(category))
                {
                    MessageBox.Show("Поле для выбора категории обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(service))
                {
                    MessageBox.Show("Поле для выбора услуги обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(employee))
                {
                    MessageBox.Show("Поле для выбора мастера обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(dateTime))
                {
                    MessageBox.Show("Поле для выбора даты и времени обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(firstName, "^[а-яА-Яa-zA-Z]{2,}$"))
                {
                    MessageBox.Show("Имя должно содержать не менее двух символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!string.IsNullOrEmpty(lastName) && !Regex.IsMatch(lastName, "^[а-яА-Яa-zA-Z]{2,}$"))
                {
                    MessageBox.Show("Фамилия должна содержать не менее двух символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(phoneNumber, "^[0-9]{10}$"))
                {
                    MessageBox.Show("Номер телефона должен состоять из 10 цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!DateTime.TryParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultDateTime))
                {
                    MessageBox.Show("Дата и время должна выглядеть следующего формата: дд.мм.гггг чч:мм", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
    }
}
