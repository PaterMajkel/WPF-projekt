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
    /// Logika interakcji dla klasy FelonySingle.xaml
    /// </summary>
    public partial class FelonySingle : Window
    {
        public Felony wykroczenia;
        private DatabaseService databaseService = new();
        public Register pickedKartoteka;
        public Policeman pickedPolicjant;
        public ICollection<Register> register;
        public ICollection<Policeman> policjant;
        public bool IdOrder = false;
        public Felony wykroczeniepom;
        public FelonySingle(Felony wykro)
        {
            wykroczenia = databaseService.GetFelonyByObj(wykro);
            register = databaseService.GetRegisters();
            policjant = databaseService.GetPolicemenAndRank();
            InitializeComponent();
            Name.Content = wykroczenia.Name;
            Date.Content = wykroczenia.Date;
            Hour.Content = wykroczenia.Hour;


            ListViewColumnsPolicjanci.ItemsSource = wykroczenia.Policemans;
            ListViewColumnsSprawcy.ItemsSource = wykroczenia.Registers;
            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
            KartotekaBox.ItemsSource = register;
            PolicjantBox.ItemsSource= policjant;
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
                            wykroczenia.Registers = wykroczenia.Registers.OrderByDescending(id => id.RegisterId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Registers = wykroczenia.Registers.OrderBy(id => id.RegisterId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "FirstName":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Registers = wykroczenia.Registers.OrderByDescending(id => id.FirstName).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Registers = wykroczenia.Registers.OrderBy(id => id.FirstName).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Surname":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Registers = wykroczenia.Registers.OrderByDescending(id => id.Surname).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Registers = wykroczenia.Registers.OrderBy(id => id.Surname).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Age":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Registers = wykroczenia.Registers.OrderByDescending(id => id.Age).ToList();
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
                            wykroczenia.Policemans = wykroczenia.Policemans.OrderByDescending(id => id.PolicemanId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Policemans = wykroczenia.Policemans.OrderBy(id => id.PolicemanId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "FirstName.":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Policemans = wykroczenia.Policemans.OrderByDescending(id => id.FirstName).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Policemans = wykroczenia.Policemans.OrderBy(id => id.FirstName).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Surname.":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Policemans = wykroczenia.Policemans.OrderByDescending(id => id.Surname).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Policemans = wykroczenia.Policemans.OrderBy(id => id.Surname).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "PoliceStation ID.":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Policemans = wykroczenia.Policemans.OrderByDescending(id => id.PoliceStationId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Policemans = wykroczenia.Policemans.OrderBy(id => id.PoliceStationId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Rank.":
                    {
                        if (!IdOrder)
                        {
                            wykroczenia.Policemans = wykroczenia.Policemans.OrderByDescending(id => id.RankId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        wykroczenia.Policemans = wykroczenia.Policemans.OrderBy(id => id.RankId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
            }
            ListViewColumnsSprawcy.ItemsSource = wykroczenia.Registers;
            ListViewColumnsPolicjanci.ItemsSource = wykroczenia.Policemans;

        }
        private void Refresh()
        {
            wykroczenia = databaseService.GetFelonyByObj(wykroczenia);
            register = databaseService.GetRegisters();
            policjant = databaseService.GetPolicemenAndRank();

            ListViewColumnsPolicjanci.ItemsSource = null;
            ListViewColumnsSprawcy.ItemsSource = null;

            ListViewColumnsPolicjanci.ItemsSource = wykroczenia.Policemans;
            ListViewColumnsSprawcy.ItemsSource = wykroczenia.Registers;

            KartotekaBox.ItemsSource = register;
            PolicjantBox.ItemsSource = policjant;
        }

        private void Add_Policjant_Click(object sender, RoutedEventArgs e)
        {
            pickedPolicjant = (Policeman)PolicjantBox.SelectedItem;
            if (pickedPolicjant != null)
            {
                if (wykroczenia.Policemans.Contains(pickedPolicjant))
                {
                    MessageBox.Show("Dana osoba nie może uczystniczyć w jednym wydarzeniu kilkukrotnie", "Co Ty wyprawiasz?", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                databaseService.AddPolicemanToFelony(wykroczenia, pickedPolicjant);
            }
            Refresh();
        }

        private void AddSprawca_Click(object sender, RoutedEventArgs e)
        {
            pickedKartoteka = (Register)KartotekaBox.SelectedItem;
            if (pickedKartoteka != null)
            {
                if (wykroczenia.Registers.Contains(pickedKartoteka))
                {
                    MessageBox.Show("Dana osoba nie może uczystniczyć w jednym wydarzeniu kilkukrotnie", "Co Ty wyprawiasz?", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                databaseService.AddRegistryToFelony(wykroczenia, pickedKartoteka);
            }
            Refresh();
        }
        private void Delete_Policjant_Click(object sender, RoutedEventArgs e)
        {
            var pickedPolicjants = ListViewColumnsPolicjanci.SelectedItems.Cast<Policeman>().ToList();
            if (pickedPolicjants == null)
            {
                MessageBox.Show("Error during deletion!", "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.DeletePolicemenFromFelony(wykroczenia, pickedPolicjants);
            //usuwanie lokalne, aby nie pobierać od nowa informacji
            Refresh();

        }
        private void Delete_Sprawca_Click(object sender, RoutedEventArgs e)
        {
            var pickedSprawcy = ListViewColumnsSprawcy.SelectedItems.Cast<Register>().ToList();
            if (pickedSprawcy == null)
            {
                MessageBox.Show("Error during deletion!", "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.DeleteFelonFromFelony(wykroczenia, pickedSprawcy);
            //usuwanie lokalne, aby nie pobierać od nowa informacji
            Refresh();

        }
    }
}

