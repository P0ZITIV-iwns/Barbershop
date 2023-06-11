using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
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
    public partial class AddEditServiceWindow : Window
    {
        private Service _tempService;
        public AddEditServiceWindow(Service service)
        {
            InitializeComponent();
            _tempService = service;
            using (DbAppContext ctx = new DbAppContext())
            {
                categoryComboBox.ItemsSource = ctx.Service.GroupBy(item => item.Category).Select(g => new { Category = g.Key }).ToList();
            }
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
            if (Check())
            {
                _tempService.Name = nameTextBox.Text;
                _tempService.Category = (string)categoryComboBox.SelectedValue;
                _tempService.Duration = Convert.ToInt32(durationTextBox.Text);
                _tempService.Price = Convert.ToDecimal(priceTextBox.Text);
                DatabaseControl.UpdateService(_tempService);
                Close();
            }
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
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

        // проверка валидности
        private bool Check()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                string name = nameTextBox.Text.Trim();
                string category = categoryComboBox.Text.Trim();
                string duration = durationTextBox.Text;
                string price = priceTextBox.Text;

                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(duration) && string.IsNullOrEmpty(price))
                {
                    MessageBox.Show("Поля для ввода наименования, категории, продолжительности и стоимости обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Поле для ввода наименования обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(category))
                {
                    MessageBox.Show("Поле для выбора категории обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(duration))
                {
                    MessageBox.Show("Поле для ввода продолжительности обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(price))
                {
                    MessageBox.Show("Поле для ввода стоимости обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                var currentServices = ctx.Service.FirstOrDefault(c => (c.Name == name && c.Category == "Мужская") || (c.Name == name && c.Category == "Женская"));
                if (currentServices != null && _tempService == null)
                {
                    MessageBox.Show("Услуга уже имеется в базе данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(name, "[а-яА-Яa-zA-Z]") && name.Length >= 3)
                {
                    MessageBox.Show("Наименование должно содержать не менее трёх символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!int.TryParse(duration, out int Duration))
                {
                    MessageBox.Show("Продолжительность должна принимать целые числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (int.Parse(duration) <= 0 || int.Parse(duration) > 480)
                {
                    MessageBox.Show("Продолжительность не может быть меньше или равна нулю и не может быть больше 480!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!decimal.TryParse(price, out decimal Price))
                {
                    MessageBox.Show("Стоимость должна принимать числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (decimal.Parse(price) <= 0)
                {
                    MessageBox.Show("Стоимость не может быть меньше или равна нулю!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
    }
}
