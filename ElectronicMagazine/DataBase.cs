using System;

namespace ElectronicMagazine
{
    public partial class Students 
    {
        public override string ToString()
        {
            return Фамилия + " " + Имя;
        }
    }
    public partial class Grades 
    {
        public override string ToString() 
        {
            return Id_Дисциплины.ToString();
        }
    }
    public partial class Discipline 
    {
        public override string ToString()
        {
            return Дисциплина;
        }
    }

    partial class JournalEntities : System.Data.Entity.DbContext
    {
        public static JournalEntities _context;
        public static JournalEntities GetContext()
        {
            if (_context == null)
            {
                _context = new JournalEntities();
            }
            return _context;
        }
    }
    public partial class Classes 
    {
        public override string ToString()
        {
            return Класс.ToString();
        }
    }

    public partial class Teachers
    {
        public override string ToString()
        {
            return Имя_ + " " + Фамилия;
        }
    }
    public partial class Users
    {
        public override string ToString()
        {
            return Name + " " + Login + "-" + Password;
        }
    }
    public partial class Role
    {
        public override string ToString()
        {
            return Role1;
        }
    }
    public static class Profilb
    {
        public static string loginusers { get; set; }
        public static string lograz { get; set; }
        public static string SeconName { get; set; }
        public static string Discipline { get; set; }

    }

    public static class StudentsProf
    {
        public static string FirstName { get; set; }
        public static string SecondName { get; set; }
        public static string Birthday { get; set;}
        public static string Class { get; set;}
    }

    public static class TitleDiscipline 
    {
        public static string Name { get; set;  }
    }

    public static class StudentId
    {
        public static string IdStudent { get; set; }
        public static string IdDiscipline { get; set; }
        public static string IdTeachers { get; set; }
        public static string IdClasses { get; set; }

    }
}
