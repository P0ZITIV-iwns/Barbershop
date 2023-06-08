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
    /// <summary>
    /// Логика взаимодействия для MasterMainWindon.xaml
    /// </summary>
    public partial class MasterMainWindow : Window
    {
        public Employee _currentMaster;
        public MasterMainWindow(Employee currentMaster)
        {
            InitializeComponent();
            _currentMaster = currentMaster;
            this.mainMasterFrame.Navigate(new EntryDataPage(_currentMaster));
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
            EmployeeSettingButton.IsDefault = false;

            this.mainMasterFrame.Navigate(new EntryDataPage(_currentMaster));
        }

        private void EmployeeSettingButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDataButton.IsDefault = false;
            EmployeeSettingButton.IsDefault = true;

            this.mainMasterFrame.Navigate(new AboutEmployeePage(_currentMaster));
        }
    }
}
