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
using PoliceApp.Modals;

namespace PoliceApp
{
    /// <summary>
    /// Logika interakcji dla klasy KartotekaZdarzenie.xaml
    /// </summary>
    public partial class RegistryFelony : Page
    {
        public DatabaseService databaseService = new();
        public ICollection<Felony> data;
        public bool IdOrder = false;

        public RegistryFelony()
        {
            InitializeComponent();
            data = databaseService.GetFelonys();
            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
            ListViewColumns.ItemsSource = data;
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
                        data = data.OrderByDescending(id => id.FelonyId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }

                    data = data.OrderBy(id => id.FelonyId).ToList();
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
                case "Date":
                {
                    if (!IdOrder)
                    {
                        data = data.OrderByDescending(id => id.Date).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }

                    data = data.OrderBy(id => id.Date).ToList();
                    IdOrder = !IdOrder;
                    break;
                }
                case "Hour":
                {
                    if (!IdOrder)
                    {
                        data = data.OrderByDescending(id => id.Hour).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }

                    data = data.OrderBy(id => id.Hour).ToList();
                    IdOrder = !IdOrder;
                    break;
                }
            }

            ListViewColumns.ItemsSource = data;
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            FelonyCrimeWindow form = new()
            {
                Owner = Window.GetWindow(this)
            };
            form.ShowDialog();
            var newFelony = form.felony;
            if (newFelony != null)
            {
                databaseService.AddFelony(newFelony);
                Refresh();
            }
        }

        private void Refresh()
        {
            data = databaseService.GetFelonys();
            ListViewColumns.ItemsSource = data;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement) e.OriginalSource).DataContext as Felony;
            if (item != null)
            {
                Window wykroczeniesingle = new FelonySingle(item);
                wykroczeniesingle.Show();
            }
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            var selected = ListViewColumns.SelectedItems.Cast<Felony>().ToList();
            if (selected == null)
            {
                MessageBox.Show("Error during deletion!", "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            databaseService.DeleteFelonies(selected);
            foreach (var element in selected)
                data.Remove(element);
            ListViewColumns.ItemsSource = null;
            ListViewColumns.ItemsSource = data;
        }

        private void ListViewColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove.IsEnabled = ListViewColumns.SelectedItems.Count != 0;
        }
        private void PrintData()
        {
            FlowDocument fd = new FlowDocument();
            foreach (var item in data)
            {
                fd.Blocks.Add(new Paragraph(new Run(item.ToString())));
            }
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            fd.PageHeight = pd.PrintableAreaHeight;
            fd.PageWidth = pd.PrintableAreaWidth;

            IDocumentPaginatorSource idocument = fd;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }



        private void PrintButton(object sender, RoutedEventArgs e)
        {
            PrintData();
        }
    }
}