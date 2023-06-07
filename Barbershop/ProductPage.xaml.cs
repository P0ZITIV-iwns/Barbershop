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
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            productsListView.ItemsSource = from product in DatabaseControl.GetProducts()
                                           group product
                                           by product.Category;

            productItemsControl.ItemsSource = from product in DatabaseControl.GetProducts()
                                              where !string.IsNullOrEmpty(product.Name)
                                              select product;
        }
        // Фильтрация товаров из выпадающего списка
        private void productsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem comboBoxItem = comboBox.ItemContainerGenerator.ContainerFromItem(comboBox.SelectedItem) as ComboBoxItem;
            if (comboBoxItem == null)
            {
                return;
            }
            TextBlock textBlock = FindVisualChildByName<TextBlock>(comboBoxItem, "categoryTextBlock");
            if (textBlock.Text == "Все")
            {
                productItemsControl.ItemsSource = from product in DatabaseControl.GetProducts()
                                                  where product.Category != textBlock.Text && !string.IsNullOrEmpty(product.Name)
                                                  select product;
            }
            else
            {
                productItemsControl.ItemsSource = from product in DatabaseControl.GetProducts()
                                                  where product.Category == textBlock.Text && !string.IsNullOrEmpty(product.Name)
                                                  select product;
            }
            productItemsControl.ScrollIntoView(productItemsControl.Items.GetItemAt(0));
        }
        // Поиск категории для фильтрации в TextBlock
        private static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                T result = FindVisualChildByName<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void productItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = productItemsControl.SelectedItem as Product;
            if (product != null)
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.mainUserProductFrame.Content = new AboutProductPage(product);
            }
            productItemsControl.UnselectAll();
        }
    }
}
