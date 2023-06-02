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
    public partial class AddEditProductWindow : Window
    {
        private Product _tempProduct;
        public AddEditProductWindow(Product product)
        {
            InitializeComponent();
            _tempProduct = product;
            categoryComboBox.ItemsSource = from _product in DatabaseControl.GetProducts()
                                           where _product.Category != "Все"
                                           group _product
                                           by _product.Category;

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
    }
}
