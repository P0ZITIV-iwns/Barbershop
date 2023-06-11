using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Barbershop
{
    public partial class AddEditEmployeeWindow : Window
    {
        private Employee _tempEmployee;
        string _photoSource = Environment.CurrentDirectory + "\\..\\..\\..\\" + "Images\\Employees";
        string _photoSourceToDatabase = "Images/Employees/";
        private OpenFileDialog _img;
        string filePath;
        public AddEditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            if (employee == null)
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
                _tempEmployee = employee;

                lastNameTextBox.Text = employee.LastName;
                firstNameTextBox.Text = employee.FirstName;
                patronymicTextBox.Text = employee.Patronymic;
                loginTextBox.Text = employee.Login;
                passwordTextBox.Text = employee.Password;
                phoneTextBox.Text = employee.Phone;
            }

        }

        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg, *.png)|*.jpg;*.png;*.JPG;*.PNG";
            if (openFileDialog.ShowDialog() == true)
            {
                _img = openFileDialog;
            }
        }
        private void saveEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (_tempEmployee.Photo != "Images/Employees/noEmployeeImage.png")
                {
                    if (_img != null)
                    {
                        File.Delete(_tempEmployee.FullPathToPhoto);
                    }
                }
                if (_img != null)
                {
                    filePath = System.IO.Path.Combine(_photoSource, _img.SafeFileName);
                    File.Copy(_img.FileName, filePath, true);
                    _tempEmployee.Photo = System.IO.Path.Combine(_photoSourceToDatabase, _img.SafeFileName);
                }
                
                _tempEmployee.LastName = lastNameTextBox.Text;
                _tempEmployee.FirstName = firstNameTextBox.Text;
                _tempEmployee.Patronymic = patronymicTextBox.Text;
                _tempEmployee.Login = loginTextBox.Text;
                _tempEmployee.Password = passwordTextBox.Text;
                _tempEmployee.Phone = phoneTextBox.Text;
                DatabaseControl.UpdateEmployee(_tempEmployee);
                Close();
            }
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                string filePathBase;
                if (_img == null)
                {
                    filePathBase = "Images/Employees/noEmployeeImage.png";
                }
                else
                {
                    filePath = System.IO.Path.Combine(_photoSource, _img.SafeFileName);
                    File.Copy(_img.FileName, filePath, true);
                    filePathBase = System.IO.Path.Combine(_photoSourceToDatabase, _img.SafeFileName);
                }
                DatabaseControl.AddEmployee(new Employee
                {
                    LastName = lastNameTextBox.Text,
                    FirstName = firstNameTextBox.Text,
                    Patronymic = patronymicTextBox.Text,
                    Login = loginTextBox.Text,
                    Password = passwordTextBox.Text,
                    Phone = phoneTextBox.Text,
                    Post = "Парикмахер",
                    Photo = filePathBase
                });
                Close();
            }
        }

        // проверка валидности
        private bool Check()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                string lastName = lastNameTextBox.Text.Trim();
                string firstName = firstNameTextBox.Text.Trim();
                string patronymic = patronymicTextBox.Text.Trim();
                string login = loginTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();
                string phoneNumber = phoneTextBox.Text.Trim();

                if (string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(phoneNumber) && string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Все поля для ввода обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(lastName, "^[а-яА-Яa-zA-Z]{2,}$"))
                {
                    MessageBox.Show("Фамилия должна содержать не менее двух символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(firstName, "^[а-яА-Яa-zA-Z]{2,}$"))
                {
                    MessageBox.Show("Имя должно содержать не менее двух символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!string.IsNullOrEmpty(patronymic) && !Regex.IsMatch(patronymic, "^[а-яА-Яa-zA-Z]{2,}$"))
                {
                    MessageBox.Show("Отчество должно содержать не менее двух символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!Regex.IsMatch(login, @"^[a-zA-Z\d*_.-]{4,}$"))
                {
                    MessageBox.Show("Логин должен содержать не менее четырёх символов и состоять из букв английского алфавита, цифр и спецсимволов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(password, @"^[a-zA-Z\d!#@$%&'*+\-/=?^_`{|}~]{5,}$"))
                {
                    MessageBox.Show("Пароль должен содержать не менее пяти символов и состоять из букв английского алфавита, цифр и спецсимволов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(phoneNumber, "^[0-9]{10}$"))
                {
                    MessageBox.Show("Номер телефона должен состоять из 10 цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                var currentClients = ctx.Client.FirstOrDefault(c => c.Phone == phoneNumber);
                if (currentClients != null && _tempEmployee == null)
                {
                    MessageBox.Show("Сотрудник с указанным номером телефона уже имеется в базе данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
    }
}
