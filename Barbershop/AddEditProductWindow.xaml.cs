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
    public partial class AddEditProductWindow : Window
    {
        private Product _tempProduct;
        string _imageSource = Environment.CurrentDirectory + "\\..\\..\\..\\" + "Images\\Products";
        string _imageSourceToDatabase = "Images/Products/";
        private OpenFileDialog _img;
        string filePath;
        public AddEditProductWindow(Product product)
        {
            InitializeComponent();
            _tempProduct = product;
            //categoryComboBox.ItemsSource = from _product in DatabaseControl.GetProducts()
            //                               where _product.Category != "Все"
            //                               group _product
            //                               by _product.Category;
            using (DbAppContext ctx = new DbAppContext())
            {
                categoryComboBox.ItemsSource = ctx.Product.Where(item => item.Category != "Все").GroupBy(item => item.Category).Select(g => new { Category = g.Key }).ToList();
            }

            if (product == null)
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
                categoryComboBox.SelectedValue = product.Category;
                nameTextBox.Text = product.Name;
                descriptionTextBox.Text = product.Description;
                priceTextBox.Text = product.Price.ToString();
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
                if (_tempProduct.Image != "Images/Products/noProductImage.png")
                {
                    if (_img != null)
                    {
                        File.Delete(_tempProduct.FullPathToImage);
                    } 
                }
                if (_img != null)
                {
                    filePath = System.IO.Path.Combine(_imageSource, _img.SafeFileName);
                    File.Copy(_img.FileName, filePath, true);
                    _tempProduct.Image = System.IO.Path.Combine(_imageSourceToDatabase, _img.SafeFileName);
                }


                _tempProduct.Name = nameTextBox.Text;
                _tempProduct.Category = (string)categoryComboBox.SelectedValue;
                _tempProduct.Description = descriptionTextBox.Text;
                _tempProduct.Price = Convert.ToDecimal(priceTextBox.Text);
                DatabaseControl.UpdateProduct(_tempProduct);
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
                    filePathBase = "Images/Products/noProductImage.png";
                }
                else
                {
                    filePath = System.IO.Path.Combine(_imageSource, _img.SafeFileName);
                    File.Copy(_img.FileName, filePath, true);
                    filePathBase = System.IO.Path.Combine(_imageSourceToDatabase, _img.SafeFileName);
                }
                DatabaseControl.AddProduct(new Product
                {
                    Name = nameTextBox.Text,
                    Category = (string)categoryComboBox.SelectedValue,
                    Description = descriptionTextBox.Text,
                    Price = Convert.ToDecimal(priceTextBox.Text),
                    Image = filePathBase
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
                string description = descriptionTextBox.Text.Trim();
                string price = priceTextBox.Text;

                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(price))
                {
                    MessageBox.Show("Поля для ввода наименования, категории и стоимости обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (string.IsNullOrEmpty(price))
                {
                    MessageBox.Show("Поле для ввода стоимости обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                var currentProducts = ctx.Product.FirstOrDefault(c => c.Name == name);
                if (currentProducts != null && _tempProduct == null)
                {
                    MessageBox.Show("Товар с указанным наименованием уже имеется в базе данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!Regex.IsMatch(name, "[а-яА-Яa-zA-Z]"))
                {
                    MessageBox.Show("Наименование должно содержать не менее трёх символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!string.IsNullOrEmpty(description) && !Regex.IsMatch(description, "[а-яА-Яa-zA-Z]") && description.Length >= 3)
                {
                    MessageBox.Show("Описание должно содержать не менее трёх символов и состоять только из букв русского или английского алфавита!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
