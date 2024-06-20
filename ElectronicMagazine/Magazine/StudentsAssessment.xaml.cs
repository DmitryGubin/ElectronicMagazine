using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;

namespace ElectronicMagazine.Magazine
{
    /// <summary>
    /// Логика взаимодействия для StudentsAssessment.xaml
    /// </summary>
    public partial class StudentsAssessment : Page
    {
        JournalEntities entities = new JournalEntities();
        Students student;
        Discipline dis;


        public StudentsAssessment(Discipline discipline, Students students)
        {
            InitializeComponent();
            student = students;
            dis = discipline;
            Update();

            ImagePhoto(student);
        }

        public void ImagePhoto(Students student)
        {
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
        public void Update()
        {
            TitleName.Content = StudentsProf.FirstName;
            TitleSecondName.Content = StudentsProf.SecondName;
            Birthday.Content = StudentsProf.Birthday;
        }

        private void ShowStudentGrades(Discipline discipline)
        {

            var grades = entities.Grades
                    .Where(attr => attr.Id_Студента == student.Id && attr.Id_Дисциплины == discipline.Id)
                    .ToList();


            if (grades != null && grades.Any())
            {
                double averageGrade = grades.Average(attr => Convert.ToDouble(attr.Оценка));

                averageGrade = Math.Round(averageGrade, 1);

                LableAverage.Content = averageGrade;
            }
            else
                LableAverage.Content = "0";

            dGrid.ItemsSource = grades;
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddGrades(student, dis, null));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedGrades = dGrid.SelectedItems.Cast<Grades>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {selectedGrades.Count} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var context = JournalEntities.GetContext();
                bool hasErrorOccurred = false;

                foreach (var grade in selectedGrades)
                {
                    var gradeToRemove = context.Grades.Find(grade.Id);
                    if (gradeToRemove != null)
                    {
                        context.Grades.Remove(gradeToRemove);
                    }
                    else
                    {
                        hasErrorOccurred = true;
                    }
                }

                try
                {
                    context.SaveChanges();
                    dGrid.ItemsSource = context.Grades.ToList();
                    ShowStudentGrades(dis);
                    MessageBox.Show("Данные успешно удалены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка при удалении данных из базы!", "Провал!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (hasErrorOccurred)
                {
                    MessageBox.Show("Некоторые записи не были найдены и не удалены!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                dGrid.Items.Refresh();
            }
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Loaded += (s, q) => ShowStudentGrades(dis);
            dGrid.ItemsSource = JournalEntities.GetContext().Grades.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var soft_remove = dGrid.SelectedItems.Cast<Grades>().ToList();


            var context = JournalEntities.GetContext();

            foreach (var grade in soft_remove)
            {
                var gradeToRemove = context.Grades.Find(grade.Id);
                if (gradeToRemove != null)
                {
                    context.Grades.Remove(gradeToRemove);
                    context.SaveChanges();
                    dGrid.ItemsSource = JournalEntities.GetContext().Grades.ToList();
                    ShowStudentGrades(dis);
                }

            }

            Manager.MainFrame.Navigate(new AddGrades(student, dis, (sender as Button).DataContext as Grades));
            dGrid.Items.Refresh();

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                this.Loaded += (s, q) => ShowStudentGrades(dis);
                dGrid.ItemsSource = JournalEntities.GetContext().Grades.ToList();
            }
        }
    }
}


