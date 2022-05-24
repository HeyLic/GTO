using GTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PagePersonalData.xaml
    /// </summary>
    public partial class PagePersonalData : Page
    {
        GTOEntities context;
        int id = 0;
        List<Personal_data> data = new List<Personal_data>();
        
        public PagePersonalData()
        {
            InitializeComponent();
            ShowTable();
            context = new GTOEntities();
            UserName.Text = UserData.Name + " " + UserData.Surname;
            CmbMedal.ItemsSource = context.Medal.ToList();
        }
        void ShowTable()
        {
            context = new GTOEntities();
            data = context.Personal_data.ToList();
            Personal_data_DG.ItemsSource = data.ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageAutorisation());
        }

        private void PeopleAdd_Click(object sender, RoutedEventArgs e)
        {
            Personal_data PD = new Personal_data();
            context.Personal_data.Add(PD);
            FrameManager.MainFrame.Navigate(new PagePeopleAdd(context, PD));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            var selectedRow = (Personal_data)editButton.DataContext;
            FrameManager.MainFrame.Navigate(new PageEditPeople(context, selectedRow));
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 3;

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);



            for (int i = 1; i <= 3; i++)
            {
                int startRowIndex = 1;

                Excel.Worksheet worksheet = application.Worksheets.Item[i];
                worksheet.Name = "Медаль " + context.Medal.FirstOrDefault(x => x.id == i).Name;

                worksheet.Cells[1][startRowIndex] = "Фамилия";
                worksheet.Cells[2][startRowIndex] = "Имя";
                worksheet.Cells[3][startRowIndex] = "Отчество";

                startRowIndex++;

                List<Personal_data> PD = data.Where(x => x.id_medal == i).ToList();

                foreach (var person in PD)
                {
                    worksheet.Cells[1][startRowIndex] = person.Surname.ToString();
                    worksheet.Cells[2][startRowIndex] = person.Name.ToString();
                    worksheet.Cells[3][startRowIndex] = person.Patronymic.ToString();

                    startRowIndex++;
                }

                Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[3][startRowIndex - 1]];
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                worksheet.Columns.AutoFit();
            }
            application.Visible = true;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ShowTable();
        }

        private void TB_Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Surname.Text == "") { ShowTable(); return; }
            String Name = TB_Surname.Text;
            List<Personal_data> SearchList = context.Personal_data.ToList();
            SearchList = SearchList.Where(x => x.Surname.ToLower().Contains(Name.ToLower())).ToList();
            Personal_data_DG.ItemsSource = SearchList.ToList();
        }

        private void TB_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Name.Text == "") { ShowTable(); return; }
            String Name = TB_Name.Text;
            List<Personal_data> SearchList = context.Personal_data.ToList();
            SearchList = SearchList.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
            Personal_data_DG.ItemsSource = SearchList.ToList();
        }

        private void CmbMedal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int medal = Convert.ToInt32(CmbMedal.SelectedValue);
            Personal_data_DG.ItemsSource = context.Personal_data.Where(x => x.id_medal == medal).ToList();
        }
    }
}
