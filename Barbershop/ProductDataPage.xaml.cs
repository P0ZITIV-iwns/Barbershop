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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Barbershop
{
    public partial class ProductDataPage : Page
    {
        public ProductDataPage()
        {
            InitializeComponent();
            //productsDataGridView.ItemsSource = from product in DatabaseControl.GetProducts()
            //                                   where product.Category != "Все"
            //                                   select product;
            productsDataGridView.ItemsSource = DatabaseControl.GetProducts();
        }
        private void AboutProductMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Product product = productsDataGridView.SelectedItem as Product;
            if (product != null)
            {
                AdminMainWindow mw = Application.Current.Windows.OfType<AdminMainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.mainAdminFrame.Content = new AboutProductPage(product);
            }
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            productsDataGridView.ItemsSource = from product in DatabaseControl.GetProducts()
                                               where product.Name.ToString().ToUpper().Contains(searchTextBox.Text.ToUpper())
                                               select product;
        }
        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }

        // добавление товара
        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = null;
            AddEditProductWindow win = new AddEditProductWindow(product);
            win.ShowDialog();
            RefreshTable();
        }
        // редактирование товара
        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = productsDataGridView.SelectedItem as Product;
            if (product != null)
            {
                AddEditProductWindow win = new AddEditProductWindow(product);
                win.ShowDialog();
                RefreshTable();
            }
        }
        // удаление товара
        private void deleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = productsDataGridView.SelectedItem as Product;
            if (product != null)
            {
                DatabaseControl.RemoveProduct(product);
                RefreshTable();
            }
        }
        // обновление таблиц с данными товаров
        public void RefreshTable()
        {
            productsDataGridView.ItemsSource = null;
            productsDataGridView.ItemsSource = from product in DatabaseControl.GetProducts()
                                               where product.Category != "Все"
                                               select product;
        }
    }
}