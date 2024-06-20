using System.Windows;
using System.Windows.Controls;

namespace ElectronicMagazine.PageUsers.ProfileTeacher
{
    public class TeacherMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate TeacherTemplate { get; set; }

        public int TeacherId { get; set; }  // Идентификатор преподавателя

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as Message;
            if (message == null)
                return base.SelectTemplate(item, container);

            if (message.Id_Teacher == TeacherId)
                return TeacherTemplate;
            else
                return StudentTemplate;
        }
    }
}