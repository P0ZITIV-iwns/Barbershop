using Barbershop;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace Barbershop
{
    public partial class AddEditClientWindow : Window
    {
        private Client _tempClient;
        public AddEditClientWindow(Client client)
        {
            InitializeComponent();
            if (client == null)
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
                _tempClient = client;
                firstNameTextBox.Text = client.FirstName;
                lastNameTextBox.Text = client.LastName;
                phoneTextBox.Text = client.Phone.ToString();
            }
            
        }

        private void saveEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                _tempClient.FirstName = firstNameTextBox.Text;
                _tempClient.LastName = lastNameTextBox.Text;
                _tempClient.Phone = phoneTextBox.Text;
                DatabaseControl.UpdateClient(_tempClient);
                Close();
            }
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                DatabaseControl.AddClient(new Client
                {
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    Phone = phoneTextBox.Text,
                });
                Close();
            }
        }


        // проверка валидности
        private bool Check()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                string firstName = firstNameTextBox.Text.Trim();
                string lastName = lastNameTextBox.Text.Trim();
                string phoneNumber = phoneTextBox.Text.Trim();

                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Поля для ввода имени и телефона обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var currentClients = ctx.Client.FirstOrDefault(c => c.Phone == phoneNumber);
                if (currentClients != null && _tempClient == null)
                {
                    MessageBox.Show("Клиент с указанным номером телефона уже имеется в базе данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
    }
}
