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
            apdute();

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
        public void apdute()
        {
            TitleName.Content = StudentsProf.FirstName;
            TitleSecondName.Content = StudentsProf.SecondName;
            TitleClass.Content = StudentsProf.Class;
            Birthday.Content = StudentsProf.Birthday;
        }

        private void ShowStudentGrades(Discipline discipline)
        {

            var grades = entities.Grades
                    .Where(attr => attr.Id_Студента == student.Id && attr.Id_Дисциплины == discipline.Id)
                    .ToList();

            dGrid.ItemsSource = grades;
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddGrades(student, dis, null));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var soft_remove = dGrid.SelectedItems.Cast<Grades>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {soft_remove.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
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


