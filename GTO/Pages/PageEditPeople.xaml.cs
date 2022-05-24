using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace GTO.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageEditPeople.xaml
    /// </summary>
    public partial class PageEditPeople : System.Windows.Controls.Page
    {
        GTOEntities context;
        int id;
        Personal_data data;
        public PageEditPeople(GTOEntities context, Personal_data newRow)
        {
            data = newRow;
            this.context = context;
            this.DataContext = data;
            InitializeComponent();
            id = data.id;
            CmbMedal.ItemsSource = context.Medal.ToList();
            CmbStep.ItemsSource = context.Steps.ToList();
            ShowTable();


        }
        void ShowTable()
        {
            Results.ItemsSource = context.Results.Where(x => x.id_People == id).ToList();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.GoBack();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TxbSurname.Text != "" && TxbName.Text != "" && TxbPatronymic.Text != null)
            {
                try
                {
                    context.SaveChanges();
                    FrameManager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка в данных");
                }
            }
            else
            {
                MessageBox.Show("Введены не все данные", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void BtnResult_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageResults(id));
        }

        private void BtnNormativ_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new PageNormativ(id));
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            Personal_data People = context.Personal_data.FirstOrDefault(x => x.id == id);

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 1;

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            for (int i = 1; i <= 1; i++)
            {
                int startRowIndex = 1;

                Excel.Worksheet worksheet = application.Worksheets.Item[i];
                worksheet.Name = "Личный зачет";

                worksheet.Cells[1][startRowIndex] = People.NameAdd + "\n Медаль - " + People.Medal.Name;
                Range Title = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                Title.Merge(Type.Missing);
                Title.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                Title.VerticalAlignment = XlHAlign.xlHAlignCenter;
                Title.Font.Bold = true;
                startRowIndex++;
                worksheet.Cells[1][startRowIndex] = "Испытание";
                worksheet.Cells[2][startRowIndex] = "Результат";
                worksheet.Cells[3][startRowIndex] = "Дата сдачи";
                worksheet.Cells[4][startRowIndex] = "Штаб";

                startRowIndex++;

                List<Results> RESULTS = context.Results.Where(x => x.id_People == id).ToList();

                foreach (var result in RESULTS)
                {
                    worksheet.Cells[1][startRowIndex] = result.NormativSubTypes.SubTypeName.ToString();
                    worksheet.Cells[2][startRowIndex] = result.Result.ToString();
                    worksheet.Cells[3][startRowIndex] = result.date_of_complete.Date;
                    worksheet.Cells[4][startRowIndex] = result.Headquarters.Name.ToString();

                    startRowIndex++;
                }

                Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[4][startRowIndex - 1]];
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

        private void BtnEndResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (context.Results.Where(x => x.id_People == id).Count() == 7)
                {
                    CmbMedal.SelectedValue = 3;
                    context.SaveChanges();
                    MessageBox.Show("Результат присвоен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (context.Results.Where(x => x.id_People == id).Count() == 8)
                    {
                        CmbMedal.SelectedValue = 2;
                        context.SaveChanges();
                        MessageBox.Show("Результат присвоен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (context.Results.Where(x => x.id_People == id).Count() >= 9)
                        {
                            CmbMedal.SelectedValue = 1;
                            context.SaveChanges();
                            MessageBox.Show("Результат присвоен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            CmbMedal.SelectedValue = 4;
                            context.SaveChanges();
                            MessageBox.Show("Результата нет!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Ошибка присвоения результата", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ShowTable();
        }
    }
}
