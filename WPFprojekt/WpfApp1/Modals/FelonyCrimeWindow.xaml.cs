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

namespace PoliceApp.Modals
{
    /// <summary>
    /// Interaction logic for FelonyWindow.xaml
    /// </summary>
    public partial class FelonyCrimeWindow : Window
    {
        public Felony felony;
        public Crime crime;
        public FelonyCrimeWindow()
        {
            InitializeComponent();
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
          
            if (Name.Text.Length == 0 || Hour.Text.Length == 0 || Date.Text.Length == 0)
            {
                MessageBox.Show("Values must not be empty", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            felony = new Felony()
            {
                Name = Name.Text,
                Date = Date.Text,
                Hour = Hour.Text
            };
            crime = new Crime()
            {
                Name = Name.Text,
                Date = Date.Text,
                Hour = Hour.Text
            };
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}