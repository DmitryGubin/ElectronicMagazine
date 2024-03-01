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
using System.Xml;

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
/*            ShowStudentGrades(dis);*/

            ImagePhoto(student);
            this.Loaded += (s, e) => ShowStudentGrades(dis);


        }

        public void ImagePhoto(Students student)
        {

            if (student != null)
            {
                if (!string.IsNullOrEmpty(student.PhotoPath))
                {
                    ImageStudens.Source = new BitmapImage(new Uri(student.PhotoPath));
                }
                else
                {
                    ImageStudens.Source = new BitmapImage(new Uri("C:\\Users\\gubin\\Desktop\\ElectronicMagazine\\ElectronicMagazine\\StudensPhoto\\user.png"));
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
            var linq = from attr in entities.Grades
                       where attr.Id_Студента == student.Id && attr.Id_Дисциплины == discipline.Id
                       select attr;

            foreach (var item in linq)
            {
                dGrid.Items.Add(item);
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddGrades(student, dis));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dGrid.Items.Clear();
            dGrid.ItemsSource = JournalEntities.GetContext().Grades.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditGrade());
        }
    }
}

