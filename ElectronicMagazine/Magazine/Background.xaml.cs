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
        JournalEntities entities = new JournalEntities();
        int _dis;
        int _group;
        int _idTeacher;
        string disciplineName;


        public Background(int group, int discipline, int idTeacher)
        {
            _dis = discipline;
            _group = group;
            _idTeacher = idTeacher;
            InitializeComponent();

            var service = new Discipline();

            var disName = entities.Discipline.FirstOrDefault(t => t.Id == _dis);

            disciplineName = disName.Дисциплина;

            Manager.MainFrame = MainFrame;

            Manager.MainFrame.Navigate(new JournalPage(group, disciplineName));

            sDiscipline.Content = disciplineName;


        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {

            var window = new MainMenu(_idTeacher);

            window.Show();
            this.Close();
        }
    }
}
