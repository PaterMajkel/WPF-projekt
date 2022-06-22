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
    /// Logika interakcji dla klasy PlanOfSuborned.xaml
    /// </summary>
    public partial class PlanOfSuborned : Window
    {
        public ICollection<Patrol> patrol;
        public Policeman policjant;
        private DatabaseService databaseService = new();

        public PlanOfSuborned(Policeman pato)
        {
            InitializeComponent();
            policjant = databaseService.GetPolicemanByObj(pato);
            Name.Content = policjant.PolicemanId;
            ListViewColumns.ItemsSource = policjant.Patrols;
        }
        private void PrintData()
        {
            FlowDocument fd = new FlowDocument();
            foreach (var item in policjant.Patrols)
            {
                fd.Blocks.Add(new Paragraph(new Run(item.ToString())));
            }
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            fd.PageHeight = pd.PrintableAreaHeight;
            fd.PageWidth = pd.PrintableAreaWidth;

            IDocumentPaginatorSource idocument = fd;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }



        private void PrintButton(object sender, RoutedEventArgs e)
        {
            PrintData();
        }
    }
}
