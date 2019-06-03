using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.Entity.Migrations;

namespace MiniEMR.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        String NumarFisa;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ListaPacienti.ItemsSource = App.DB.ListaPacientis.ToList();
        }

        //lista cazuri pacient selectat
        private void ListaPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = ListaPacienti.SelectedItem;
            NumarFisa = selectedItem.NumarFisa;
            ListaCazuri.ItemsSource = App.DB.ListaCazuris.Where(x => x.NumarFisa == NumarFisa).ToList();
        }

        private void ListaPacienti_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //setarea elementelor din pagina PacientNou cu datele pacientului selectat
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            PacientNou pn = new PacientNou();
            mw.MenuFrame.Content = pn;
            dynamic selectedItem = ListaPacienti.SelectedItem;
            pn.NrFisaMedicalaTextBox.Text = selectedItem.NumarFisa;
            pn.CNPTextBox.Text = selectedItem.CNP;
            pn.NumeTextBox.Text = selectedItem.Nume;
            pn.PrenumeTextBox.Text = selectedItem.Prenume;
            pn.NumarFisaPacientSelectat = selectedItem.NumarFisa; //transmiterea Numarului Fisei catre pagina PacientNou
        }

        private void DeschidereCaz_Click(object sender, RoutedEventArgs e)
        {
            //inserare date pacient in pagina de caz
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            CazNou caz = new CazNou();
            mw.MenuFrame.Content = caz;
            Button btn = ((Button)sender);
            dynamic selectedItem = btn.DataContext;
            caz.NumePrenumeTB.Text += " " + selectedItem.Nume.ToString() + " " + selectedItem.Prenume.ToString();
            caz.VarstaTB.Text += " " + selectedItem.Varsta.ToString();
            caz.SexTB.Text += " " + selectedItem.Sex.ToString();
            caz.NumarFisaTB.Text = selectedItem.NumarFisa;
            String nrFisa = selectedItem.NumarFisa;
            List<Caz> list = App.DB.Cazs.ToList();
            Caz el = list.LastOrDefault();
            int nr = Int32.Parse(el.NumarCaz.Substring(4)) + 1;
            caz.NumarCazTB.Text = "CAZ-" + nr;

            //trimiterea listei de alergii a pacientului
            FisaPacient fp = App.DB.FisaPacients.Where(x => x.NumarFisa == nrFisa).SingleOrDefault();
            caz.ListaAlergiiPacientSelectat = App.DB.ListaAlergies.Where(x => x.IdFisa == fp.IdFisa).ToList();
        }

        private void BtnInchidereCaz_Click(object sender, RoutedEventArgs e)
        {
            Button btn = ((Button)sender);
            dynamic selectedItem = btn.DataContext;
            String numarCaz = selectedItem.NumarCaz;
            Caz selected = App.DB.Cazs.SingleOrDefault(x => x.NumarCaz == numarCaz);
            Caz newObject = selected;
            newObject.DataInchidereCaz = DateTime.Now;
            App.DB.Entry(selected).CurrentValues.SetValues(newObject);
        }

        private void ListaCazuri_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            CazNou cazPage = new CazNou();
            mw.MenuFrame.Content = cazPage;
            dynamic selectedItem = ListaCazuri.SelectedItem;
            String NumarCaz = selectedItem.NumarCaz;
            cazPage.NumePrenumeTB.Text += " " + selectedItem.Nume.ToString() + " " + selectedItem.Prenume.ToString();
            cazPage.VarstaTB.Text += " " + App.DB.ListaPacientis.Where(x => x.NumarFisa == NumarFisa).SingleOrDefault().Varsta;
            cazPage.SexTB.Text += " " + App.DB.ListaPacientis.Where(x => x.NumarFisa == NumarFisa).SingleOrDefault().Sex;
            String numarFisa = selectedItem.NumarFisa;
            cazPage.NumarFisaTB.Text = numarFisa;
            Caz caz = App.DB.Cazs.Where(x => x.NumarCaz == NumarCaz).SingleOrDefault();
            cazPage.CazSelectat = caz;

            FisaPacient fp = App.DB.FisaPacients.Where(x => x.NumarFisa == numarFisa).SingleOrDefault();
            cazPage.ListaAlergiiPacientSelectat = App.DB.ListaAlergies.Where(x => x.IdFisa == fp.IdFisa).ToList();
        }
    }
}
