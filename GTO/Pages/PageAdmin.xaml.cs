using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageAutorisation());
        }

        private void Btn_Staff_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageStaff());
        }

        private void Btn_Headquarters_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageHeadquarters());
        }

        private void Btn_Peoples_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PagePeoples());
        }
    }
}
