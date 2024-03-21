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
    /// Логика взаимодействия для PanelTeacher.xaml
    /// </summary>
    public partial class PanelTeacher : Page
    {
        Teachers teachers;
        JournalEntities entities = new JournalEntities();
        public PanelTeacher()
        {
            InitializeComponent();

            DataContext = teachers;
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void ClickAdd(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditPanelTeacher(null));
            dGrid.Items.Refresh();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new EditPanelTeacher((sender as Button).DataContext as Teachers));
            dGrid.Items.Refresh();

        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var service_remove = dGrid.SelectedItem as Teachers;
            try
            {
                var exx = JournalEntities.GetContext().Users.Where(p => p.Id ==
               service_remove.id_User).First();
                MessageBox.Show("Запись удалить нельзя!" + Environment.NewLine + "Существует запись на данную процедуру", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
            catch
            {
                var remove = dGrid.SelectedItems.Cast<Teachers>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {remove.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var context = JournalEntities.GetContext();

                    foreach (var grade in remove)
                    {
                        var gradeToRemove = context.Teachers.Find(grade.Id);
                        if (gradeToRemove != null)
                        {
                            context.Teachers.Remove(gradeToRemove);
                            context.SaveChanges();
                            dGrid.ItemsSource = JournalEntities.GetContext().Teachers.ToList();
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

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                dGrid.ItemsSource = JournalEntities.GetContext().Teachers.ToList();
        }
    }

}
