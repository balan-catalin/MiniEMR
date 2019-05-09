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
using MiniEMR.Pages;

namespace MiniEMR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            hName.Text = Application.Current.FindResource("hospitalName").ToString();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem itemSelected = (ListViewItem)((ListView)sender).SelectedItem;

            switch (itemSelected.Name)
            {
                case "ItemHome":

                    break;
                case "ItemNewPatient":
                    MenuFrame.Content = new PacientNou();
                    break;
                case "ItemNewCase":
                    MenuFrame.Content = new CazNou();
                    break;
                case "ItemVerificareAsigurat":
                    MenuFrame.Content = new VerificareAsigurat();
                    break;
                case "ItemNumarConcediiMedicale":
                    MenuFrame.Content = new NumarConcediiMedicale();
                    break;
                default:
                    break;
            }
        }

    }
}
