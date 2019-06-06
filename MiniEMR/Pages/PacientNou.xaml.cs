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
using System.Collections.ObjectModel;

namespace MiniEMR.Pages
{
    /// <summary>
    /// Interaction logic for PacientNou.xaml
    /// </summary>
    public partial class PacientNou : Page
    {
        public String NumarFisaPacientSelectat { set; get; }
        private ObservableCollection<AlergieLV> alergii = new ObservableCollection<AlergieLV>();

        public PacientNou()
        {
            InitializeComponent();
            Loaded += PacientNou_Loaded;
        }

        private void PacientNou_Loaded(object sender, RoutedEventArgs e)
        {
            AlergieListView.ItemsSource = alergii;
            CompletareListView();
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
             
            Pacient PacientNou = new Pacient()
            {
                CNP = CNPTextBox.Text,
                Nume = NumeTextBox.Text,
                Prenume = PrenumeTextBox.Text
            };

            FisaPacient FisaPacientNoua = new FisaPacient()
            {
                IdPacient = PacientNou.IdPacient,
                NumarFisa = NrFisaMedicalaTextBox.Text,
                DataDeschidereFisa = DateTime.Now
            };

            if (NumarFisaPacientSelectat == null)
            {
                App.DB.Pacients.Add(PacientNou);
                App.DB.FisaPacients.Add(FisaPacientNoua);
            }

            int idFisa;
            if(NumarFisaPacientSelectat != null)
                idFisa = App.DB.FisaPacients.Where(z => z.NumarFisa == NumarFisaPacientSelectat).FirstOrDefault().IdFisa;
            else
                idFisa = 0;
            
            // Inainte de a reface legaturile cu alergiile noi selectate trebuie sa le stergem pe cele existente!
            foreach (ListaAlergie alg in App.DB.ListaAlergies.Where(x => x.IdFisa == idFisa).ToList())
            {
                App.DB.ListaAlergies.Remove(alg);
            }

            foreach (AlergieLV item in alergii)
            {
                if (item.Selectat == true)
                {
                    ListaAlergie listaAlergie = new ListaAlergie();
                    if (idFisa == 0)
                    {
                        listaAlergie.CodAlergie = item.CodAlergie;
                        listaAlergie.IdFisa = FisaPacientNoua.IdFisa;
                    }
                    else
                    {
                        listaAlergie.CodAlergie = item.CodAlergie;
                        listaAlergie.IdFisa = idFisa;
                    }
                    App.DB.ListaAlergies.Add(listaAlergie);
                    item.Selectat = false;
                }
            }

            App.DB.SaveChanges();
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            PacientNou pn = new PacientNou();
            mw.MenuFrame.Content = pn;
        }

        private void CompletareListView()
        {
            if (NumarFisaPacientSelectat != null)
            {
                FisaPacient fp = App.DB.FisaPacients.Where(x => x.NumarFisa == NumarFisaPacientSelectat).SingleOrDefault();
                List<ListaAlergie> listaAlergiiPacientSelectat = App.DB.ListaAlergies.Where(x => x.IdFisa == fp.IdFisa).ToList();

                foreach (Alergie alergie in App.DB.Alergies)
                {
                    alergii.Add(
                        new AlergieLV()
                        {
                            CodAlergie = alergie.CodAlergie,
                            DenumireAlergie = alergie.NumeAlergie,
                            Selectat = (listaAlergiiPacientSelectat.Where(x => x.CodAlergie == alergie.CodAlergie).Count() == 1)
                        }
                    );
                }
            }
            else
            {
                // Pacient nou --> initial nu avem selectate alergii
                foreach (Alergie alergie in App.DB.Alergies)
                {
                    alergii.Add(
                        new AlergieLV()
                        {
                            CodAlergie = alergie.CodAlergie,
                            DenumireAlergie = alergie.NumeAlergie,
                            Selectat = false
                        }
                    );
                }
            }
        }
    }
}
