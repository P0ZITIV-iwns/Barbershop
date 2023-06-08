using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Barbershop
{
    public partial class AboutEmployeePage : Page
    {
        private Employee _tempEmployee;
        public AboutEmployeePage(Employee _employee)
        {
            _tempEmployee = _employee;
            InitializeComponent();
            lastNameEmployeePageView.Text = _employee.LastName;
            firstNameEmployeePageView.Text = _employee.FirstName;
            patronymicEmployeePageView.Text = _employee.Patronymic;
            phoneEmployeePageView.Text = _employee.Phone;
            loginEmployeePageView.Text = _employee.Login;
            passwordEmployeePageView.Text = _employee.Password;
            BitmapImage _bitmapImage = new BitmapImage();
            var source = Environment.CurrentDirectory + "\\..\\..\\..\\" + _employee.Photo;
            using (var stream = File.OpenRead(source))
            {
                _bitmapImage.BeginInit();
                _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                _bitmapImage.StreamSource = stream;
                _bitmapImage.EndInit();
            }
            photoEmployeePageView.Source = _bitmapImage;
        }

        private void editLoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginTextBlock.Visibility = Visibility.Collapsed;
            loginWritePanel.Visibility = Visibility.Visible;
            loginTextBox.Text = _tempEmployee.Login;
            loginTextBox.CaretIndex = loginTextBox.Text.Length;
            loginTextBox.ScrollToEnd();
            loginTextBox.Focus();
        }

        private void saveLoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginTextBlock.Visibility = Visibility.Visible;
            loginWritePanel.Visibility = Visibility.Collapsed;
            if (CheckLogin())
            {
                _tempEmployee.Login = loginTextBox.Text;
                DatabaseControl.UpdateEmployee(_tempEmployee);
                loginEmployeePageView.Text = _tempEmployee.Login;
            }
        }

        private void editPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            passwordTextBlock.Visibility = Visibility.Collapsed;
            passwordWritePanel.Visibility = Visibility.Visible;
            passwordTextBox.Text = _tempEmployee.Password;
            passwordTextBox.CaretIndex = passwordTextBox.Text.Length;
            passwordTextBox.ScrollToEnd();
            passwordTextBox.Focus();
        }

        private void savePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            passwordTextBlock.Visibility = Visibility.Visible;
            passwordWritePanel.Visibility = Visibility.Collapsed;
            if (CheckPassword())
            {
                _tempEmployee.Password = passwordTextBox.Text;
                DatabaseControl.UpdateEmployee(_tempEmployee);
                passwordEmployeePageView.Text = _tempEmployee.Password;
            }
        }

        // проверка валидности
        private bool CheckLogin()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                string login = loginTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();
                if (!Regex.IsMatch(login, @"^[a-zA-Z\d*_.-]{4,}$"))
                {
                    MessageBox.Show("Логин должен содержать не менее четырёх символов и состоять из букв английского алфавита, цифр и спецсимволов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
        private bool CheckPassword()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                string login = loginTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();
                if (!Regex.IsMatch(password, @"^[a-zA-Z\d!#@$%&'*+\-/=?^_`{|}~]{5,}$"))
                {
                    MessageBox.Show("Пароль должен содержать не менее пяти символов и состоять из букв английского алфавита, цифр и спецсимволов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
    }
}


