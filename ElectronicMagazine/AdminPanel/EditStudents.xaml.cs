using ElectronicMagazine.Magazine;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectronicMagazine.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для EditStudents.xaml
    /// </summary>
    public partial class EditStudents : Page
    {
        JournalEntities entities = new JournalEntities();
        Students students = new Students();
        public EditStudents()
        {
            InitializeComponent();
            DataContext = students;

        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var remove = dGrid.SelectedItems.Cast<Students>().ToList();

            var service_remove = dGrid.SelectedItem as Students;
            try
            {
                var exx = JournalEntities.GetContext().Classes.Where(p => p.Id ==
               service_remove.Id_Класса).First();
                MessageBox.Show("Запись удалить нельзя!" + Environment.NewLine + "Существует запись на данную процедуру","Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
            catch
            {
                if (MessageBox.Show($"Вы точно хотите удалить следующие {remove.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var context = JournalEntities.GetContext();

                    foreach (var grade in remove)
                    {
                        var gradeToRemove = context.Students.Find(grade.Id);
                        if (gradeToRemove != null)
                        {
                            context.Students.Remove(gradeToRemove);
                            context.SaveChanges();
                            dGrid.ItemsSource = JournalEntities.GetContext().Students.ToList();
                            MessageBox.Show("Данные успешно удалены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при удалении!", "Провал!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    dGrid.Items.Refresh();
                }
            }

        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new EditPanelStudent((sender as Button).DataContext as Students));
            dGrid.Items.Refresh();

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                dGrid.ItemsSource = JournalEntities.GetContext().Students.ToList();
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickAdd(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditPanelStudent(null));
            dGrid.Items.Refresh();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        public void UpdateService()
        {
            var serviceListt = JournalEntities.GetContext().Students.ToList();

            var searchText = SearchText.Text.ToLower();

            serviceListt = serviceListt.Where(p =>
                p.Фамилия.ToLower().Contains(searchText) ||
                p.Имя.ToLower().Contains(searchText) ||
                p.Дата_рождения.ToString().Contains(searchText) ||
                p.Id_Класса.ToString().Contains(searchText)).ToList();

            dGrid.ItemsSource = serviceListt.ToList();
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Установка контекста лицензии для библиотеки EPPlus
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            // Получаем заголовки столбцов
            for (int i = 0; i < dGrid.Columns.Count; i++)
            {
                if (dGrid.Columns[i].Header != null && dGrid.Columns[i].Header.ToString() != "Фото")
                {
                    worksheet.Cells[1, i + 1].Value = dGrid.Columns[i].Header;
                }
            }

            // Заполняем ячейки данными из DataGrid
            var dataItems = dGrid.ItemsSource as IEnumerable<Students>;
            if (dataItems != null)
            {
                int rowIndex = 2;
                foreach (var item in dataItems)
                {
                    int columnIndex = 1;
                    for (int i = 0; i < dGrid.Columns.Count; i++)
                    {
                        if (dGrid.Columns[i].Header != null && dGrid.Columns[i].Header.ToString() != "Фото")
                        {
                            switch (dGrid.Columns[i].Header.ToString())
                            {
                                case "Имя":
                                    worksheet.Cells[rowIndex, columnIndex].Value = item.Имя;
                                    break;
                                case "Фамилия":
                                    worksheet.Cells[rowIndex, columnIndex].Value = item.Фамилия;
                                    break;
                                case "Дата рождения":
                                    worksheet.Cells[rowIndex, columnIndex].Value = item.Дата_рождения?.ToShortDateString();
                                    break;
                                case "Класс":
                                    worksheet.Cells[rowIndex, columnIndex].Value = item.Id_Класса?.ToString();
                                    break;
                            }
                            columnIndex++;
                        }
                    }
                    rowIndex++;
                }
            }

            // Автонастройка размеров столбцов
            worksheet.Cells.AutoFitColumns();
            // Сохраняем файл Excel
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var excelFile = new FileInfo(saveFileDialog.FileName);
                excelPackage.SaveAs(excelFile);
                MessageBox.Show("Данные успешно экспортированы в Excel.", "Экспорт завершен", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
