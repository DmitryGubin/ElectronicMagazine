using ElectronicMagazine.PageUsers.ProfileSudents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicMagazine
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SudentProf mainWindowInstance;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Предположим, что у вас есть экземпляр Auth (LoginWindow), назовем его loginWindow
            Auth.Auth loginWindow = new Auth.Auth(); // Здесь создайте экземпляр вашего окна авторизации

            // Передайте loginWindow и studentsId в конструктор SudentProf
            int studentsId = 123; // Пример идентификатора студента
            mainWindowInstance = new SudentProf(loginWindow, studentsId);
            mainWindowInstance.Show();


            mainWindowInstance.Hide();
            loginWindow.Hide();
        }

        public static SudentProf GetMainWindowInstance()
        {
            return mainWindowInstance;
        }
    }


}
