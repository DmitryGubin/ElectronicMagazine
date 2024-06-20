using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace ElectronicMagazine.PageUsers.ProfileSudents
{
    /// <summary>
    /// Логика взаимодействия для SudentProf.xaml
    /// </summary>
    public partial class SudentProf : Window
    {

        private MessageTemplateSelector viewModel;
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();
        private JournalEntities entities = new JournalEntities();
        public int _studentsId;
        private int _currentTeacherId;

        private Auth.Auth loginWindow;

        public SudentProf(Auth.Auth loginWindow, int studentsId)
        {

            InitializeComponent();
            _studentsId = studentsId;
            this.loginWindow = loginWindow;
            viewModel = new MessageTemplateSelector
            {
                StudentsId = _studentsId  // Установка _studentsId в MessageTemplateSelector
            };
            foreach (var item in entities.Discipline)
                cDiscipline.Items.Add(item);
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
                System.Windows.MessageBox.Show("Предмет не выбран");
                return;
            }

            var selectedStudent = entities.Students.FirstOrDefault(s => s.Id == _studentsId);
            if (selectedStudent == null)
            {
                System.Windows.MessageBox.Show("Студент не выбран");
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

        private void LoadMessages()
        {
            var selectedDiscipline = cDiscipline.SelectedItem as Discipline;
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
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            SudentProf mainWindow = App.GetMainWindowInstance();

            if (mainWindow == null)
            {
                System.Windows.MessageBox.Show("Ошибка при отправке сообщения: невозможно получить доступ к основному окну.");
                return;
            }
            string newMessageText = MessageBox.Text.Trim();
            if (string.IsNullOrEmpty(newMessageText))
            {
                System.Windows.MessageBox.Show("Введите текст сообщения.");
                return;
            }

            var selectedDiscipline = cDiscipline.SelectedItem as Discipline;
            if (selectedDiscipline != null)
            {
                int disciplineId = selectedDiscipline.Id;

                using (var entities = new JournalEntities())
                {
                    var teacherId = entities.DisTeacher
                                            .Where(dt => dt.Id_Dis == disciplineId)
                                            .Select(dt => dt.Id_Teacher)
                                            .FirstOrDefault();

                    var student = entities.Students.FirstOrDefault(s => s.Id == _studentsId);

                    Message newMessage = new Message
                    {
                        Id_Student = _studentsId,
                        Id_Teacher = (int)teacherId,
                        Report = newMessageText,
                        Author = student?.Имя + " " + student?.Фамилия,
                        Date = DateTime.Now
                    };

                    entities.Message.Add(newMessage);
                    entities.SaveChanges();

                    messages.Add(newMessage);
                    MessageBox.Clear();
                }
            }
        }
        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.None)
            {
                SendButton_Click(sender, e);
                e.Handled = true;
            }
        }

        private void cDiscipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadMessages();
        }



    }
}
