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
    /// Interaction logic for VerificareAsigurat.xaml
    /// </summary>
    public partial class VerificareAsigurat : Page
    {
        public VerificareAsigurat()
        {
            InitializeComponent();
        }

        private void CNPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
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
                DateTime dateOfBirth = new DateTime(year, month, day);
                DataNasteriiTextBlock.Text = dateOfBirth.ToString("dd/MM/yyyy");
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > today.AddYears(-age)) age--;
                VarstaTextBlock.Text = age.ToString();
            }
        }
    }
}
