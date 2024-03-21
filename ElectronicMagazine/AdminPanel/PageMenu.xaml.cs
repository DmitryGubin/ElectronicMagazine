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

namespace ElectronicMagazine.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void edtStud(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditStudents());
        }

        private void editTeacher(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PanelTeacher());
        }

        private void editUser(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageUser());
        }

        private void editRole(object sender, RoutedEventArgs e)
        {

        }
    }
}
