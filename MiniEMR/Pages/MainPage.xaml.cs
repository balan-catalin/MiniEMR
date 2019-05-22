﻿using System;
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

namespace MiniEMR.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ListaPacienti.ItemsSource = App.DB.ListaPacientis.ToList();
        }

        private void ListaPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = ListaPacienti.SelectedItem;
            String NumarFisa = selectedItem.NumarFisa;
            
            ListaCazuri.ItemsSource = App.DB.ListaCazuris.Where(x => x.NumarFisa == NumarFisa).ToList();
        }

        
    }
}
