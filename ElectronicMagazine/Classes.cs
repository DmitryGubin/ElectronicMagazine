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
    
    public partial class Classes
    {
        public Classes()
        {
            this.GroupTeacher = new HashSet<GroupTeacher>();
        }
    
        public int Id { get; set; }
        public string Класс { get; set; }
    
        public virtual ICollection<GroupTeacher> GroupTeacher { get; set; }
        public virtual Students Students { get; set; }
    }
}
