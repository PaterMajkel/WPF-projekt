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
using EntityFramework.Models;
using EntityFramework.Services;

namespace PoliceApp.Modals
{
    /// <summary>
    /// Interaction logic for SubornedPlanWindow.xaml
    /// </summary>
    public partial class SubornedPlanWindow : Window
    {
        private List<Policeman> policemen;
        private ICollection<PoliceCar> radiowozy;

        private DatabaseService databaseService = new();
        private SharedData user = SharedData.GetInstance(null);


        public SubornedPlanWindow(List<Policeman> policemen)
        {
            this.policemen = policemen;
            this.radiowozy = databaseService.GetPoliceCars();
            InitializeComponent();
            RadiowozBox.ItemsSource = radiowozy;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Data_roz.Text.Length == 0 || Godzina_zak.Text.Length == 0 || Data_zak.Text.Length == 0 ||
                Godzina_roz.Text.Length == 0 || RadiowozBox.SelectedItem == null)
            {
                MessageBox.Show("Wprowadzono nie mogą być puste", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            databaseService.AddPatrol(new Patrol
            {
                Start_Date = Data_roz.Text,
                Data_zakonczenia = Data_zak.Text,
                Start_Hour = Godzina_roz.Text,
                End_hour = Data_zak.Text,
                PoliceCarId = ((PoliceCar) RadiowozBox.SelectedItem).PoliceCarId,
                Policemans = policemen
            });
            Close();
        }
    }
}