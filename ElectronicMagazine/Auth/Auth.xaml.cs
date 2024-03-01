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
            user = entities.Users.Where(p => p.Login == login && p.Password == password).FirstOrDefault();
            if (user != null && user.id_Role == 1)
            {
                MessageBox.Show("Вы вошли как " + user.Login);
                var window1 = new Window1();
                window1.Show();
                this.Close();
            }
            else if (user != null)
            {
                Profilb.lograz = user.Name;
                MessageBox.Show("Вы вошли как " + user.Login);
                var window1 = new Window1();

                window1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ввели неверные логин или пароль");
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
