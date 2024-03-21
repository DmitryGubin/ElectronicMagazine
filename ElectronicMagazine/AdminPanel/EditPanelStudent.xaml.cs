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
using System.Xml;

namespace ElectronicMagazine.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для EditPanelStudent.xaml
    /// </summary>
    public partial class EditPanelStudent : Page
    {
        private Students students = new Students();
        JournalEntities entities = new JournalEntities();
        public EditPanelStudent(Students students_)
        {
            InitializeComponent();
            foreach (var classes in entities.Classes)
                ComboClass.Items.Add(classes);

            if (students_ != null)
            {
                students = students_;
            }
            else
            {
                students = new Students();
            }

            DataContext = students;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (students.Id == 0)
                JournalEntities.GetContext().Students.Add(students);

            try
            {
                students.Дата_рождения = DateTime.Parse(DataPicerBirthday.Text);
                JournalEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void PhotoAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
                    TextPhoto.Text = dlg.FileName.ToString();
                students.PhotoPath = dlg.FileName;
            }
            catch 
            {
                MessageBox.Show("Не верный формат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
