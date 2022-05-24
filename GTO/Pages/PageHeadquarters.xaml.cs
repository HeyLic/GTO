using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageHeadquarters.xaml
    /// </summary>
    public partial class PageHeadquarters : Page
    {
        GTOEntities context;
        public PageHeadquarters()
        {
            InitializeComponent();
            ShowTable();
        }

        void ShowTable()
        {
            context = new GTOEntities();
            Headquarters.ItemsSource = context.Headquarters.ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        private void TBRegion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBRegion.Text == "") { ShowTable(); return; }
            String Name = TBRegion.Text;
            List<Headquarters> SearchList = context.Headquarters.ToList();
            SearchList = SearchList.Where(x => x.Region.ToLower().Contains(Name.ToLower())).ToList();
            Headquarters.ItemsSource = SearchList.ToList();
        }

        private void TBCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBCity.Text == "") { ShowTable(); return; }
            String Name = TBCity.Text;
            List<Headquarters> SearchList = context.Headquarters.ToList();
            SearchList = SearchList.Where(x => x.City.ToLower().Contains(Name.ToLower())).ToList();
            Headquarters.ItemsSource = SearchList.ToList();
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBName.Text == "") { ShowTable(); return; }
            String Name = TBName.Text;
            List<Headquarters> SearchList = context.Headquarters.ToList();
            SearchList = SearchList.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
            Headquarters.ItemsSource = SearchList.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddHeadquarters_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageHeadquartersAdd());
        }
    }
}
