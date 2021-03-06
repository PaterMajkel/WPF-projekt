using EntityFramework.Models;
using EntityFramework.Services;
using PoliceApp.Modals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

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
                            data = data.OrderByDescending(id => id.Rank.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }

                        data = data.OrderBy(id => id.Rank.Name).ToList();
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
            if (ListViewColumns.SelectedItems.Count == 0)
            {
                MessageBox.Show("You didn't choose anybody", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SubornedPlanWindow form = new(ListViewColumns.SelectedItems.Cast<Policeman>().ToList())
            {
                Owner = Window.GetWindow(this)
            };
            form.ShowDialog();
            var newPatrol = form.patrol;
            if (newPatrol != null)
            {
                databaseService.AddPatrol(newPatrol);
                RefreshData();
            }
            return;
        }

        public void RefreshData()
        {
            var policjant = databaseService.GetUserByObj(user.uzytkownik);
            data = databaseService.GetSubordinates(policjant.Policeman);
            radiowozy = databaseService.GetPoliceCars();
            ListViewColumns.ItemsSource = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void PrintData()
        {
            
            FlowDocument fd = new FlowDocument();
            foreach (var item in this.data)
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