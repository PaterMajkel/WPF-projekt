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
using Microsoft.Win32;
using System.IO;
using PoliceApp.Modals;

namespace PoliceApp
{
    /// <summary>
    /// Logika interakcji dla klasy Kartoteka.xaml
    /// </summary>
    public partial class RegistryPage : Page
    {
        public DatabaseService databaseService = new();
        public ICollection<Register> data;
        public bool IdOrder = false;
        public string imie;
        public string nazwisko;
        public int wiek;
        public List<Register_Crime> selectedToEdit;
        private byte[] pickedImage;


        public RegistryPage()
        {
            InitializeComponent();
            data = databaseService.GetRegisters();
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
                        data = data.OrderByDescending(id => id.RegisterId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }

                    data = data.OrderBy(id => id.RegisterId).ToList();
                    IdOrder = !IdOrder;
                    break;
                }
                case "Name":
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
                        data = data.OrderByDescending(id => id.Age).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }

                    data = data.OrderBy(id => id.Age).ToList();
                    IdOrder = !IdOrder;
                    break;
                }
            }

            ListViewColumns.ItemsSource = data;
        }

        private void ListViewColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewColumns.SelectedItems.Count == 0)
            {
                Edit.IsEnabled = false;
                Delete.IsEnabled = false;
            }
            else
            {
                Edit.IsEnabled = false;
                Delete.IsEnabled = true;
                if (ListViewColumns.SelectedItems.Count == 1)
                    Edit.IsEnabled = true;
            }
        }

        private void RefreshData()
        {
            data = databaseService.GetRegisters();
            ListViewColumns.ItemsSource = data;
        }

        private void Button_Click_Usun(object sender, RoutedEventArgs e)
        {
            var selected = ListViewColumns.SelectedItems.Cast<Register>().ToList();
            if (selected == null)
            {
                MessageBox.Show("Error during deletion!", "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            databaseService.DeleteRegistries(selected);
            foreach (var element in selected)
                data.Remove(element);
            ListViewColumns.ItemsSource = null;
            ListViewColumns.ItemsSource = data;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement) e.OriginalSource).DataContext as Register;
            if (item != null)
            {
                Window kartotekaOsoba = new RegistryPerson(item);
                kartotekaOsoba.Show();
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            var form = new RegisterWindow()
            {
                Owner = Window.GetWindow(this)
            };
            form.ShowDialog();
            var register = form.register;
            if (register != null)
            {
                databaseService.AddRegistry(register);
                RefreshData();
            }

            return;
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            var register = ListViewColumns.SelectedItems[0] as Register;
            var form = new RegisterWindow(register)
            {
                Owner = Window.GetWindow(this)
            };
            if (form.ShowDialog().GetValueOrDefault(false))
            {
                var newRegister = form.register;
                if (register != null)
                {
                    databaseService.EditRegistry(newRegister);
                    RefreshData();
                }
            }

            return;
        }
    }
}