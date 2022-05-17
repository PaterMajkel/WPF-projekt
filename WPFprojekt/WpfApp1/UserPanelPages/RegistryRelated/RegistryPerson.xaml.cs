using EntityFramework.Models;
using EntityFramework.Services;
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

namespace PoliceApp
{
    /// <summary>
    /// Logika interakcji dla klasy RegistryPerson.xaml
    /// </summary>
    public partial class RegistryPerson : Window
    {
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public Register register;
        private DatabaseService databaseService = new();
        public RegistryPerson(Register osoba)
        {
            register = databaseService.getKartotekaByObj(osoba);
            //Console.WriteLine(register.Surname);
            InitializeComponent();
            FirstName.Content = register.FirstName;
            Surname.Content = register.Surname;
            Age.Content = register.Age.ToString();
            ListViewColumnsPrzestepstwa.ItemsSource = register.Crimes;
            ListViewColumnsWykroczenia.ItemsSource = register.Felonys;
            Image.Source = LoadImage(register.Picture);
        }

    }
}
