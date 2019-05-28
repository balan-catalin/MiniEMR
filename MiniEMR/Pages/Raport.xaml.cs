using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Raport.xaml
    /// </summary>
    public partial class Raport : Page
    {
        List<RaportCASExtins> raportCAs = new List<RaportCASExtins>();

        public Raport()
        {
            InitializeComponent();
            Loaded += Raport_Loaded;
        }

        private void Raport_Loaded(object sender, RoutedEventArgs e)
        {
            raportCAs = App.DB.RaportCASExtins.Where(x => x.LunaInchidereCaz == DateTime.Now.Month && x.AnInchidereCaz == DateTime.Now.Year).ToList();
            RaportListView.ItemsSource = raportCAs;
        }

        private void BtnTrimitereRaport_Click(object sender, RoutedEventArgs e)
        {
            foreach (RaportCASExtins item in raportCAs)
            {
                RaportTrimis rt = new RaportTrimis()
                {
                    Id = 0,
                    CodSpital = item.CodSpital,
                    CNP = item.CNP,
                    NumarCaz = item.NumarCaz,
                    DataInchidereCaz = item.DataInchidereCaz,
                    CodDiagnosticPrincipal = item.CodDiagnosticPrincipal,
                    CodInvestigatie = item.CodInvestigatie,
                    CostAditionalInvestigatie = item.CostAditionalInvestigatie,
                    CodServiciuMedical = item.CodServiciuMedical,
                    CostAditionalServiciuMedical = item.CostAditionalServicuMedical
                };

                string json = JsonConvert.SerializeObject(rt);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:65080/api/RaportSpitale");
                request.Method = "POST";
                request.ContentType = "application/json";
                byte[] data = UTF8Encoding.UTF8.GetBytes(json);
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                //try
                //{
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //}
                //catch (Exception)
                //{
                  //  MessageBox.Show("Raport deja trimis!");
                  //  break;
                //}
                Thread.Sleep(200);
            }
        }
    }
}
