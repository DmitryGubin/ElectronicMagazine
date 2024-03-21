using ElectronicMagazine.Magazine;
using System;
using System.Collections.Generic;
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
    }
}
