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
using System.Windows.Shapes;

namespace PoliceApp
{
    /// <summary>
    /// Logika interakcji dla klasy PlanCheckPage.xaml
    /// </summary>
    public partial class PlanCheckPage : Page
    {
        private SharedData singleton = SharedData.GetInstance(null);
        private User uzytkownik;
        private DatabaseService databaseService = new();
        private bool IdOrder = false;
        public PlanCheckPage()
        {
            uzytkownik = databaseService.GetUserByObj(singleton.uzytkownik);

            InitializeComponent();
            ListViewColumns.ItemsSource = uzytkownik.Policeman.Patrols;
            //AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));

        }
        
    }
}
