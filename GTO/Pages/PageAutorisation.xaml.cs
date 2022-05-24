using GTO.Class;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAutorisation.xaml
    /// </summary>
    public partial class PageAutorisation : Page
    {
        GTOEntities context;
        public PageAutorisation()
        {
            InitializeComponent();
            context = new GTOEntities();
        }

        void SaveUserData(Staff staff)
        {
            UserData.Name = staff.Name;
            UserData.Surname = staff.Surname;
        }

        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            string LoginFinal = TxtbLogin.Text.ToString();
            string PasswordFinal = TxtbPassword.Password.ToString();

            Staff LoginUser = context.Staff.FirstOrDefault(p => p.Login == LoginFinal && p.Password == PasswordFinal);
            if (LoginUser != null)
            {
                switch (LoginUser.Role.Name)
                {
                    case "admin":
                        SaveUserData(LoginUser);
                        FrameManager.MainFrame.Navigate(new PageAdmin());
                        break;

                    case "people":
                        SaveUserData(LoginUser);
                        FrameManager.MainFrame.Navigate(new PagePersonalData());
                        break;
                }
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
