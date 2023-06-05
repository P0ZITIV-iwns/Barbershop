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
    public partial class EntryDataPage : Page
    {
        public EntryDataPage()
        {
            InitializeComponent();
            entriesDataGridView.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                              where !string.IsNullOrWhiteSpace(_entry.DateTime)
                                              select _entry;
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            entriesDataGridView.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                              where !string.IsNullOrWhiteSpace(_entry.DateTime) &&
                                              (
                                                  _entry.ClientEntity.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                  _entry.ClientEntity.Phone.Contains(searchTextBox.Text) ||
                                                  _entry.EmployeeEntity.LastName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                  _entry.EmployeeEntity.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                  _entry.ServiceEntity.Name.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                  _entry.Status.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                  _entry.DateTime.ToLower().Contains(searchTextBox.Text.ToLower())
                                              )
                                              select _entry;
        }
        private void searchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }

        // добавление записи
        private void addEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = entriesDataGridView.SelectedItem as Entry;
            AddEntryWindow win = new AddEntryWindow(entry);
            win.ShowDialog();
            RefreshTable();
        }
        // редактирование записи
        private void editEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = entriesDataGridView.SelectedItem as Entry;
            if (entry != null)
            {
                EditEntryWindow win = new EditEntryWindow(entry);
                win.ShowDialog();
                RefreshTable();
            }
        }
        // отмена записи
        private void cancelEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = entriesDataGridView.SelectedItem as Entry;
            if (entry != null)
            {
                entry.Status = "Отменена";
                DatabaseControl.UpdateEntry(entry);
                RefreshTable();
            }
        }
        // обновление таблиц с данными записи
        public void RefreshTable()
        {
            entriesDataGridView.ItemsSource = null;
            entriesDataGridView.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                              where !string.IsNullOrWhiteSpace(_entry.DateTime)
                                              select _entry;
        }

        
    }
}
