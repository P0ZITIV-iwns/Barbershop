using Barbershop;
using Microsoft.EntityFrameworkCore.Metadata;
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
            _tempClient.FirstName = firstNameTextBox.Text;
            _tempClient.LastName = lastNameTextBox.Text;
            _tempClient.Phone = phoneTextBox.Text;
            DatabaseControl.UpdateClient(_tempClient);
            Close();
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
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text) && string.IsNullOrWhiteSpace(phoneTextBox.Text)){
                MessageBox.Show("Поля для ввода имени и телефона обязательны!", "Ошибка");
                return false;   
            }else if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
            {
                MessageBox.Show("Поле для ввода имени обязательно!", "Ошибка");
                return false;
            }else if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
            {
                MessageBox.Show("Поле для ввода телефона обязательно!", "Ошибка");
                return false;
            }else if ((from client in DatabaseControl.GetClients() where client.Phone.ToString() == phoneTextBox.Text select client.Phone) != null)
            {
                MessageBox.Show("Клиент с введёным телефоном уже имеется в базе данных!", "Ошибка");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
