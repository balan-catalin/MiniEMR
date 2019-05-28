﻿using System;
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
        ObservableCollection<ObservatieLV> listaObservatii = new ObservableCollection<ObservatieLV>();

        public CazNou()
        {
            InitializeComponent();
            Loaded += CazNou_Loaded;
        }

        private void CazNou_Loaded(object sender, RoutedEventArgs e)
        {
            DiagnosticListView.ItemsSource = App.DB.Diagnostics.ToList();
            InvestigatieListView.ItemsSource = App.DB.Investigaties.ToList();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FisaPacient fp = App.DB.FisaPacients.Where(x => x.NumarFisa == NumarFisaTB.Text).FirstOrDefault();
            Caz caz = new Caz()
            {
                DataDeschidereCaz = DateTime.Now,
                NumarCaz = NumarCazTB.ToString(),
                IdFisa = fp.IdFisa,
                IdPersonalMedical = 3,
            };
            App.DB.Cazs.Add(caz);

            foreach (Diagnostic item in DiagnosticListView.SelectedItems)
            {
                if (DiagnosticListView.SelectedIndex.Equals(1))
                {
                    ListaDiagnostice listaDiagnostice = new ListaDiagnostice()
                    {
                        CodDiagnostic = item.CodDiagnostic,
                        IdCaz = caz.IdCaz,
                        DiagnosticPrincipal = true
                    };
                    App.DB.ListaDiagnostices.Add(listaDiagnostice);
                }
                else
                {
                    ListaDiagnostice listaDiagnostice = new ListaDiagnostice()
                    {
                        CodDiagnostic = item.CodDiagnostic,
                        IdCaz = caz.IdCaz,
                        DiagnosticPrincipal = false
                    };
                    App.DB.ListaDiagnostices.Add(listaDiagnostice);
                }

                foreach (Investigatie itemInv in InvestigatieListView.SelectedItems)
                {
                    ListaInvestigatii listaInvestigatii = new ListaInvestigatii()
                    {
                        CodInvestigatie = itemInv.CodInvestigatie,
                        IdCaz = caz.IdCaz
                    };
                    App.DB.ListaInvestigatiis.Add(listaInvestigatii);
                }

                foreach (ServiciuMedical itemSrv in ServiciuMedicalListView.SelectedItems)
                {
                    ListaServiciiMedicale listaServiciiMedicale = new ListaServiciiMedicale()
                    {
                        CodServiciu = itemSrv.CodServiciu,
                        IdCaz = caz.IdCaz
                    };
                    App.DB.ListaServiciiMedicales.Add(listaServiciiMedicale);
                }

                foreach(ObservatieLV itemObs in ObservatieListView.Items)
                {

                }
            }
        }
    }
}
