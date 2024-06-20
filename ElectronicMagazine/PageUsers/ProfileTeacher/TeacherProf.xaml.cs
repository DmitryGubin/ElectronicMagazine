using ElectronicMagazine.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace ElectronicMagazine.PageUsers.ProfileTeacher
{
    /// <summary>
    /// Логика взаимодействия для TeacherProf.xaml
    /// </summary>
    public partial class TeacherProf : Window
    {
        JournalEntities entities = new JournalEntities();
        int _idTeacher;
        public TeacherProf(int idTeacher)
        {
            _idTeacher = idTeacher;
            InitializeComponent();
            ImagePhoto();
            Update();
            UpdateListView();
        }


        private void UpdateListView()
        {
            MessageListBox.ItemsSource = entities.Message.ToList();
        }

        public void Update()
        {

            var teachers = entities.Teachers.FirstOrDefault(s => s.Id == _idTeacher);

            if (teachers != null)
            {

                TitleName.Content = teachers.Имя_;
                TitleSecondName.Content = teachers.Фамилия;
            }
            else 
            {
                TitleName.Content = "";
                TitleSecondName.Content = "";
            }
        }

        public void ImagePhoto()
        {
            var student = entities.Teachers.FirstOrDefault(s => s.Id == _idTeacher);
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var win = new Menu.MainMenu(_idTeacher);
            win.Show();
            this.Close();
        }

        private void MessageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       /* private void LoadMessages()
        {
            if (selectedDiscipline != null)
            {
                int disciplineId = selectedDiscipline.Id;

                using (var entities = new JournalEntities())
                {
                    var teacherId = entities.DisTeacher
                                            .Where(dt => dt.Id_Dis == disciplineId)
                                            .Select(dt => dt.Id_Teacher)
                                            .FirstOrDefault();

                    _currentTeacherId = (int)teacherId;

                    var messagesFromDb = entities.Message
                                                .Where(m => m.Id_Student == _studentsId && m.Id_Teacher == teacherId)
                    .ToList();

                    messages.Clear();
                    foreach (var message in messagesFromDb)
                    {
                        messages.Add(message);
                    }

                    MessageListBox.ItemsSource = messages;
                }
            }
        }*/

    }
}
