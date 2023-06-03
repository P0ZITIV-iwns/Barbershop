using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.mainUserProductFrame.Content = null;
            this.mainUserFrame.Navigate(new AboutPage());
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            var window = new Authorization();
            window.Show();
            this.Close();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutButton.IsDefault = true;
            ProductButton.IsDefault = false;
            ServiceButton.IsDefault = false;
            MasterButton.IsDefault = false;
            ContactButton.IsDefault = false;

            this.mainUserProductFrame.Content = null;
            this.mainUserFrame.Navigate(new AboutPage());

        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            AboutButton.IsDefault = false;
            ProductButton.IsDefault = true;
            ServiceButton.IsDefault = false;
            MasterButton.IsDefault = false;
            ContactButton.IsDefault = false;

            this.mainUserProductFrame.Content = null;
            this.mainUserFrame.Navigate(new ProductPage());
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            AboutButton.IsDefault = false;
            ProductButton.IsDefault = false;
            ServiceButton.IsDefault = true;
            MasterButton.IsDefault = false;
            ContactButton.IsDefault = false;

            this.mainUserProductFrame.Content = null;
            this.mainUserFrame.Navigate(new ServicePage());
        }

        private void MasterButton_Click(object sender, RoutedEventArgs e)
        {
            AboutButton.IsDefault = false;
            ProductButton.IsDefault = false;
            ServiceButton.IsDefault = false;
            MasterButton.IsDefault = true;
            ContactButton.IsDefault = false;

            this.mainUserProductFrame.Content = null;
            this.mainUserFrame.Navigate(new MasterPage());
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            AboutButton.IsDefault = false;
            ProductButton.IsDefault = false;
            ServiceButton.IsDefault = false;
            MasterButton.IsDefault = false;
            ContactButton.IsDefault = true;

            this.mainUserProductFrame.Content = null;
            this.mainUserFrame.Navigate(new ContactPage());
        }

        private void EntryButton_Click(object sender, RoutedEventArgs e)
        {
            AddEntryWindow win = new AddEntryWindow(null);
            win.ShowDialog();
        }
    }
}
