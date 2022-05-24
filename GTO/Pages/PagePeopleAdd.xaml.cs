using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PagePeopleAdd.xaml
    /// </summary>
    public partial class PagePeopleAdd : Page
    {
        GTOEntities context;
        int genderID = 1;

        public PagePeopleAdd(GTOEntities context, Personal_data pD)
        {
            this.context = context;
            this.DataContext = pD;
            InitializeComponent();
            context = new GTOEntities();
            DpAge.SelectedDate = DateTime.Now;
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbMedal.ItemsSource = context.Medal.ToList();
            CmbMedal.SelectedValue = 4;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context.SaveChanges();
                MessageBox.Show("Человек добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameManager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }


        private void DpAge_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var date = DpAge.SelectedDate.Value.Date;
                var age = DateTime.Now.Year - date.Year;
                if (DateTime.Now.DayOfYear < date.DayOfYear)
                    age++;
                Steps step = context.Steps.FirstOrDefault(x => x.Age.ToLower().Contains(age.ToString().ToLower()) && x.id_gender == genderID);
                if (step != null)
                    CmbStep.SelectedValue = step.id;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Введён некорректный возраст", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            genderID = Convert.ToInt32(CmbGender.SelectedValue);
        }
    }
}
