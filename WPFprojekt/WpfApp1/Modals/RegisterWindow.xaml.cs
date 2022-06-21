using System;
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
using System.Windows.Shapes;
using EntityFramework.Models;
using Microsoft.Win32;

namespace PoliceApp.Modals
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public Register register;
        private byte[] picture;


        public RegisterWindow()
        {
            InitializeComponent();
        }

        public RegisterWindow(Register register)
        {
            this.register = register;
            InitializeComponent();
            Name.Text = this.register.FirstName;
            Surname.Text = this.register.Surname;
            Age.Text = this.register.Age.ToString();
            this.picture = this.register.Picture;
            this.register.RegisterId = register.RegisterId;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length == 0 || Surname.Text.Length == 0 || Age.Text.Length == 0)
            {
                MessageBox.Show("Brakujące dane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int age = 0;
            if (!int.TryParse(Age.Text, out age))
            {
                MessageBox.Show("Podany wiek nie jest liczbą", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (age <= 17 || age >= 100)
            {
                MessageBox.Show("Wiek nie może być mniejszy niż 17 i większy od 100 ", "Błąd", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            register = new Register
            {
                RegisterId = register != null ? register.RegisterId : 0,
                FirstName = Name.Text,
                Surname = Surname.Text,
                Age = age,
                Picture = picture
            };
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) |*.jpg; *.jpeg; *.png";
            //fileDialog.DefaultExt = ".png | *.jpg";
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);
                picture = image;
                return;
            }

            MessageBox.Show("Nie udało się pobrać pliku", "bruh", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}