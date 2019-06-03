using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Caz CazSelectat { set; get; }
        public List<ListaAlergie> ListaAlergiiPacientSelectat { set; get; }
        ObservableCollection<ObservatieLV> listaObservatii = new ObservableCollection<ObservatieLV>();

        ObservableCollection<DiagnosticLV> listaDiagnostice = new ObservableCollection<DiagnosticLV>();
        ObservableCollection<DiagnosticLV> copieListaDiagnostice = new ObservableCollection<DiagnosticLV>();

        ObservableCollection<InvestigatieLV> listaInvestigatii = new ObservableCollection<InvestigatieLV>();
        List<InvestigatieLV> copieListaInvestigatii = new List<InvestigatieLV>();

        public CazNou()
        {
            InitializeComponent();
            Loaded += CazNou_Loaded;
        }

        private void CazNou_Loaded(object sender, RoutedEventArgs e)
        {
            //Completare antet
            CompletareAlergii();
            CompletareDiagnostice();
            CompletareInvestigatii();
            if (CazSelectat != null)
            {
                NumarCazTB.Text = CazSelectat.NumarCaz;
            }

            //declarare itemsSourc
            DiagnosticPrincipalCB.ItemsSource = App.DB.Diagnostics.ToList();
            DiagnosticListView.ItemsSource = listaDiagnostice;
            InvestigatieListView.ItemsSource = listaInvestigatii;
            ServiciuMedicalListView.ItemsSource = App.DB.ServiciuMedicals.ToList();
            ObservatieListView.ItemsSource = listaObservatii;
        }

        private void ButonSaveObservatie_Click(object sender, RoutedEventArgs e)
        {
            listaObservatii.Add(new ObservatieLV() { DataObservatie = DateTime.Now, TextObservatie = ObservatieTextBox.Text });
            ObservatieTextBox.Clear();
        }

        private void NumarCazTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<Caz> list = App.DB.Cazs.ToList();
            Caz el = list.LastOrDefault();
            int nr = Int32.Parse(el.NumarCaz.Substring(4)) + 1;
            NumarCazTB.Text = "CAZ-" + nr;
        }

        //Adaugarea unui caz nou
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //adaugare
            FisaPacient fp = App.DB.FisaPacients.Where(x => x.NumarFisa == NumarFisaTB.Text).FirstOrDefault();

            Caz caz = new Caz();
            if (CazSelectat == null) {
                caz.DataDeschidereCaz = DateTime.Now;
                caz.NumarCaz = NumarCazTB.ToString();
                caz.IdFisa = fp.IdFisa;
                caz.IdPersonalMedical = 3;
                caz.CodDiagnosticPrincipal = null;
                App.DB.Cazs.Add(caz);
            }
            else
                caz = CazSelectat;

            for (int i = 0; i < copieListaDiagnostice.ToArray().Length; i++)
            {
                if (!copieListaDiagnostice.ToArray()[i].Equals(listaDiagnostice.ToArray()[i]))
                {
                    ListaDiagnostice itemToAdd = new ListaDiagnostice()
                    {
                        CodDiagnostic = listaDiagnostice.ToArray()[i].CodDiagnostic,
                        IdCaz = caz.IdCaz,
                        DataDiagnostic = DateTime.Now
                    };
                    App.DB.ListaDiagnostices.Add(itemToAdd);
                }
            }

            for (int i = 0; i < copieListaInvestigatii.ToArray().Length; i++)
            {
                if (!copieListaInvestigatii.ToArray()[i].Equals(listaInvestigatii.ToArray()[i]))
                {
                    ListaInvestigatii itemToAdd = new ListaInvestigatii()
                    {
                        CodInvestigatie = listaInvestigatii.ToArray()[i].CodInvestigatie,
                        IdCaz = caz.IdCaz,
                        DataInvestigatie = DateTime.Now
                    };
                    App.DB.ListaInvestigatiis.Add(itemToAdd);
                }
            }
        }

        //Functie completare alergii
        private void CompletareAlergii()
        {
            if(ListaAlergiiPacientSelectat != null)
            {
                if (ListaAlergiiPacientSelectat.Count == 0)
                {
                    AlergiiTB.Text += "Pacientul nu are alergii!";
                    return;
                }
                foreach (ListaAlergie item in ListaAlergiiPacientSelectat)
                {
                    AlergiiTB.Text += App.DB.Alergies.Where(x => x.CodAlergie == item.CodAlergie).FirstOrDefault().NumeAlergie + ",   ";
                }
            }
        }

        private void CompletareDiagnostice()
        {
            List<ListaDiagnostice> listaDiagnosticePacientSelectat = new List<ListaDiagnostice>();
            if(CazSelectat!=null)
                listaDiagnosticePacientSelectat = App.DB.ListaDiagnostices.Where(x => x.IdCaz == CazSelectat.IdCaz).ToList();

            foreach (Diagnostic diagnostic in App.DB.Diagnostics)
            {
                DiagnosticLV diag = new DiagnosticLV()
                {
                    CodDiagnostic = diagnostic.CodDiagnostic,
                    DenumireDiagnosctic = diagnostic.NumeDiagnostic,
                    Selectat = (listaDiagnosticePacientSelectat.Where(x => x.CodDiagnostic == diagnostic.CodDiagnostic).Count() == 1)
                };

                listaDiagnostice.Add(diag);
                copieListaDiagnostice.Add(diag);
            }
        }

        //completareInvestigatii
        private void CompletareInvestigatii()
        {
            List<ListaInvestigatii> listaInvestigatiiPacientSelectat = new List<ListaInvestigatii>();
            if(CazSelectat != null)
                listaInvestigatiiPacientSelectat = App.DB.ListaInvestigatiis.Where(x => x.IdCaz == CazSelectat.IdCaz).ToList();

            foreach (Investigatie investigate in App.DB.Investigaties)
            {
                InvestigatieLV inv = new InvestigatieLV()
                {
                    CodInvestigatie = investigate.CodInvestigatie,
                    DenumireInvestigatie = investigate.NumeInvestigatie,
                    Selectat = (listaInvestigatiiPacientSelectat.Where(x => x.CodInvestigatie == investigate.CodInvestigatie).Count() == 1)
                };

                listaInvestigatii.Add(inv);
                copieListaInvestigatii.Add(inv);
            }
        }
    }
}
