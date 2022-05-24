using System;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageStaffAdd.xaml
    /// </summary>
    public partial class PageStaffAdd : Page
    {
        GTOEntities context;
        public PageStaffAdd()
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
                Staff name = new Staff()
                {
                    Surname = TxbSurname.Text,
                    Name = TxbName.Text,
                    Patronymic = TxbPatronymic.Text,
                    Email = TxbEmail.Text,
                    Login = TxbLogin.Text,
                    Password = TxbPassword.Text,
                    id_Role = 2
                };
                context.Staff.Add(name);
                context.SaveChanges();
                MessageBox.Show("Сотрудник добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameManager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
