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
    /// Логика взаимодействия для PageEditUsers.xaml
    /// </summary>
    public partial class PageEditUsers : Page
    {
        JournalEntities entities = new JournalEntities();
        Users users;
        public PageEditUsers(Users user_)
        {

            InitializeComponent();
            List<Role> disciplines = entities.Role.ToList();
            foreach (var discipline in entities.Role)
                ComboDis.Items.Add(discipline);

            if (user_ != null)
            {

                users = user_;
                int index = disciplines.FindIndex(d => d.Id == users.id_Role);
                if (index != -1)
                {
                    ComboDis.SelectedIndex = index;
                }
            }
            else
            {
                users = new Users();
            }

            DataContext = users;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var context = JournalEntities.GetContext();
            if (users.Id == 0)
                JournalEntities.GetContext().Users.Add(users);

            try
            {
                var selectedDiscipline = ComboDis.SelectedItem as Role;
                users.Role = context.Role.FirstOrDefault(d => d.Id == selectedDiscipline.Id);
                JournalEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
