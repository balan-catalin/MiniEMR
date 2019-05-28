using Newtonsoft.Json;
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
    /// Interaction logic for VerificareAsigurat.xaml
    /// </summary>
    public partial class VerificareAsigurat : Page
    {
        public VerificareAsigurat()
        {
            InitializeComponent();
            Loaded += VerificareAsigurat_Loaded;
        }

        private void VerificareAsigurat_Loaded(object sender, RoutedEventArgs e)
        {
            TrueLogo.Visibility = Visibility.Hidden;
            FalseLogo.Visibility = Visibility.Hidden;
        }

        public void CNPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int year = 0;
            int month = 0;
            int day = 0;
            if (CNPTextBox.Text.Length == 13)
            {
                TrueLogo.Visibility = Visibility.Hidden;
                FalseLogo.Visibility = Visibility.Hidden;

                Pacient p = App.DB.Pacients.Where(x => x.CNP.Equals(CNPTextBox.Text)).FirstOrDefault();
                if(p != null)
                    NumeAsigurat.Text = p.Nume + " " + p.Prenume;
                else
                    NumeAsigurat.Text = "-";

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

                if(cnp.Substring(0, 1) == "1" || cnp.Substring(0, 1) == "5")
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
                    Stream data = client.OpenRead($"http://localhost:65080/Api/Asigurat/GetStareByID?cnp={CNPTextBox.Text}");
                    StreamReader sr = new StreamReader(data);
                    //Console.WriteLine(sr.ReadToEnd());
                    string asig = sr.ReadToEnd();
                    if (asig.Equals("true"))
                        TrueLogo.Visibility = Visibility.Visible;
                    else
                        FalseLogo.Visibility = Visibility.Visible;
                }
                catch (System.Net.WebException)
                {
                    MessageBox.Show("Conectare imposibilă la server!");
                }
            }
        }
    }
}
