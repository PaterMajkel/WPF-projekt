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
    public partial class RegistryCrime : Page
    {
        public DatabaseService databaseService = new();
        public ICollection<Crime> data;
        public bool IdOrder = false;

        //public DatePicker data;

        public RegistryCrime()
        {
            InitializeComponent();
            data = databaseService.GetCrimes();
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
                        data = data.OrderByDescending(id => id.PrzestepstwoId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }

                    data = data.OrderBy(id => id.PrzestepstwoId).ToList();
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
            var newCrime = form.crime;
            if (newCrime != null)
            {
                databaseService.AddCrime(newCrime);
                Refresh();
            }

            return;
        }

        private void Refresh()
        {
            data = databaseService.GetCrimes();
            ListViewColumns.ItemsSource = data;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var selected = ListViewColumns.SelectedItems.Cast<Crime>().ToList();
            if (selected == null)
            {
                MessageBox.Show("Error during deletion!", "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            databaseService.DeleteCrimes(selected);
            foreach (var element in selected)
                data.Remove(element);
            ListViewColumns.ItemsSource = null;
            ListViewColumns.ItemsSource = data;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement) e.OriginalSource).DataContext as Crime;
            if (item != null)
            {
                Window przestepstwo = new CrimeSingle(item);
                przestepstwo.Show();
            }
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