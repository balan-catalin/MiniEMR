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

        Diagnostic diagPrincipalSelectat = new Diagnostic();

        ObservableCollection<ObservatieLV> listaObservatii = new ObservableCollection<ObservatieLV>();
        int numarObservatii=0;

        ObservableCollection<DiagnosticLV> listaDiagnostice = new ObservableCollection<DiagnosticLV>();
        List<DiagnosticLV> copieListaDiagnostice = new List<DiagnosticLV>();

        ObservableCollection<InvestigatieLV> listaInvestigatii = new ObservableCollection<InvestigatieLV>();
        List<InvestigatieLV> copieListaInvestigatii = new List<InvestigatieLV>();

        ObservableCollection<ServiciuLV> listaServicii = new ObservableCollection<ServiciuLV>();
        List<ServiciuLV> copieListaServicii = new List<ServiciuLV>();

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
            CompletareServicii();
            CompletareObservatii();

            DiagnosticPrincipalCB.ItemsSource = App.DB.Diagnostics.ToList();

            if (CazSelectat != null)
            {
                NumarCazTB.Text = CazSelectat.NumarCaz;
                if(CazSelectat.CodDiagnosticPrincipal!=null)
                    DiagnosticPrincipalCB.SelectedItem = App.DB.Diagnostics.Where(x => x.CodDiagnostic == CazSelectat.CodDiagnosticPrincipal).SingleOrDefault();
            }

            //declarare itemsSource
            DiagnosticPrincipalCB.ItemsSource = App.DB.Diagnostics.ToList();
            DiagnosticListView.ItemsSource = listaDiagnostice;
            InvestigatieListView.ItemsSource = listaInvestigatii;
            ServiciuMedicalListView.ItemsSource = listaServicii;
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
                caz.NumarCaz = NumarCazTB.Text;
                caz.IdFisa = fp.IdFisa;
                caz.IdPersonalMedical = 3;
                caz.CodDiagnosticPrincipal = diagPrincipalSelectat.CodDiagnostic;
                App.DB.Cazs.Add(caz);
                CazSelectat = caz;
            }
            else
                caz = CazSelectat;
            App.DB.SaveChanges();

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
                    App.DB.SaveChanges();
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
                        DataInvestigatie = DateTime.Now,
                        CostAditional = listaInvestigatii.ToArray()[i].CostAditional
                    };
                    App.DB.ListaInvestigatiis.Add(itemToAdd);
                    App.DB.SaveChanges();
                }
                else if(copieListaInvestigatii.ToArray()[i].CostAditional != listaInvestigatii.ToArray()[i].CostAditional)
                {
                    String codInvestigatie = listaInvestigatii.ToArray()[i].CodInvestigatie;
                    App.DB.ListaInvestigatiis.Where(x => x.IdCaz == CazSelectat.IdCaz && x.CodInvestigatie == codInvestigatie).SingleOrDefault().CostAditional = listaInvestigatii.ToArray()[i].CostAditional;
                    App.DB.SaveChanges();
                }
            }

            for (int i = 0; i < copieListaServicii.ToArray().Length; i++)
            {
                if (!copieListaServicii.ToArray()[i].Equals(listaServicii.ToArray()[i]))
                {
                    ListaServiciiMedicale itemToAdd = new ListaServiciiMedicale()
                    {
                        CodServiciu = listaServicii.ToArray()[i].CodServiciu,
                        IdCaz = caz.IdCaz,
                        Data = DateTime.Now,
                        CostAditional = listaServicii.ToArray()[i].CostAditional
                    };
                    App.DB.ListaServiciiMedicales.Add(itemToAdd);
                    App.DB.SaveChanges();
                }
                else if(copieListaServicii.ToArray()[i].CostAditional != listaServicii.ToArray()[i].CostAditional)
                {
                    String CodServiciu = listaServicii.ToArray()[i].CodServiciu;
                    App.DB.ListaServiciiMedicales.Single(x => x.CodServiciu == CodServiciu && x.IdCaz == CazSelectat.IdCaz).CostAditional = listaServicii.ToArray()[i].CostAditional;
                    App.DB.SaveChanges();
                }
            }

            //salvare observatii noi in baza de date
            for (int i = numarObservatii; i < listaObservatii.ToArray().Length; i++)
            {
                Observatie observatie = new Observatie()
                {
                    TextObservatie = listaObservatii.ToArray()[i].TextObservatie
                };
                App.DB.Observaties.Add(observatie);
                App.DB.SaveChanges();

                ListaObservatii itemToAdd = new ListaObservatii()
                {
                    IdObservatie = observatie.IdObservatie,
                    DataObservatie = listaObservatii.ToArray()[i].DataObservatie,
                    IdCaz = CazSelectat.IdCaz
                };
                App.DB.ListaObservatiis.Add(itemToAdd);
                App.DB.SaveChanges();
            }
            App.DB.SaveChanges();
            MessageBox.Show("Caz salvat!");
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            CazNou cazPage = new CazNou();
            mw.MenuFrame.Content = cazPage;
        }

        private void DiagnosticPrincipalCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            diagPrincipalSelectat = DiagnosticPrincipalCB.SelectedItem as Diagnostic;
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
                    AlergiiTB.Text += App.DB.Alergies.Where(x => x.CodAlergie == item.CodAlergie).FirstOrDefault().NumeAlergie + ",  ";
                }
            }
        }

        //completare diagnostice
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

                DiagnosticLV diag2 = new DiagnosticLV()
                {
                    CodDiagnostic = diagnostic.CodDiagnostic,
                    DenumireDiagnosctic = diagnostic.NumeDiagnostic,
                    Selectat = (listaDiagnosticePacientSelectat.Where(x => x.CodDiagnostic == diagnostic.CodDiagnostic).Count() == 1)
                };

                listaDiagnostice.Add(diag);
                copieListaDiagnostice.Add(diag2);
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
                double CostAditional;
                if (listaInvestigatiiPacientSelectat.Where(x => x.CodInvestigatie == investigate.CodInvestigatie).SingleOrDefault() != null)
                    CostAditional = listaInvestigatiiPacientSelectat.Where(x => x.CodInvestigatie == investigate.CodInvestigatie).SingleOrDefault().CostAditional;
                else
                    CostAditional = 0;

                InvestigatieLV inv = new InvestigatieLV()
                {
                    CodInvestigatie = investigate.CodInvestigatie,
                    DenumireInvestigatie = investigate.NumeInvestigatie,
                    Selectat = (listaInvestigatiiPacientSelectat.Where(x => x.CodInvestigatie == investigate.CodInvestigatie).Count() == 1),
                    CostAditional = CostAditional
                };

                InvestigatieLV inv2 = new InvestigatieLV()
                {
                    CodInvestigatie = investigate.CodInvestigatie,
                    DenumireInvestigatie = investigate.NumeInvestigatie,
                    Selectat = (listaInvestigatiiPacientSelectat.Where(x => x.CodInvestigatie == investigate.CodInvestigatie).Count() == 1),
                    CostAditional = CostAditional
                };

                listaInvestigatii.Add(inv);
                copieListaInvestigatii.Add(inv2);
            }
        }

        //completare Servicii
        private void CompletareServicii()
        {
            List<ListaServiciiMedicale> listaServiciiPacientSelectat = new List<ListaServiciiMedicale>();
            if (CazSelectat != null)
                listaServiciiPacientSelectat = App.DB.ListaServiciiMedicales.Where(x => x.IdCaz == CazSelectat.IdCaz).ToList();

            foreach (ServiciuMedical serviciu in App.DB.ServiciuMedicals)
            {
                double CostAditional;
                if (listaServiciiPacientSelectat.Where(x => x.CodServiciu == serviciu.CodServiciu).SingleOrDefault() != null)
                    CostAditional = listaServiciiPacientSelectat.Where(x => x.CodServiciu == serviciu.CodServiciu).SingleOrDefault().CostAditional;
                else
                    CostAditional = 0;

                ServiciuLV srv = new ServiciuLV()
                {
                    CodServiciu = serviciu.CodServiciu,
                    NumeServiciu = serviciu.NumeServiciu,
                    Selectat = (listaServiciiPacientSelectat.Where(x => x.CodServiciu == serviciu.CodServiciu).Count() == 1),
                    CostAditional = CostAditional
                };

                ServiciuLV srv2 = new ServiciuLV()
                {
                    CodServiciu = serviciu.CodServiciu,
                    NumeServiciu = serviciu.NumeServiciu,
                    Selectat = (listaServiciiPacientSelectat.Where(x => x.CodServiciu == serviciu.CodServiciu).Count() == 1),
                    CostAditional = CostAditional
                };

                listaServicii.Add(srv);
                copieListaServicii.Add(srv2);
            }
        }

        //Completare observatii
        private void CompletareObservatii()
        {
            if(CazSelectat!=null)
                foreach(ListaObservatii obs in App.DB.ListaObservatiis.Where(x => x.IdCaz == CazSelectat.IdCaz).ToList())
                {
                    ObservatieLV itemToAdd = new ObservatieLV()
                    {
                        DataObservatie = obs.DataObservatie,
                        TextObservatie = App.DB.Observaties.Where(x => x.IdObservatie == obs.IdObservatie).Single().TextObservatie
                    };
                    listaObservatii.Add(itemToAdd);
                }
            numarObservatii = listaObservatii.Count();
        }
    }
}
