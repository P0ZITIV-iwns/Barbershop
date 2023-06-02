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
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();

        }

        private void DefaultButtonStyle_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminMainWindow();
            window.Show();
            this.Close();
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordPasswordBox.Password.Length > 0)
            {
                placeholder.Visibility = Visibility.Collapsed;
            }
            else
            {
                placeholder.Visibility = Visibility.Visible;
            }
        }

        private void passwordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordTextBox.Text.Length > 0)
            {
                placeholder.Visibility = Visibility.Collapsed;
            }
            else
            {
                placeholder.Visibility = Visibility.Visible;
            }
        }

        private void checkPassword_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                passwordTextBox.Text = passwordPasswordBox.Password; // Копируем пароль в TextBox из PasswordBox
                passwordTextBox.Visibility = Visibility.Visible; // Отобразить TextBox
                passwordPasswordBox.Visibility = Visibility.Collapsed; // Скрыть PasswordBox

            }
            else
            {
                passwordPasswordBox.Password = passwordTextBox.Text; // Копируем пароль в PasswordBox из TextBox 
                passwordTextBox.Visibility = Visibility.Collapsed; // Скрыть TextBox
                passwordPasswordBox.Visibility = Visibility.Visible; // Отобразить PasswordBox

            }
        }


        private void imagePassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (showImage.Visibility == Visibility.Visible)
            {
                passwordTextBox.Text = passwordPasswordBox.Password; // Копируем пароль в TextBox из PasswordBox
                passwordTextBox.Visibility = Visibility.Visible; // Отобразить TextBox
                passwordPasswordBox.Visibility = Visibility.Collapsed; // Скрыть PasswordBox
                showImage.Visibility = Visibility.Collapsed;
                hideImage.Visibility = Visibility.Visible;
                passwordTextBox.CaretIndex = passwordTextBox.Text.Length;
                passwordTextBox.ScrollToEnd();
                passwordTextBox.Focus();
            }
            else
            {
                passwordPasswordBox.Password = passwordTextBox.Text; // Копируем пароль в PasswordBox из TextBox 
                passwordPasswordBox.Visibility = Visibility.Visible; // Отобразить PasswordBox
                passwordTextBox.Visibility = Visibility.Collapsed; // Скрыть TextBox
                showImage.Visibility = Visibility.Visible;
                hideImage.Visibility = Visibility.Collapsed;

                passwordPasswordBox.Focus();
            }
        }
    }
}