using EntityFramework.Models;
using EntityFramework.Services;
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

namespace PoliceApp
{
    /// <summary>
    /// Logika interakcji dla klasy Suborneds.xaml
    /// </summary>
    public partial class Suborneds : Page
    {
        private ICollection<Policeman> data;
        private ICollection<PoliceCar> radiowozy;
        private DatabaseService databaseService = new();
        private SharedData user = SharedData.GetInstance(null);
        private bool IdOrder = false;
        public Suborneds()
        {
            var policjant = databaseService.GetUserByObj(user.uzytkownik);
            data = databaseService.GetSubordinates(policjant.Policeman);
            radiowozy = databaseService.GetPoliceCars();
            InitializeComponent();
            ListViewColumns.ItemsSource = data;
            RadiowozBox.ItemsSource = radiowozy;
            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));

        }

        private void ListView_OnColumnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType().Name != "GridViewColumnHeader")
                return;
            string headerName = (e.OriginalSource as GridViewColumnHeader).Content.ToString();
            switch (headerName)
            {
                case "ID":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.PolicemanId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.PolicemanId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "FirstName":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.FirstName).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.FirstName).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Surname":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Surname).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Surname).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Age":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Ranga.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Ranga.Name).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "PoliceStation adres":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.PoliceStation.Address).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.PoliceStation.Address).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
            }
            ListViewColumns.ItemsSource = data;
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Policeman;
            if (item != null)
            {
                Window patrolsingle = new PlanOfSuborned(item);
                patrolsingle.Show();
            }
        }
        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            if(ListViewColumns.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nikogo nie wybrałeś", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                if (RadiowozBox.SelectedItem == null || Data_roz.Text == null || Godzina_Zak.Text == null || Data_zak.Text == null || Godzina_roz.Text == null)
                {
                    MessageBox.Show("Wprowadzono złe dane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(Data_roz.Text.Count() == 0 || Godzina_Zak.Text.Count() == 0 || Data_zak.Text.Count() == 0 || Godzina_roz.Text.Count() == 0)
            {
                MessageBox.Show("Wprowadzono nie mogą być puste", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.AddPatrol(new Patrol { Start_Date = Data_roz.Text , Data_zakonczenia = Data_zak.Text , Start_Hour = Godzina_roz.Text ,
            End_hour= Data_zak.Text, PoliceCarId = ((PoliceCar)RadiowozBox.SelectedItem).PoliceCarId, Policemans = ListViewColumns.SelectedItems.Cast<Policeman>().ToList()
            });
            Data_roz.Text = "";
            Godzina_Zak.Text = "";
            Godzina_roz.Text = "";
            Data_zak.Text = "";
                RefreshData();
            return;
            

        }
        public void RefreshData()
        {
            var policjant = databaseService.GetUserByObj(user.uzytkownik);
            data = databaseService.GetSubordinates(policjant.Policeman);
            radiowozy = databaseService.GetPoliceCars();
            ListViewColumns.ItemsSource = data;
            RadiowozBox.ItemsSource = radiowozy;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
