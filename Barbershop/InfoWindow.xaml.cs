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
                countAllEntries.Text = ctx.Entry.Count().ToString();

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
                    countAllEntries.Text = ctx.Entry.Count().ToString();

                    countSellWomenServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена").Count().ToString();
                    sumSellWomenServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    countSellManServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена").Count().ToString();
                    sumSellManServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    sumAllSellServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.Status == "Завершена").GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                }
                else if (infoTimeComboBox.SelectedIndex == 1)
                {
                    DateTime lastWeek = DateTime.Today.AddDays(-7);
                    countAssignedEntries.Text = ctx.Entry.Where(item => item.Status == "Назначена" && item.DateTime >= lastWeek).Count().ToString();
                    countInProcessEntries.Text = ctx.Entry.Where(item => item.Status == "В процессе" && item.DateTime >= lastWeek).Count().ToString();
                    countCancelEntries.Text = ctx.Entry.Where(item => item.Status == "Отменена" && item.DateTime >= lastWeek).Count().ToString(); ;
                    countCompletedEntries.Text = ctx.Finance.Where(item => item.DateTime >= lastWeek).Count().ToString();
                    countAllEntries.Text = ctx.Entry.Where(item => item.DateTime >= lastWeek).Count().ToString();

                    countSellWomenServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена" && item.DateTime >= lastWeek).Count().ToString();
                    sumSellWomenServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена" && item.DateTime >= lastWeek).GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    countSellManServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена" && item.DateTime >= lastWeek).Count().ToString();
                    sumSellManServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена" && item.DateTime >= lastWeek).GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    sumAllSellServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.Status == "Завершена" && item.DateTime >= lastWeek).GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                }
                else if (infoTimeComboBox.SelectedIndex == 2)
                {
                    DateTime lastMonth = DateTime.Today.AddDays(-30);
                    countAssignedEntries.Text = ctx.Entry.Where(item => item.Status == "Назначена" && item.DateTime >= lastMonth).Count().ToString();
                    countInProcessEntries.Text = ctx.Entry.Where(item => item.Status == "В процессе" && item.DateTime >= lastMonth).Count().ToString();
                    countCancelEntries.Text = ctx.Entry.Where(item => item.Status == "Отменена" && item.DateTime >= lastMonth).Count().ToString(); ;
                    countCompletedEntries.Text = ctx.Finance.Where(item => item.DateTime >= lastMonth).Count().ToString();
                    countAllEntries.Text = ctx.Entry.Where(item => item.DateTime >= lastMonth).Count().ToString();

                    countSellWomenServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена" && item.DateTime >= lastMonth).Count().ToString();
                    sumSellWomenServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Женская" && item.Status == "Завершена" && item.DateTime >= lastMonth).GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    countSellManServices.Text = ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена" && item.DateTime >= lastMonth).Count().ToString();
                    sumSellManServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.ServiceEntity.Category == "Мужская" && item.Status == "Завершена" && item.DateTime >= lastMonth).GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                    sumAllSellServices.Text = String.Format("{0:0.##}", ctx.Entry.Where(item => item.Status == "Завершена" && item.DateTime >= lastMonth).GroupBy(item => item.ServiceEntity.Price).Select(item => item.Key).Sum());
                }
            }
        }
    }
}
