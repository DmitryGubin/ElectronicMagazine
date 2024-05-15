using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace ElectronicMagazine.Auth
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        JournalEntities entities = new JournalEntities();
        public Registration()
        {
            InitializeComponent();

            foreach (var discipline in entities.Discipline)
                ComboBoxDiscipline.Items.Add(discipline);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (LoginBox.Text != "" || PasswordBox.Password != "")
                {
                    Teachers teachers = new Teachers();
                    Users user = new Users();
                    entities.Teachers.Add(teachers);
                    entities.Users.Add(user);
                    foreach (var item in entities.Users)
                    {
                        if (LoginBox.Text == user.Login)
                        {
                            MessageBox.Show("Пользователь с таким именем уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw new Exception();
                        }
                    }
                    user.Login = LoginBox.Text;
                    user.Name = TextName.Text;
                    user.Password = HashPassword(PasswordBox.Password);
                    user.id_Role = 2; // по умолчанию пользователь
                    teachers.Имя_ = TextName.Text;
                    teachers.Фамилия = TextSecondName.Text;
                    teachers.Discipline = ComboBoxDiscipline.SelectedItem as Discipline;
                    teachers.id_User = user.Id;
                    entities.SaveChanges();
                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    var window1 = new Auth();
                    window1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            { }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Auth();
            window1.Show();
            this.Close();
        }
    }
}

