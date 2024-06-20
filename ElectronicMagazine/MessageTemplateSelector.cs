using System.Windows;
using System.Windows.Controls;

namespace ElectronicMagazine.PageUsers.ProfileSudents
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate TeacherTemplate { get; set; }

        public int StudentsId { get; set; }  // Свойство зависимости для передачи _studentsId
        private Auth.Auth loginWindow;

        public MessageTemplateSelector(Auth.Auth loginWindow)
        {
            this.loginWindow = loginWindow;
        }
        public MessageTemplateSelector()
        {
            // Можно установить значения по умолчанию для свойств, если требуется
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as Message;
            if (message == null)
                return base.SelectTemplate(item, container);

            if (message.Id_Student == StudentsId)
                return StudentTemplate;
            else
                return TeacherTemplate;
        }
    }
}