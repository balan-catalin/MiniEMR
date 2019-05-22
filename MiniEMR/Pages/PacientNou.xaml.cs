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
    /// Interaction logic for PacientNou.xaml
    /// </summary>
    public partial class PacientNou : Page
    {
        public PacientNou()
        {
            InitializeComponent();
            Loaded += PacientNou_Loaded;
        }

        private void PacientNou_Loaded(object sender, RoutedEventArgs e)
        {
            AlergieListView.ItemsSource = App.DB.Alergies.ToList();
        }

        private void NrFisaMedicalaTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<FisaPacient> list = App.DB.FisaPacients.ToList();
            FisaPacient el = list.LastOrDefault();
            int nr = Int32.Parse(el.NumarFisa.Substring(3, 3)) + 1;
            if(nr<10)
                NrFisaMedicalaTextBox.Text = "FIS00" + nr.ToString();
            else if(nr<100)
                NrFisaMedicalaTextBox.Text = "FIS0" + nr.ToString();
            else
                NrFisaMedicalaTextBox.Text = "FIS" + nr.ToString();
        }

        private void SavePatientButton_Click(object sender, RoutedEventArgs e)
        {
            Pacient newPacient = new Pacient()
            {
                CNP = CNPTextBox.Text,
                Nume = NumeTextBox.Text,
                Prenume = NumeTextBox.Text
            };
            FisaPacient newFisaPacient = new FisaPacient()
            {
                IdPacient = newPacient.IdPacient,
                NumarFisa = NrFisaMedicalaTextBox.Text,
                DataDeschidereFisa = DateTime.Now
            };

            List<Alergie> listaAlergiiSelectate = new List<Alergie>();
            foreach(Alergie item in AlergieListView.SelectedItems)
            {
                
            }
        }
    }
}
