using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

namespace ElectronicMagazine.Magazine
{
    /// <summary>
    /// Логика взаимодействия для Journal.xaml
    /// </summary>
    public partial class JournalPage : Page
    {
        JournalEntities entities = new JournalEntities();
        string discipline;
        int group;
        private List<Students> students;

        public JournalPage(int group1, string discipline)
        {
            InitializeComponent();

            this.discipline = discipline;
            this.group = group1;

            Manager.MainFrame = Manager.MainFrame;

            var matches = from attr in entities.Students
                          where attr.Id_Класса == group1
                          orderby attr.Фамилия, attr.Имя
                          select attr;

            foreach (var student in matches)
            {
                ListBoxStudens.Items.Add(student);
            }

            this.students = matches.ToList();
            DataTable dataTable = GetDataTable(matches.ToList());
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private DataTable GetDataTable(List<Students> students)
        {
            DataTable table = new DataTable();

            DataColumn orderColumn = new DataColumn("Order", typeof(string));
            orderColumn.MaxLength = 100;
            //orderColumn.ColumnName = "#";
            table.Columns.Add(orderColumn);

            DataColumn cl = new DataColumn("Studend", typeof(string));
            cl.MaxLength = 100;
            //cl.ColumnName = "Ученик";
            table.Columns.Add(cl);
            var dateGrades = students.Select(s => s.Grades.Select(g => g.Дата_оценки.Value.Date).ToList())
                .SelectMany(g => g)
                .Distinct()
                .OrderBy(d => d)
                .ToList();
            dateGrades.ForEach(d =>
            {
                DataColumn dateColumn = new DataColumn(d.ToShortDateString().Replace('.', '-'), typeof(string));
                dateColumn.MaxLength = 100;
                table.Columns.Add(dateColumn);
            });

            for (int i = 0; i < students.Count; i++)
            {
                var s = students[i];
                DataRow rw = table.NewRow();
                table.Rows.Add(rw);
                rw["Order"] = i + 1;
                rw["Studend"] = s.Фамилия + ' ' + s.Имя;
                var grades = dateGrades.Select(d =>
                {
                    if (s.Grades.Count == 0)
                    {
                        return null;
                    }
                    var grade = s.Grades.Where(g => g.Дата_оценки.Value.Date.Equals(d)).FirstOrDefault();
                    if (grade == null || grade.Оценка == null)
                    {
                        return null;
                    }
                    else
                    {

                        return grade.Оценка.ToString();
                    }
                })
                    .Select(d => d == null ? "" : d)
                    .ToArray();
                var item = new object[] { i + 1, s.Фамилия + ' ' + s.Имя };
                item = item.Concat(grades).ToArray();
                rw.ItemArray = item;
            }

            orderColumn.ReadOnly = true;
            cl.ReadOnly = true;
            return table;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ListBoxStudens.SelectedItem as Students;

            DateTime date = (DateTime)(selected.Дата_рождения);

            string dateBirth = date.ToString("dd.MM.yyyy");

            StudentsProf.FirstName = selected.Имя;
            StudentsProf.SecondName = selected.Фамилия;
            StudentsProf.Birthday = dateBirth;
            StudentsProf.Class = selected.Id_Класса.ToString();


            StudentId.IdStudent = selected.Id.ToString();
            StudentId.IdClasses = selected.Id_Класса.ToString();
            StudentId.IdDiscipline = selected.Grades.ToString();





            var linq = (from attr in entities.Discipline where attr.Дисциплина == discipline select attr).Single();
            Manager.MainFrame.Navigate(new StudentsAssessment(linq, (ListBoxStudens.SelectedItem as Students)));

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (e.OriginalSource as DataGrid).SelectedItem;
            var b = (e.OriginalSource as DataGrid).SelectedIndex;
            var c = (e.OriginalSource as DataGrid).CurrentItem;
            if (a is DataRowView && b > 2)
            {
                var newGrade = (a as DataRowView)[b];
                var d = 0;
            }
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dataGrid_CurrentCellChanged_1(object sender, EventArgs e)
        {
            var d = 0;
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    int rowIndex = e.Row.GetIndex();
                    var el = e.EditingElement as TextBox;
                    var ci = new CultureInfo("ru-RU");

                    var date = DateTime.ParseExact(column.Header.ToString(), "dd-MM-yyyy", ci);
                    var newGrade = el.Text;
                    var student = students[rowIndex];

                    MessageBox.Show(student.Фамилия + "" + student.Имя + " " + newGrade + " " + date);
                    //Нужно добавить сохранение

                    // rowIndex has the row index
                    // bindingPath has the column's binding
                    // el.Text has the new, user-entered value

                }
            }
        }
    }
}
