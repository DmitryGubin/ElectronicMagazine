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

namespace ElectronicMagazine.Magazine
{
    /// <summary>
    /// Логика взаимодействия для Journal.xaml
    /// </summary>
    public partial class JournalPage : Page
    {
        JournalEntities entities = new JournalEntities();
        string discipline;
        int group;
        public JournalPage(int group1, string discipline)
        {
            InitializeComponent();

            this.discipline = discipline;
            this.group = group1;

            Manager.MainFrame = Manager.MainFrame;

            var matches = from attr in entities.Students where attr.Id_Класса == group1 select attr;

            foreach (var student in matches)
            {
                ListBoxStudens.Items.Add(student);
            }


        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ListBoxStudens.SelectedItem as Students;

            DateTime date = (DateTime)(selected.Дата_рождения);

            string dateBirth = date.ToString("dd.MM.yyyy");

            StudentsProf.FirstName = selected.Имя;
            StudentsProf.SecondName = selected.Фамилия;
            StudentsProf.Birthday = dateBirth;
            StudentsProf.Class = selected.Id_Класса.ToString();


            StudentId.IdStudent = selected.Id.ToString();
            StudentId.IdClasses = selected.Id_Класса.ToString();
            StudentId.IdDiscipline = selected.Grades.ToString();





            var linq = (from attr in entities.Discipline where attr.Дисциплина == discipline select attr).Single();
            Manager.MainFrame.Navigate(new StudentsAssessment(linq, (ListBoxStudens.SelectedItem as Students)));

        }

    }
}
