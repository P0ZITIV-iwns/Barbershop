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
    public partial class ClientDataPage : Page
    {
        public ClientDataPage()
        {
            InitializeComponent();
            clientsDataGridView.ItemsSource = DatabaseControl.GetClients();
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            clientsDataGridView.ItemsSource = from client in DatabaseControl.GetClients()
                                              where client.Phone.ToString().Contains(searchTextBox.Text)
                                              select client;

        }
        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Client client = clientsDataGridView.SelectedItem as Client;
            if (client != null)
            {
                AddEditClientWindow win = new AddEditClientWindow(client);
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }

        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = null;
            AddEditClientWindow win = new AddEditClientWindow(client);
            win.ShowDialog();
        }
    }
}