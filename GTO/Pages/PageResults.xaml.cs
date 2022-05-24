using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageResults.xaml
    /// </summary>
    public partial class PageResults : Page
    {
        GTOEntities context;
        int genderID;
        int pdid;
        public PageResults(int id)
        {
            InitializeComponent();
            context = new GTOEntities();
            pdid = id;
            CmbPeople.ItemsSource = context.Personal_data.ToList();
            CmbPeople.SelectedValue = id;
            Personal_data pd = context.Personal_data.FirstOrDefault(x => x.id == pdid);
            CmbHeadquarters.ItemsSource = context.Headquarters.ToList();
            genderID = context.Personal_data.FirstOrDefault(x => x.id == pdid).id_gender;
            CmbVidNormativ.ItemsSource = context.NormativTypes.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Results name = new Results()
                {
                    id_People = Convert.ToInt32(CmbPeople.SelectedValue),
                    id_Headquarters = Convert.ToInt32(CmbHeadquarters.SelectedValue),
                    date_of_complete = DpDate.SelectedDate.Value.Date,
                    id_SubType = Convert.ToInt32(CmbNormativ.SelectedValue),
                    Result = TxbResult.Text
                };
                context.Results.Add(name);
                context.SaveChanges();
                MessageBox.Show("Результат добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void CmbVidNormativ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = Convert.ToInt32(CmbVidNormativ.SelectedValue);
            Personal_data pd = context.Personal_data.FirstOrDefault(x => x.id == pdid);
            CmbNormativ.ItemsSource = context.NormativSubTypes.Where(x => x.id_gender == genderID && x.id_NormativType == id && x.Step.Contains(pd.Steps.Step)).ToList();
        }
    }
}
