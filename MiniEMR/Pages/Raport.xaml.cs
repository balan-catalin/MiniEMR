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
        public Raport()
        {
            InitializeComponent();
            Loaded += Raport_Loaded;
        }

        private void Raport_Loaded(object sender, RoutedEventArgs e)
        {
            List<RaportCAS> raportCAS = App.DB.RaportCAS.ToList();
            RaportListView.ItemsSource = raportCAS;
        }

        private void BtnTrimitereRaport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
