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

            using (DbAppContext ctx = new DbAppContext())
            {
                clientsDataGridView.ItemsSource = ctx.Client.Where(item => item.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) || 
                                                                           item.LastName.ToLower().Contains(searchTextBox.Text.ToLower()) || 
                                                                           item.Phone.Contains(searchTextBox.Text)).ToList();
            }
        }
        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }
        // добавление клиента
        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = null;
            AddEditClientWindow win = new AddEditClientWindow(client);
            win.ShowDialog();
            RefreshTable();
        }
        // редактирование клиента
        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = clientsDataGridView.SelectedItem as Client;
            if (client != null)
            {
                AddEditClientWindow win = new AddEditClientWindow(client);
                win.ShowDialog();
                RefreshTable();
            }
        }
        // удаление клиента
        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = clientsDataGridView.SelectedItem as Client;
            if (client != null)
            {
                using (DbAppContext ctx = new DbAppContext())
                {
                    var entry = ctx.Entry.Where(item => item.ID_client == client.Id).ToList();
                    foreach (var item in entry)
                        DatabaseControl.RemoveEntry(item);
                    ctx.SaveChanges();
                }
                DatabaseControl.RemoveClient(client);
                RefreshTable();
            }
        }
        // обновление таблиц с данными клиентов
        public void RefreshTable()
        {
            clientsDataGridView.ItemsSource = null;
            clientsDataGridView.ItemsSource = DatabaseControl.GetClients();
        }
    }
}