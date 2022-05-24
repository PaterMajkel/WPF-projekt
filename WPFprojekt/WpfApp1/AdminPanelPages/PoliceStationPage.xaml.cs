using EntityFramework.DTO;
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
    /// Logika interakcji dla klasy PoliceStationPage.xaml
    /// </summary>
    public partial class PoliceStationPage : Page
    {
        public DatabaseService databaseService = new();
        public ICollection<PoliceStation> data;
        public bool IdOrder = false;
        public City pickedMiasto;
        public Region_City pickedRegion;
        public ICollection<City> miasta;
        public ICollection<Region_City> regiony;
        public string adres;
        public bool editMode = false;
        public PoliceStation selectedToEdit;
        public PoliceStationPage()
        {
            InitializeComponent();
            data = databaseService.GetPoliceStations();
            miasta = databaseService.GetCities();
             AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
            //foreach (var x in data)
            //{
            //    ListViewColumns.Items.Add(x);
            //}
            // skalowanie z ilością danych
            //List<PoliceStation_City_Region> data2=data.ToList();
            //List<PoliceStation_City_Region> data3 = data.ToList();
            //
            //for (int i=0; i<20; i++)
            //{
            //    data2.Add(data3[i%9]);
            //}
            ListViewColumns.ItemsSource = data;
            MiastoBox.ItemsSource = miasta;
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
                            data = data.OrderByDescending(id => id.PoliceStationId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.PoliceStationId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Address":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Address).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Address).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Region":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Region_City.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Region_City.Name).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "City":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Region_City.City.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Region_City.City.Name).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Stopien Zagrozenia":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Region_City.DangerStage).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Region_City.DangerStage).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
            }
            ListViewColumns.ItemsSource = data;

        }

        private void Button_Click_Usun(object sender, RoutedEventArgs e)
        {
            var selected = ListViewColumns.SelectedItems.Cast<PoliceStation>().ToList();
            if (selected == null)
            {
                MessageBox.Show("Błąd przy usuwaniu!", "Usuń", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.DeletePoliceStations(selected);
            //usuwanie lokalne, aby nie pobierać od nowa informacji
            foreach(var element in selected)
            {
                data.Remove(element);
            }
            ListViewColumns.ItemsSource = null;
            ListViewColumns.ItemsSource = data;
        }
        private void Button_Click_Edytuj(object sender, RoutedEventArgs e)
        {
            selectedToEdit = (PoliceStation)ListViewColumns.SelectedItem;
            if (selectedToEdit == null)
            {
                MessageBox.Show("Błąd przy edytowaniu!", "Edytuj", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            editMode = true;
            AddEdit.Content = "Zmień";
            EditButton.IsEnabled = false;
            Abort.Visibility = Visibility.Visible;
            Address.Text = selectedToEdit.Address;

            CurrentMode.Content = $"Edytuj pozycję";
            MiastoBox.SelectedItem = selectedToEdit.Region_City.City;
            RegionBox.SelectedItem = selectedToEdit.Region_City;

        }

        private void ComboBoxMiasto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pickedMiasto= (City)MiastoBox.SelectedItem;
            regiony = databaseService.GetRegionsOfCity(pickedMiasto);
            RegionBox.ItemsSource = regiony;
        }

        private void ComboBoxRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pickedRegion = (Region_City)RegionBox.SelectedItem;

        }

        private void Button_Click_DodajOrEdytuj(object sender, RoutedEventArgs e)
        {
            if (!editMode)
            {
                if (pickedMiasto == null || pickedRegion == null || adres==null)
                {
                    MessageBox.Show("Wprowadzono złe dane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (adres.Length == 0)
                {
                    MessageBox.Show("Address nie może być pusty", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                databaseService.AddPoliceStation(new PoliceStation {Address = adres, Region_CityId = pickedRegion.Region_CityId, Region_City = pickedRegion });
                RefreshData();
                return;
            }
            selectedToEdit.Address = Address.Text;
            selectedToEdit.Region_CityId = ((Region_City)RegionBox.SelectedItem).Region_CityId;
            databaseService.EditPoliceStations(selectedToEdit);
            AbortChange();
            RefreshData();

        }
        private void Adres_TextChanged(object sender, TextChangedEventArgs e)
        {
            adres = Address.Text.ToString();
        }
        private void RefreshData()
        {
            data = databaseService.GetPoliceStations();
            miasta = databaseService.GetCities();
            ListViewColumns.ItemsSource = data;
            MiastoBox.ItemsSource = miasta;
        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void Button_Click_Abort(object sender, RoutedEventArgs e)
        {
            AbortChange();
        }
        public void AbortChange()
        {
            editMode = false;
            AddEdit.Content = "Dodaj";
            Abort.Visibility = Visibility.Hidden;
            CurrentMode.Content = $"Nowa pozycja";
            EditButton.IsEnabled = true;

            Address.Text = "";
            MiastoBox.ItemsSource = miasta;

        }

        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            ListViewColumns.ItemsSource = data
                .Where(p => p.PoliceStationId.ToString().Contains(FilterId.Text))
                .Where(p => p.Address.ToUpper().Contains(FilterAdres.Text.ToUpper()))
                .Where(p => p.Region_City.Name.ToUpper().Contains(FilterRegion.Text.ToUpper()))
                .Where(p => p.Region_City.City.Name.ToUpper().Contains(FilterMiasto.Text.ToUpper()))
                .Where(p => p.Region_City.DangerStage.ToUpper().Contains(FilterStopien.Text.ToUpper()))
                .ToList();
        }
    }
}
