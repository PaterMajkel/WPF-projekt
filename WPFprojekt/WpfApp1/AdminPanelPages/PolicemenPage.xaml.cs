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
    /// Logika interakcji dla klasy Policjanci.xaml
    /// </summary>
    public partial class PolicemenPage : Page
    {
        public DatabaseService databaseService = new();
        public ICollection<User> data;

        public bool IdOrder = false;

        public Rank pickedRanga;
        public PoliceStation pickedKomenda;
        public ICollection<Rank> ranga;
        public ICollection<PoliceStation> komenda;
        public string imie;
        public string nazwisko;
        public string login;
        public string password;
        public bool editMode = false;
        private bool isAdmin = false;
        public User selectedToEdit;

        public PolicemenPage()
        {
            data = databaseService.GetUsers();
            InitializeComponent();
            ListViewColumns.ItemsSource = data;
            ranga = databaseService.GetRanks();
            komenda = databaseService.GetPoliceStations();
            KomendaBox.ItemsSource = komenda;
            RangaBox.ItemsSource = ranga;
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
                            data = data.OrderByDescending(id => id.UserId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.UserId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "FirstName":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Policeman.FirstName).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Policeman.FirstName).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Surname":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Policeman.Surname).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Policeman.Surname).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "Ranga":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Policeman.Ranga.Name).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Policeman.Ranga.Name).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
                case "PoliceStation":
                    {
                        if (!IdOrder)
                        {
                            data = data.OrderByDescending(id => id.Policeman.PoliceStation.PoliceStationId).ToList();
                            IdOrder = !IdOrder;
                            break;
                        }
                        data = data.OrderBy(id => id.Policeman.PoliceStation.PoliceStationId).ToList();
                        IdOrder = !IdOrder;
                        break;
                    }
            }
            ListViewColumns.ItemsSource = data;

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var selected = ListViewColumns.SelectedItems.Cast<User>().ToList();
            if (selected == null)
            {
                MessageBox.Show("Błąd przy usuwaniu!", "Usuń", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            databaseService.DeleteUsers(selected);
            //usuwanie lokalne, aby nie pobierać od nowa informacji
            foreach (var element in selected)
            {
                if(element.Role.ToUpper()=="ADMIN")
                    MessageBox.Show("CZY TY SERIO PRÓBOWAŁEŚ USUNĄĆ ADMINA?!", "Usuń", MessageBoxButton.OK, MessageBoxImage.Error);

                data.Remove(element);
            }
            ListViewColumns.ItemsSource = null;
            ListViewColumns.ItemsSource = data;
        }
    
        private void Button_Click_Edytuj(object sender, RoutedEventArgs e)
        {
            selectedToEdit = (User)ListViewColumns.SelectedItem;
            if (selectedToEdit == null)
            {
                MessageBox.Show("Błąd przy edytowaniu!", "Edytuj", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            editMode = true;
            AddEdit.Content = "Zmień";
            EditButton.IsEnabled = false;
            Abort.Visibility = Visibility.Visible;
            Name.Text = selectedToEdit.Policeman.FirstName;
            Sunrame.Text = selectedToEdit.Policeman.Surname;
            Login.Text = selectedToEdit.Login;
            Password.Text = selectedToEdit.Password;
            CurrentMode.Content = $"Edytuj pozycję";
            RangaBox.SelectedItem = selectedToEdit.Policeman.Ranga;
            KomendaBox.SelectedItem = selectedToEdit.Policeman.PoliceStation;
        }
        private void RefreshData()
        {
            data = databaseService.GetUsers();
            ListViewColumns.ItemsSource = data;
            ranga = databaseService.GetRanks();
            komenda = databaseService.GetPoliceStations();
            KomendaBox.ItemsSource = komenda;
            RangaBox.ItemsSource = ranga;
            isAdmin = false;
            AdminCheckBox.IsChecked = false;
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
            isAdmin = false;

            AdminCheckBox.IsChecked = false;
            Login.Text = "";
            Password.Text = "";
            Name.Text = "";
            Sunrame.Text = "";
            RangaBox.ItemsSource = ranga;
            KomendaBox.ItemsSource = komenda;

        }

        private void Button_Click_DodajOrEdytuj(object sender, RoutedEventArgs e)
        {
            if (!editMode)
            {
                Policeman policjant = new();
                if (!isAdmin && (((PoliceStation)KomendaBox.SelectedItem) == null || ((Rank)RangaBox.SelectedItem) == null))
                {
                    MessageBox.Show("Wprowadzono złe dane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (!isAdmin && (Login.Text.Length == 0 || Password.Text.Length==0 || Name.Text.Length==0 || Sunrame.Text.Length == 0))
                {
                    MessageBox.Show("Wartości nie mogą być puste", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (isAdmin && (Login.Text.Length == 0 || Password.Text.Length == 0))
                {
                    MessageBox.Show("Wartości nie mogą być puste", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
 
                policjant = new Policeman { FirstName = Name.Text, Surname = Sunrame.Text, PoliceStationId = ((PoliceStation)KomendaBox.SelectedItem).PoliceStationId, RangaId = ((Rank)RangaBox.SelectedItem).RangaId};

                databaseService.AddUser(new User { Login = Login.Text, Password = Password.Text, Role = isAdmin ? "Admin" : "" }, policjant);
                RefreshData();
                return;
            }
            if(isAdmin!=true)
            {
                selectedToEdit.Policeman.RangaId = ((Rank)RangaBox.SelectedItem).RangaId;
                selectedToEdit.Policeman.PoliceStationId = ((PoliceStation)KomendaBox.SelectedItem).PoliceStationId;
                selectedToEdit.Policeman.FirstName = Name.Text;
                selectedToEdit.Policeman.Surname = Sunrame.Text;
            }
            selectedToEdit.Login = Login.Text;
            selectedToEdit.Password = Password.Text;


            databaseService.EditUser(selectedToEdit);
            AbortChange();
            RefreshData();

        }

        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            ListViewColumns.ItemsSource = data
               .Where(p => p.UserId.ToString().Contains(FilterId.Text))
               .Where(p => p.Policeman.FirstName.ToUpper().Contains(FilterImie.Text.ToUpper()))
               .Where(p => p.Policeman.Surname.ToUpper().Contains(FilterSurname.Text.ToUpper()))
               .Where(p => p.Policeman.Ranga.Name.ToUpper().Contains(FilterRanga.Text.ToUpper()))
               .Where(p => p.Policeman.PoliceStation.PoliceStationId.ToString().Contains(FilterIdKomendy.Text.ToUpper()))
               .Where(p => p.Policeman.PoliceStation.Address.ToUpper().Contains(FilterAdres.Text.ToUpper()))
               .ToList();
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isAdmin = !isAdmin;

        }
    }
}
