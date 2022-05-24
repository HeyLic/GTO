using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageStaff.xaml
    /// </summary>
    public partial class PageStaff : Page
    {
        GTOEntities context;
        public PageStaff()
        {
            InitializeComponent();
            ShowTable();
        }

        void ShowTable()
        {
            context = new GTOEntities();
            Staff.ItemsSource = context.Staff.ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        private void TB_SurName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_SurName.Text == "") { ShowTable(); return; }
            String Name = TB_SurName.Text;
            List<Staff> SearchList = context.Staff.ToList();
            SearchList = SearchList.Where(x => x.Surname.ToLower().Contains(Name.ToLower())).ToList();
            Staff.ItemsSource = SearchList.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddPersonal_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageStaffAdd());
        }
    }
}
