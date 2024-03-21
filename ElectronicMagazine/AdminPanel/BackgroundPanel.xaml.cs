using ElectronicMagazine.Magazine;
using ElectronicMagazine.Menu;
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
using System.Windows.Shapes;

namespace ElectronicMagazine.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для BackgroundPanel.xaml
    /// </summary>
    public partial class BackgroundPanel : Window
    {
        public BackgroundPanel()
        {
            InitializeComponent();

            InitializeComponent();

            Manager.MainFrame = MainFrame;

            Manager.MainFrame.Navigate(new PageMenu());
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            var window1 = new Auth.Auth();
            window1.Show();
            this.Close();
        }
    }
}
