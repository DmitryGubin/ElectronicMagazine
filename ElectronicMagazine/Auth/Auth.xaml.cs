using ElectronicMagazine.AdminPanel;
using ElectronicMagazine.CaphaWin;
using ElectronicMagazine.Menu;
using ElectronicMagazine.PageUsers.ProfileSudents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        private int loginAttempts = 0;
        private bool loginBlocked = false;
        JournalEntities entities = new JournalEntities();
        public Auth()
        {
            InitializeComponent();
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
            LoginUser();
        }

            private void Registration_Click(object sender, RoutedEventArgs e)
            {
                var window1 = new Registration();
                window1.Show();
                this.Close();
            }

        private async void LoginUser() 
        {
            if (loginBlocked)
            {
                MessageBox.Show("Подождите несколько секунд перед следующей попыткой входа.");
                return;
            }
            if (LoginBox.Text == "" || PasswordBox.Password == "")
                MessageBox.Show("Введите логин и пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            try
            {
                string login = LoginBox.Text.Trim();
                string password = PasswordBox.Password.Trim();
                string hashedPassword = HashPassword(password);
                Role role = new Role();
                Users user = new Users();
                Teachers teachers = new Teachers();
                Discipline disciplines = new Discipline();
                if (LoginBox.Text == "admin")
                {
                    user = entities.Users.Where(p => p.Login == login && p.Password == password).FirstOrDefault();
                }
                else
                {
                    user = entities.Users.Where(p => p.Login == login && p.Password == hashedPassword).FirstOrDefault();
                }

                if (user != null && user.id_Role == 1)
                {
                    MessageBox.Show("Вы вошли как " + user.Login, "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    var window1 = new BackgroundPanel();
                    window1.Show();
                    this.Close();
                }
                else if (user != null && user.id_Role == 2)
                {
                    var teacher = entities.Teachers.FirstOrDefault(t => t.id_User == user.Id);
                    if (teacher != null)
                    {
                        var discipline = entities.Discipline.FirstOrDefault(d => d.Id == teacher.Id_Дисциплины);
                        if (discipline != null)
                        {
                            Profilb.lograz = user.Name;
                            var disciplineText = discipline.Дисциплина;

                            MessageBox.Show("Вы вошли как " + user.Login, "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                            var window1 = new MainMenu(disciplineText);

                            window1.Show();
                            this.Close();
                        }
                    }
                }
                else if (user != null && user.id_Role == 3)
                {
                    var students = entities.Students.FirstOrDefault(t => t.Id_User == user.Id);
                    int studentsId = user.Students.FirstOrDefault()?.Id ?? 0;
                    if (students != null)
                    {

                        Profilb.lograz = user.Name;

                        MessageBox.Show("Вы вошли как " + user.Login, "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        var window1 = new SudentProf(studentsId);
                        window1.Show();
                        this.Close();
                    }
                }
                else
                {
                    loginAttempts++;
                    if (loginAttempts == 3)
                    {
                        var window1 = new WindowCapha();
                        window1.Show();
                    }

                    else if (loginAttempts >= 4)
                    {
                        loginBlocked = true;
                        MessageBox.Show("Вы ввели неверные логин или пароль. Попробуйте снова через 10 секунд.");
                        await Task.Delay(10000);
                        loginAttempts = 0;
                        loginBlocked = false;
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверные логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch { }
        }
        }
    }  
