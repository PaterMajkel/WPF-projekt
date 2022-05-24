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
    /// Logika interakcji dla klasy RegionPanel.xaml
    /// </summary>
    public partial class RegionPanel : Page
    {
        public bool IdOrder = false;
        public ICollection<Region_City> data;
        private DatabaseService databaseService = new();
        private string[] levels = { "Niski", "Średni", "Wysoki", "Śmiertelny" };
        private bool editMode = false;
        private Region_City selectedToEdit;
        public RegionPanel()
        {
            data = databaseService.GetRegions();
            InitializeComponent();
            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
            ListViewColumns.ItemsSource = data;
            EditBox.ItemsSource = levels;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            selectedToEdit.DangerStage = EditBox.SelectedItem.ToString() ;
            databaseService.EditRegion(selectedToEdit);
            RefreshData();
        }

        private void ListView_OnColumnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType().Name != "GridViewColumnHeader")
                return;
            string headerName=(e.OriginalSource as GridViewColumnHeader).Content.ToString();
            switch(headerName)
            {
                case "ID":
                    {
                        if(!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Region_CityId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Region_CityId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "City":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.City.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.City.Name).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Name":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Name).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Stopień zagrożenia":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.DangerStage).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.DangerStage).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
            }
            ListViewColumns.ItemsSource = data;
        }

        private void ListViewColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void RefreshData()
        {
            data = databaseService.GetRegions();
            ListViewColumns.ItemsSource = data;
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            editMode = true;
            EditBox.SelectedItem = ((Region_City)ListViewColumns.SelectedItem).DangerStage;
            selectedToEdit = (Region_City)ListViewColumns.SelectedItem;

        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
