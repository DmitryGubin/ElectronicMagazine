using ElectronicMagazine.Magazine;
using System.IO;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using ElectronicMagazine.AdminPanel;
using ElectronicMagazine.PageUsers.ProfileTeacher;
using System.Linq;

namespace ElectronicMagazine.Menu
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        JournalEntities entities = new JournalEntities();
        int _idTeacher;
        public MainMenu(int idTeacher)
        {
            _idTeacher = idTeacher;

            InitializeComponent();

            ComboBoxDis.Items.Clear();



            var groupId = entities.GroupTeacher
                            .Where(dt => dt.Id_Teacher == _idTeacher)
                            .Select(dt => dt.Id_Group)
                            .ToList();

            var groups = entities.Classes
                          .Where(d => groupId.Contains(d.Id))
                          .ToList();


            foreach (var classes in groups)
                ComboBoxClass.Items.Add(classes);



            var disciplineIds = entities.DisTeacher
                                        .Where(dt => dt.Id_Teacher == _idTeacher)
                                        .Select(dt => dt.Id_Dis)
                                        .ToList();

            var disciplines = entities.Discipline
                          .Where(d => disciplineIds.Contains(d.Id))
                          .ToList();


            foreach (var dis in disciplines)
                ComboBoxDis.Items.Add(dis);
            


            LableFirstName.Content = Profilb.lograz;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItemGroup = ComboBoxClass.SelectedItem as Classes;
            var selectedItemDis = ComboBoxDis.SelectedItem as Discipline;


            if (selectedItemDis == null)
            {
                MessageBox.Show("Выберите дисциплину", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var window = new Background(int.Parse(selectedItemGroup.Id.ToString()), int.Parse(selectedItemDis.Id.ToString()), _idTeacher);
                window.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Auth.Auth();
            window1.Show();
            this.Close();
        }

        private void btnProf_Click(object sender, RoutedEventArgs e)
        {
            TeacherProf window1 = new TeacherProf(_idTeacher);
            window1.Show();
            this.Close();
        }
    }
}
