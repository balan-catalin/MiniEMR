﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiniEMR
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach (PersonalMedical pm in App.DB.PersonalMedicals)
                if (pm.NumeUtilizator.Equals(NumeUtilizatorTB.Text) && pm.Parola.Equals(PasswordBoxLogin.Password))
                {
                    MainWindow mw = new MainWindow
                    {
                        LoggedPersonalMedical = pm
                    };
                    mw.Show();
                    this.Close();
                }
            Storyboard sb = this.FindResource("OpenError") as Storyboard;
            sb.Begin();
        }
    }
}
