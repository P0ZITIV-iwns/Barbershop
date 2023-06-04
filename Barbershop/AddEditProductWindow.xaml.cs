using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class AddEditProductWindow : Window
    {
        private Product _tempProduct;
        private const string _Image = "Images\\Products";
        private OpenFileDialog _img;
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
            _tempProduct.Name = nameTextBox.Text;
            _tempProduct.Category = (string)categoryComboBox.SelectedValue;
            _tempProduct.Description = descriptionTextBox.Text;
            _tempProduct.Price = Convert.ToDecimal(priceTextBox.Text);
            DatabaseControl.UpdateProduct(_tempProduct);
            Close();
        }

        private void saveAddButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            if (_img == null)
            {
                filePath = "Images/Products/noProductImage.png";
            }
            else
            {
                //filePath = System.IO.Path.Combine(_Image, _img.SafeFileName);
                //File.Copy(_img.FileName, filePath, true);
                filePath = "Images/Products/noProductImage.png";
            }
            DatabaseControl.AddProduct(new Product
            {
                Name = nameTextBox.Text,
                Category = (string)categoryComboBox.SelectedValue,
                Description = descriptionTextBox.Text,
                Price = Convert.ToDecimal(priceTextBox.Text),
                Image = filePath
            });
            Close();
        }
    }
}
