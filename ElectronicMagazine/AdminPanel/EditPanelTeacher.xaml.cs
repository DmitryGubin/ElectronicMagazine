using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для EditPanelTeacher.xaml
    /// </summary>
    public partial class EditPanelTeacher : Page
    {
        JournalEntities entities = new JournalEntities();
        Teachers teachers;
        public EditPanelTeacher(Teachers teacher_)
        {

            List<Discipline> disciplines = entities.Discipline.ToList();
            InitializeComponent();
            foreach (var discipline in entities.Discipline)
                ComboDis.Items.Add(discipline);

            if (teacher_ != null)
            {
                
                teachers = teacher_;
                int index = disciplines.FindIndex(d => d.Id == teachers.Id_Дисциплины);
                if (index != -1)
                {
                    ComboDis.SelectedIndex = index;
                }
            }
            else
            {
                teachers = new Teachers();
            }

            DataContext = teachers;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var context = JournalEntities.GetContext();
            if (teachers.Id == 0)
                JournalEntities.GetContext().Teachers.Add(teachers);

            try
            {
                var selectedDiscipline = ComboDis.SelectedItem as Discipline;
                teachers.Discipline = context.Discipline.FirstOrDefault(d => d.Id == selectedDiscipline.Id);
                JournalEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
