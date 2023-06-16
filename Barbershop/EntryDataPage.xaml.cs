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
        private Employee _employee;
        public EntryDataPage(Employee employee)
        {
            _employee = employee;
            using (DbAppContext ctx = new DbAppContext())
            {
                InitializeComponent();
                if (_employee.Post == "Парикмахер")
                {
                    entriesDataGridView.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                                      where _entry.ID_employee == _employee.Id
                                                      select _entry;
                    operationColumn.Visibility = Visibility.Collapsed;
                    addEntryButton.Visibility = Visibility.Collapsed;
                    menu.Visibility = Visibility.Collapsed;
                }
                else
                {
                    entriesDataGridView.ItemsSource = from _entry in  DatabaseControl.GetEntries()
                                                      where _entry.DateTime != DateTime.MinValue
                                                      select _entry;
                }
            }
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_employee.Post == "Парикмахер")
            {
                entriesDataGridView.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                                  where _entry.ID_employee == _employee.Id &&
                                                  (
                                                      _entry.ClientEntity.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.ClientEntity.Phone.Contains(searchTextBox.Text) ||
                                                      _entry.ServiceEntity.Name.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.Status.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.DateTime.ToString().Contains(searchTextBox.Text)
                                                  )
                                                  select _entry;
            }
            else
            {
                entriesDataGridView.ItemsSource = from _entry in DatabaseControl.GetEntries()
                                                  where
                                                  (
                                                      _entry.ClientEntity.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.ClientEntity.Phone.Contains(searchTextBox.Text) ||
                                                      _entry.EmployeeEntity.LastName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.EmployeeEntity.FirstName.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.ServiceEntity.Name.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.Status.ToLower().Contains(searchTextBox.Text.ToLower()) ||
                                                      _entry.DateTime.ToString().Contains(searchTextBox.Text)
                                                  )
                                                  select _entry;
            }
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
        // изменение статуса
        private void changeStatusEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = entriesDataGridView.SelectedItem as Entry;
            if (entry != null)
            {
                if (entry.Status == "Согласование")
                    entry.Status = "Назначена";
                else if (entry.Status == "Назначена")
                    entry.Status = "В процессе";
                else if (entry.Status == "В процессе")
                {
                    entry.Status = "Завершена";
                    using (DbAppContext ctx = new DbAppContext())
                    {
                        DateTime currentTime = entry.DateTime;
                        DateTime newDateTime = currentTime.AddMinutes(entry.ServiceEntity.Duration);
                        DatabaseControl.AddFinance(new Finance
                        {
                            ID_entry = entry.Id,
                            DateTime = DateTime.SpecifyKind(Convert.ToDateTime(newDateTime), DateTimeKind.Utc) 
                        });
                    }
                }
                    
                DatabaseControl.UpdateEntry(entry);
                RefreshTable();
            }
        }
        // отмена записи
        private void cancelEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = entriesDataGridView.SelectedItem as Entry;
            if (entry != null && entry.Status != "Завершена")
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
                                              where _entry.DateTime != DateTime.MinValue
                                              select _entry;
        }
    }
}
