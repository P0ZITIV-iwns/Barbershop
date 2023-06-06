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
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
            using (DbAppContext ctx = new DbAppContext())
            {
                countAssignedEntries.Text = ctx.Entry.Where(item => item.Status == "Назначена").Count().ToString();
                countInProcessEntries.Text = ctx.Entry.Where(item => item.Status == "В процессе").Count().ToString();
                countCancelEntries.Text = ctx.Entry.Where(item => item.Status == "Отменена").Count().ToString();
                countCompletedEntries.Text = ctx.Finance.Count().ToString();

                countSellWomenServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена").Count().ToString();
                sumSellWomenServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                countSellManServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена").Count().ToString();
                sumSellManServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                sumAllSellServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
            }

        }

        private void infoTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ComboBox comboBox = infoTimeComboBox;
                ComboBoxItem comboBoxItem = comboBox.ItemContainerGenerator.ContainerFromItem(comboBox.SelectedItem) as ComboBoxItem;
                if (comboBoxItem == null)
                {
                    return;
                }
                if (infoTimeComboBox.SelectedIndex == 0)
                {
                    countAssignedEntries.Text = ctx.Entry.Where(item => item.Status == "Назначена").Count().ToString();
                    countInProcessEntries.Text = ctx.Entry.Where(item => item.Status == "В процессе").Count().ToString();
                    countCancelEntries.Text = ctx.Entry.Where(item => item.Status == "Отменена").Count().ToString();
                    countCompletedEntries.Text = ctx.Finance.Count().ToString();

                    countSellWomenServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена").Count().ToString();
                    sumSellWomenServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    countSellManServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена").Count().ToString();
                    sumSellManServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    sumAllSellServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                }
                else if (infoTimeComboBox.SelectedIndex == 1)
                {
                    countAssignedEntries.Text = "0";
                    countInProcessEntries.Text = "0";
                    countCancelEntries.Text = "0";
                    countCompletedEntries.Text = "0";

                    countSellWomenServices.Text = "0";
                    sumSellWomenServices.Text = "0";
                    countSellManServices.Text = "0";
                    sumSellManServices.Text = "0";
                    sumAllSellServices.Text = "0";
                }
                else if (infoTimeComboBox.SelectedIndex == 2)
                {
                    countAssignedEntries.Text = "1";
                    countInProcessEntries.Text = "1";
                    countCancelEntries.Text = "1";
                    countCompletedEntries.Text = "1";

                    countSellWomenServices.Text = "1";
                    sumSellWomenServices.Text = "1";
                    countSellManServices.Text = "1";
                    sumSellManServices.Text = "1";
                    sumAllSellServices.Text = "1";
                }
            }
        }
    }
}
