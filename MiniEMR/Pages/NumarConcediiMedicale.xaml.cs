using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for NumarConcediiMedicale.xaml
    /// </summary>
    public partial class NumarConcediiMedicale : Page
    {
        public NumarConcediiMedicale()
        {
            InitializeComponent();
        }

        private void CNPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Pacient p = App.DB.Pacients.Where(x => x.CNP.Equals(CNPTextBox.Text)).FirstOrDefault();
            if (p != null)
                NumeAsigurat.Text = p.Nume + " " + p.Prenume;
            else
                NumeAsigurat.Text = "-";

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

                if (cnp.Substring(0, 1) == "1" || cnp.Substring(0, 1) == "5")
                {
                    SexTextBlock.Text = "M";
                }
                else
                {
                    SexTextBlock.Text = "F";
                }

                DateTime dateOfBirth = new DateTime(year, month, day);
                DataNasteriiTextBlock.Text = dateOfBirth.ToString("dd/MM/yyyy");
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > today.AddYears(-age)) age--;
                VarstaTextBlock.Text = age.ToString();

                WebClient client = new WebClient();
                //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                try
                {
                    Stream data = client.OpenRead($"http://localhost:65080/api/ConcediuMedical/NumarConcediiMedicale?cnp={CNPTextBox.Text}");
                    StreamReader sr = new StreamReader(data);
                    Int32.TryParse(sr.ReadToEnd(), out int con);
                    NumarZileConcediuMedical.Text = con.ToString();
                }
                catch (System.Net.WebException)
                {
                    MessageBox.Show("Conectare imposibilă la server!");
                }
            }
        }
    }
}
