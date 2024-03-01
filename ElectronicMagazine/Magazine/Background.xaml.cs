using ElectronicMagazine.Menu;
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

namespace ElectronicMagazine.Magazine
{
    /// <summary>
    /// Логика взаимодействия для Background.xaml
    /// </summary>
    public partial class Background : Window
    {
        public Background(int group, string discipline)
        {
            InitializeComponent();

            Manager.MainFrame = MainFrame;

            Manager.MainFrame.Navigate(new JournalPage(group, discipline));

            sDiscipline.Content = TitleDiscipline.Name;

        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            var window = new MainMenu();

            window.Show();
            this.Close();
        }
    }
}
