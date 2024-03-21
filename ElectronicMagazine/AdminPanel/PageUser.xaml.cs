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
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        Users users;
        public PageUser()
        {
            InitializeComponent();

            DataContext = users;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickAdd(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new PageEditUsers((sender as Button).DataContext as Users));
            dGrid.Items.Refresh();

        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
                var remove = dGrid.SelectedItems.Cast<Users>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {remove.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var context = JournalEntities.GetContext();

                    foreach (var grade in remove)
                    {
                        var gradeToRemove = context.Users.Find(grade.Id);
                        if (gradeToRemove != null)
                        {
                            context.Users.Remove(gradeToRemove);
                            context.SaveChanges();
                            dGrid.ItemsSource = JournalEntities.GetContext().Users.ToList();
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

        private void Page_IsVisivleChange(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)

                dGrid.ItemsSource = JournalEntities.GetContext().Users.ToList();

        }
    }
}
