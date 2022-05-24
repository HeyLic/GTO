using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageNormativ.xaml
    /// </summary>
    public partial class PageNormativ : Page
    {
        GTOEntities context;
        int genderID;
        int pdid;
        public PageNormativ(int id)
        {
            InitializeComponent();
            context = new GTOEntities();
            pdid = id;
            CmbPeople.ItemsSource = context.Personal_data.ToList();
            CmbPeople.SelectedValue = id;
            Personal_data pd = context.Personal_data.FirstOrDefault(x => x.id == pdid);
            genderID = context.Personal_data.FirstOrDefault(x => x.id == pdid).id_gender;
            CmbVidNormativ.ItemsSource = context.NormativTypes.ToList();
        }

        private void CmbVidNormativ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = Convert.ToInt32(CmbVidNormativ.SelectedValue);
            Personal_data pd = context.Personal_data.FirstOrDefault(x => x.id == pdid);
            CmbNormativ.ItemsSource = context.NormativSubTypes.Where(x => x.id_gender == genderID && x.id_NormativType == id && x.Step.Contains(pd.Steps.Step)).ToList();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }
    }
}
