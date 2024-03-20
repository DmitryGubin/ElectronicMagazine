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
        Discipline discipline;
        private Grades grades = new Grades();

        public AddGrades(Students student, Discipline disciplines, Grades grades_)
        {
            InitializeComponent();
            if (grades_ != null)
            {
                grades = grades_;
            }
            else 
            {
                grades = new Grades();
            }
            students = student;
            discipline = disciplines;
            DataContext = grades;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            DateTime date = DateTime.Now;

            try
            {
                int grade_;
                if (!int.TryParse(grades.Оценка.ToString(), out grade_) || int.Parse(TextBoxGrade.Text) > 5 || int.Parse(TextBoxGrade.Text) < 1)
                    MessageBox.Show("Неверный формат данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                else
                {
                    if (grades.Id == 0)
                    {
                        var newGrade = new Grades()
                        {
                            Id_Студента = students.Id,
                            Id_Дисциплины = discipline.Id,
                            Оценка = grades.Оценка,
                            Дата_оценки = date,
                        };

                        JournalEntities.GetContext().Grades.Add(newGrade);

                    }
                    try
                    {
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
            catch 
            {
                MessageBox.Show("Неверный формат данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
