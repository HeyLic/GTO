using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageStandarts.xaml
    /// </summary>
    public partial class PageStandarts : Page
    {
        GTOEntities context;
        public PageStandarts()
        {
            InitializeComponent();
            ShowTable();

            CMBMedal.ItemsSource = context.Medal.ToList();

            CMBStep.ItemsSource = context.Steps.ToList();
        }

        void ShowTable()
        {
            context = new GTOEntities();
            Standarts.ItemsSource = context.Standarts.ToList();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        private void CMBStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Searcher();
        }

        private void CMBMedal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Searcher();
        }
        void Searcher()
        {
            List<Standarts> searchlist = context.Standarts.ToList();

            if (CMBStep.SelectedItem != null)
                searchlist = searchlist.Where(x => x.id_step == Convert.ToInt32(CMBStep.SelectedValue)).ToList();
            if (CMBMedal.SelectedItem != null)
                searchlist = searchlist.Where(x => x.id_medal == Convert.ToInt32(CMBMedal.SelectedValue)).ToList();

            Standarts.ItemsSource = searchlist.ToList();
        }
        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            int step = Convert.ToInt32(CMBStep.SelectedValue);
            Standarts.ItemsSource = context.Standarts.Where(x => x.id_step == step).ToList();

            int medal = Convert.ToInt32(CMBMedal.SelectedValue);
            Standarts.ItemsSource = context.Standarts.Where(x => x.id_medal == medal).ToList();
        }
    }
}
