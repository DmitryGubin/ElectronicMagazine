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
using System.Windows.Shapes;

namespace ElectronicMagazine.PageUsers.ProfileSudents
{
    /// <summary>
    /// Логика взаимодействия для SudentProf.xaml
    /// </summary>
    public partial class SudentProf : Window
    {
        JournalEntities entities = new JournalEntities();
        private int _studentsId;
        public SudentProf(int studentsId)
        {
            InitializeComponent();
            foreach(var item in entities.Discipline)
                cDiscipline.Items.Add(item);
            _studentsId = studentsId;
            cDiscipline.SelectionChanged += (s, e) => ShowGrades();
            Update();
            ImagePhoto();
        }
        public void Update()
        {
            
            var student = entities.Students.FirstOrDefault(s => s.Id == _studentsId);

            if (student != null)
            {
                
                TitleName.Content = student.Имя;
                TitleSecondName.Content = student.Фамилия;
                Birthday.Content = student.Дата_рождения;
            }
        }
        private void ShowGrades()
        {
            
            var selectedDiscipline = cDiscipline.SelectedItem as Discipline;
            if (selectedDiscipline == null)
            {
                MessageBox.Show("Предмет не выбран");
                return;
            }

            
            var selectedStudent = entities.Students.FirstOrDefault(s => s.Id == _studentsId);
            if (selectedStudent == null)
            {
                MessageBox.Show("Студент не выбран");
                return;
            }

            
            var grades = entities.Grades
                .Where(g => g.Id_Студента == selectedStudent.Id && g.Id_Дисциплины == selectedDiscipline.Id)
                .ToList();

            if (grades != null && grades.Any())
            {
                double averageGrade = grades.Average(g => Convert.ToDouble(g.Оценка));
                averageGrade = Math.Round(averageGrade, 1);
                LableAverage.Content = averageGrade;
            }
            else
            {
                LableAverage.Content = "0";
            }

            
            dGrid.ItemsSource = grades;
            dGrid.Items.Refresh(); 
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var win = new Auth.Auth();
            win.Show();
            this.Close();
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public void ImagePhoto()
        {
            var student = entities.Students.FirstOrDefault(s => s.Id == _studentsId);
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string imagePath = System.IO.Path.Combine(projectPath, "StudensPhoto", "user.png");

            if (student != null)
            {
                if (!string.IsNullOrEmpty(student.PhotoPath))
                {
                    string imageStudents = System.IO.Path.Combine(projectPath, student.PhotoPath);
                    ImageStudens.Source = new BitmapImage(new Uri(imageStudents));
                }
                else
                {
                    ImageStudens.Source = new BitmapImage(new Uri(imagePath));
                }
            }

        }
    }
}
