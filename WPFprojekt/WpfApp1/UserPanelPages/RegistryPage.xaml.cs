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
            string headerName=(e.OriginalSource as GridViewColumnHeader).Content.ToString();
            switch(headerName)
            {
                case "ID":
                    {
                        if(!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.RegisterId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.RegisterId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Imię":
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

        }
        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            if (imie == null || nazwisko == null || wiek == 0 || pickedImage == null)
            {
                MessageBox.Show("Wprowadzono złe dane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(wiek<=17 || wiek>=100)
            {
                MessageBox.Show("Age nie może być mniejszy niż 17 i większy od 100 ", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            databaseService.AddRegistry(new Register {FirstName=imie,Surname=nazwisko,Age=wiek,Picture=pickedImage});
            RefreshData();
            pickedImage = null;
        }
        private void RefreshData()
        {
            data = databaseService.GetRegisters();
            ListViewColumns.ItemsSource = data;
        }
        private void Imie_TextChanged(object sender, TextChangedEventArgs e)
        {
            imie = FirstName.Text.ToString();
        }
        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            nazwisko = Surname.Text.ToString();
        }
        private void Wiek_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pom=Age.Text.ToString();
            wiek = int.Parse(pom);
        }
        private void Button_Click_Usun(object sender, RoutedEventArgs e)
        {
            var selected = ListViewColumns.SelectedItems.Cast<Register>().ToList();
            if (selected == null)
            {
                MessageBox.Show("Błąd przy usuwaniu!", "Usuń", MessageBoxButton.OK, MessageBoxImage.Error);
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
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Register;
            if (item != null)
            {
                Window kartotekaOsoba = new RegistryPerson(item);
                kartotekaOsoba.Show();
            }
        }

        private void Button_Click_AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog  = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) |*.jpg; *.jpeg; *.png";
            //fileDialog.DefaultExt = ".png | *.jpg";
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                pickedImage = File.ReadAllBytes(fileDialog.FileName);
                    return;
            }
            MessageBox.Show("Nie udało się pobrać pliku", "bruh", MessageBoxButton.OK,MessageBoxImage.Error);

        }
    }
}
