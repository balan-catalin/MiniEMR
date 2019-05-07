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
                    ShowGridByTag(1);
                    break;
                case "ItemNewPatient":
                    ShowGridByTag(2);
                    break;
                case "ItemNewCase":
                    ShowGridByTag(3);
                    break;
                case "ItemVerificareAsigurat":
                    ShowGridByTag(4);
                    NumarConcediiMedicaleStackPanel.Visibility = Visibility.Hidden;
                    AsiguratStackPanel.Visibility = Visibility.Visible;
                    break;
                case "ItemNumarConcediiMedicale":
                    ShowGridByTag(4);
                    NumarConcediiMedicaleStackPanel.Visibility = Visibility.Visible;
                    AsiguratStackPanel.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }

        protected void ShowGridByTag(int tag)
        {
            foreach (Grid gr in MainGrid.Children.OfType<Grid>())
            {
                if(gr.Tag != null)
                    gr.Visibility = gr.Tag.Equals(tag.ToString())? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void CNPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int year = 0;
            int month = 0;
            int day = 0;
            if (CNPTextBox.Text.Length == 13)
            {
                String cnp = CNPTextBox.Text;
                if (cnp.Substring(0, 1) == "1" || cnp.Substring(0, 1) == "2")
                {
                    year = Convert.ToInt32("19" + cnp.Substring(1, 2));
                    month = Convert.ToInt32(cnp.Substring(3, 2));
                    day = Convert.ToInt32(cnp.Substring(5, 2));
                }
                else
                {
                    year = Convert.ToInt32("20" + cnp.Substring(1, 2));
                    month = Convert.ToInt32(cnp.Substring(3, 2));
                    day = Convert.ToInt32(cnp.Substring(5, 2));
                }
                DateTime dateOfBirth = new DateTime(year, month, day);
                DataNasteriiTextBlock.Text = dateOfBirth.ToString("dd/MM/yyyy");
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > today.AddYears(-age)) age--;
                VarstaTextBlock.Text = age.ToString();
            }
        }


    }
}
