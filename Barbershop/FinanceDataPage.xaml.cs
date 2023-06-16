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
    /// <summary>
    /// Логика взаимодействия для FinanceDataPage.xaml
    /// </summary>
    public partial class FinanceDataPage : Page
    {
        public FinanceDataPage()
        {
            InitializeComponent();
            //financeDataGridView.ItemsSource = DatabaseControl.GetFinances();
            var query = from finance in DatabaseControl.GetFinances()
                        join entry in DatabaseControl.GetEntries() on finance.ID_entry equals entry.Id
                        join service in DatabaseControl.GetServices() on entry.ID_service equals service.Id
                        join employee in DatabaseControl.GetEmployees() on entry.ID_employee equals employee.Id
                        select new
                        {
                            ServiceName = service.Name,
                            ServicePriceNoEdit = service.Price,
                            EmployeeLastName = employee.LastName,
                            EmployeeFirstName = employee.FirstName,
                            finance.Id,
                            finance.DateTime
                        };
            financeDataGridView.ItemsSource = query.ToList();
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow win = new InfoWindow();
            win.ShowDialog();
        }
    }
}
