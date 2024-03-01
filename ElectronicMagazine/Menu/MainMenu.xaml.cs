using ElectronicMagazine.Magazine;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ElectronicMagazine.Menu
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        JournalEntities entities = new JournalEntities();
        public MainMenu()
        {
            InitializeComponent();
            foreach(var classes in entities.Classes)
                ComboBoxClass.Items.Add(classes);
            foreach (var discipline in entities.Discipline)
                ComboBoxDiscipline.Items.Add(discipline);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ComboBoxClass.SelectedItem as Classes;
            var selctedItemDiscipline = ComboBoxDiscipline.SelectedItem as Discipline;

            TitleDiscipline.Name = selctedItemDiscipline.Дисциплина;

            var window = new Background(int.Parse(selectedItem.Класс.ToString()), selctedItemDiscipline.Дисциплина);
            
            window.Show();
            this.Close();
        }

        private void ComboBoxClass_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }
    }
}
