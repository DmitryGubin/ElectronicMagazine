using ElectronicMagazine.Magazine;
using System.IO;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using ElectronicMagazine.AdminPanel;

namespace ElectronicMagazine.Menu
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        JournalEntities entities = new JournalEntities();
        string disciplines;
        public MainMenu(string discipline)
        {
            this.disciplines = discipline;

            InitializeComponent();
            foreach(var classes in entities.Classes)
                ComboBoxClass.Items.Add(classes);

            LableFirstName.Content = Profilb.lograz;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ComboBoxClass.SelectedItem as Classes;

            TitleDiscipline.Name = disciplines;

            var window = new Background(int.Parse(selectedItem.Класс.ToString()), disciplines);
            
            window.Show();
            this.Close();
        }

        private void ComboBoxClass_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Auth.Auth();
            window1.Show();
            this.Close();
        }
    }
}
