using System;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageHeadquartersAdd.xaml
    /// </summary>
    public partial class PageHeadquartersAdd : Page
    {
        GTOEntities context;
        public PageHeadquartersAdd()
        {
            InitializeComponent();
            context = new GTOEntities();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Headquarters name = new Headquarters()
                {
                    Region = TxbRegion.Text,
                    City = TxbCity.Text,
                    Street = TxbStreet.Text,
                    Email = TxbEmail.Text,
                    Phone = TxbPhone.Text,
                    Name = TxbName.Text
                };
                context.Headquarters.Add(name);
                context.SaveChanges();
                MessageBox.Show("Штаб добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameManager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
