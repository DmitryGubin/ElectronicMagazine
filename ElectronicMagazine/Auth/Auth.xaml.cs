using ElectronicMagazine.AdminPanel;
using ElectronicMagazine.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        JournalEntities entities = new JournalEntities();
        public Auth()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            Role role = new Role();
            Users user = new Users();
            Teachers teachers = new Teachers();
            Discipline disciplines = new Discipline();
            user = entities.Users.Where(p => p.Login == login && p.Password == password).FirstOrDefault();
            if (LoginBox.Text == "" || PasswordBox.Password == "")
                MessageBox.Show("Введите логин и пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (user != null && user.id_Role == 1)
                {
                    MessageBox.Show("Вы вошли как " + user.Login, "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    var window1 = new BackgroundPanel();
                    window1.Show();
                    this.Close();
                }
                else if (user != null)
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
                else
                {
                    PasswordBox.Password = "";
                    MessageBox.Show("Вы ввели неверные логин или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Registration();
            window1.Show();
            this.Close();
        }
    }
}
