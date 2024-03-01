using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace ElectronicMagazine.Magazine
{
    /// <summary>
    /// Логика взаимодействия для AddGrades.xaml
    /// </summary>
    public partial class AddGrades : Page
    {
        Students students;
        private Grades grades = new Grades();
        Discipline discipline;

        public AddGrades(Students student, Discipline disciplines)
        {
            InitializeComponent();
            students = student;
            discipline = disciplines;
            DataContext = grades;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            DateTime date = DateTime.Now;

            var newGrade = new Grades()
            {
                Id_Студента = students.Id,
                Id_Дисциплины = discipline.Id,
                Оценка = grades.Оценка,
                Дата_оценки = date,
            };



            if (grades.Id == 0)
                JournalEntities.GetContext().Grades.Add(newGrade);

            try
            {
                JournalEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
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
