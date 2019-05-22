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

namespace MiniEMR.Pages
{
    /// <summary>
    /// Interaction logic for CazNou.xaml
    /// </summary>
    public partial class CazNou : Page
    {
        public CazNou()
        {
            InitializeComponent();

            Loaded += CazNou_Loaded;
        }

        private void CazNou_Loaded(object sender, RoutedEventArgs e)
        {
            DiagnosticListView.ItemsSource = App.DB.Diagnostics.ToList();
            InvestigatieListView.ItemsSource = App.DB.Investigaties.ToList();
        }
    }
}
