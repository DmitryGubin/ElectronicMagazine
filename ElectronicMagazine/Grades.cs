//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectronicMagazine
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grades
    {
        public int Id { get; set; }
        public Nullable<int> Id_Студента { get; set; }
        public Nullable<int> Id_Учителя { get; set; }
        public Nullable<int> Id_Класса { get; set; }
        public Nullable<int> Id_Дисциплины { get; set; }
        public Nullable<System.DateTime> Дата_оценки { get; set; }
        public Nullable<int> Оценка { get; set; }
    
        public virtual Classes Classes { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Students Students { get; set; }
        public virtual Teachers Teachers { get; set; }
    }
}
