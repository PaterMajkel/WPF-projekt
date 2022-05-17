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
    /// Logika interakcji dla klasy CrimeSingle.xaml
    /// </summary>
    public partial class CrimeSingle : Window
    {
        private DatabaseService databaseService = new();
        public Crime przestepstwo;
        public Register pickedKartoteka;
        public Policeman pickedPolicjant;
        public ICollection<Register> register;
        public ICollection<Policeman> policjant;
        public bool IdOrder = false;
        public CrimeSingle(Crime przes)
        {
            przestepstwo = databaseService.getPrzestepstwoByObj(przes);
            InitializeComponent();
            register = databaseService.GetRegisters();
            policjant = databaseService.GetPolicemenAndRank();
            Name.Content = przestepstwo.Name;
            Date.Content = przestepstwo.Date;
            Hour.Content = przestepstwo.Hour;

            ListViewColumnsPolicjanci.ItemsSource = przestepstwo.Policemans;
            ListViewColumnsSprawcy.ItemsSource = przestepstwo.Registers;
            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
            KartotekaBox.ItemsSource = register;
            PolicjantBox.ItemsSource = policjant;
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
                            przestepstwo.Registers = przestepstwo.Registers.OrderByDescending(id => id.RegisterId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Registers = przestepstwo.Registers.OrderBy(id => id.RegisterId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "FirstName":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Registers = przestepstwo.Registers.OrderByDescending(id => id.FirstName).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Registers = przestepstwo.Registers.OrderBy(id => id.FirstName).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Surname":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Registers = przestepstwo.Registers.OrderByDescending(id => id.Surname).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Registers = przestepstwo.Registers.OrderBy(id => id.Surname).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Age":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Registers = przestepstwo.Registers.OrderByDescending(id => id.Age).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        register = register.OrderBy(id => id.Age).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "ID.":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Policemans = przestepstwo.Policemans.OrderByDescending(id => id.PolicemanId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Policemans = przestepstwo.Policemans.OrderBy(id => id.PolicemanId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "FirstName.":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Policemans = przestepstwo.Policemans.OrderByDescending(id => id.FirstName).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Policemans = przestepstwo.Policemans.OrderBy(id => id.FirstName).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Surname.":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Policemans = przestepstwo.Policemans.OrderByDescending(id => id.Surname).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Policemans = przestepstwo.Policemans.OrderBy(id => id.Surname).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "PoliceStationId.":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Policemans = przestepstwo.Policemans.OrderByDescending(id => id.PoliceStationId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Policemans = przestepstwo.Policemans.OrderBy(id => id.PoliceStationId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Ranga.":
                    {
                        if (!IdOrder)
                        {
                            przestepstwo.Policemans = przestepstwo.Policemans.OrderByDescending(id => id.RangaId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        przestepstwo.Policemans = przestepstwo.Policemans.OrderBy(id => id.RangaId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
            }
            ListViewColumnsSprawcy.ItemsSource = przestepstwo.Registers;
            ListViewColumnsPolicjanci.ItemsSource = przestepstwo.Policemans;

        }

        private void AddSprawca_Click(object sender, RoutedEventArgs e)
        {
            pickedKartoteka = (Register)KartotekaBox.SelectedItem;
            if (pickedKartoteka != null)
            {
                if (przestepstwo.Registers.Contains(pickedKartoteka))
                {
                    MessageBox.Show("Dana osoba nie może uczystniczyć w jednym wydarzeniu kilkukrotnie", "Co Ty wyprawiasz?", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                databaseService.AddKartotekaToPrzestepstwo(przestepstwo, pickedKartoteka);
            }
            Refresh();
        }
        private void Refresh()
        {
            przestepstwo = databaseService.getPrzestepstwoByObj(przestepstwo);
            register = databaseService.GetRegisters();
            policjant = databaseService.GetPolicemenAndRank();

            ListViewColumnsPolicjanci.ItemsSource = null;
            ListViewColumnsSprawcy.ItemsSource = null;

            ListViewColumnsPolicjanci.ItemsSource = przestepstwo.Policemans;
            ListViewColumnsSprawcy.ItemsSource = przestepstwo.Registers;

            KartotekaBox.ItemsSource = register;
            PolicjantBox.ItemsSource = policjant;
        }

        private void Add_Policjant_Click(object sender, RoutedEventArgs e)
        {
            pickedPolicjant = (Policeman)PolicjantBox.SelectedItem;
            if (pickedPolicjant != null)
            {
                if (przestepstwo.Policemans.Contains(pickedPolicjant))
                {
                    MessageBox.Show("Dana osoba nie może uczystniczyć w jednym wydarzeniu kilkukrotnie", "Co Ty wyprawiasz?", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                databaseService.AddPolicjatToPrzestepstwo(przestepstwo, pickedPolicjant);
            }
            Refresh();
        }
        private void Delete_Policjant_Click(object sender, RoutedEventArgs e)
        {
            var pickedPolicjants = ListViewColumnsPolicjanci.SelectedItems.Cast<Policeman>().ToList();
            if (pickedPolicjants == null)
            {
                MessageBox.Show("Błąd przy usuwaniu!", "Usuń", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.DeletePolicjantsFromPrzestepstwo(przestepstwo, pickedPolicjants);
            //usuwanie lokalne, aby nie pobierać od nowa informacji
            Refresh();

        }
        private void Delete_Sprawca_Click(object sender, RoutedEventArgs e)
        {
            var pickedSprawcy = ListViewColumnsSprawcy.SelectedItems.Cast<Register>().ToList();
            if (pickedSprawcy == null)
            {
                MessageBox.Show("Błąd przy usuwaniu!", "Usuń", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.DeleteSprawcaFromPrzestepstwo(przestepstwo, pickedSprawcy);
            //usuwanie lokalne, aby nie pobierać od nowa informacji
            Refresh();

        }
    }

}

