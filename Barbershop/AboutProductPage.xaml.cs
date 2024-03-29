﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Barbershop
{
    public partial class AboutProductPage : Page
    {
        public AboutProductPage(Product _product)
        {
            InitializeComponent();

            nameProductPageView.Text = _product.Name;
            categoryProductPageView.Text = _product.Category;
            priceProductPageView.Text = (String.Format("{0:0.##}", _product.Price));
            descriptionProductPageView.Text = _product.Description;
            BitmapImage _bitmapImage = new BitmapImage();
            var source = Environment.CurrentDirectory + "\\..\\..\\..\\" + _product.Image;
            using (var stream = File.OpenRead(source))
            {
                _bitmapImage.BeginInit();
                _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                _bitmapImage.StreamSource = stream;
                _bitmapImage.EndInit();
            }
            imageProductPageView.Source = _bitmapImage;

            //imageProductPageView.Source = new BitmapImage(new Uri(_product.Image, UriKind.Relative));
        }
        private void BackArrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();
            else Content = null;
        }
    }
}