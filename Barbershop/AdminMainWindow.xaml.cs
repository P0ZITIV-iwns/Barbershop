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
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
            this.mainAdminFrame.Navigate(new EntryDataPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }
        private void EntryDataButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = true;
            ClientDataButton.IsDefault = false;
            ProductDataButton.IsDefault = false;
            ServiceDataButton.IsDefault = false;
            EmployeeDataButton.IsDefault = false;
            FinanceDataButton.IsDefault = false;

            this.mainAdminFrame.Navigate(new EntryDataPage());
        }
        private void ClientDataButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = false;
            ClientDataButton.IsDefault = true;
            ProductDataButton.IsDefault = false;
            ServiceDataButton.IsDefault = false;
            EmployeeDataButton.IsDefault = false;
            FinanceDataButton.IsDefault = false;

            this.mainAdminFrame.Navigate(new ClientDataPage());
        }
        private void ProductDataButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = false;
            ClientDataButton.IsDefault = false;
            ProductDataButton.IsDefault = true;
            ServiceDataButton.IsDefault = false;
            EmployeeDataButton.IsDefault = false;
            FinanceDataButton.IsDefault = false;

            this.mainAdminFrame.Navigate(new ProductDataPage());
        }
        private void ServiceDataButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = false;
            ClientDataButton.IsDefault = false;
            ProductDataButton.IsDefault = false;
            ServiceDataButton.IsDefault = true;
            EmployeeDataButton.IsDefault = false;
            FinanceDataButton.IsDefault = false;

            this.mainAdminFrame.Navigate(new ServiceDataPage());
        }
        private void EmployeeDataButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = false;
            ClientDataButton.IsDefault = false;
            ProductDataButton.IsDefault = false;
            ServiceDataButton.IsDefault = false;
            EmployeeDataButton.IsDefault = true;
            FinanceDataButton.IsDefault = false;

            this.mainAdminFrame.Navigate(new MasterDataPage());
        }
        private void FinanceDataButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = false;
            ClientDataButton.IsDefault = false;
            ProductDataButton.IsDefault = false;
            ServiceDataButton.IsDefault = false;
            EmployeeDataButton.IsDefault = false;
            FinanceDataButton.IsDefault = true;

            this.mainAdminFrame.Navigate(new FinanceDataPage());
        }
    }
}

