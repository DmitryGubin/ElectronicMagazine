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

namespace ElectronicMagazine.CaphaWin
{
    /// <summary>
    /// Логика взаимодействия для WindowCapha.xaml
    /// </summary>
    public partial class WindowCapha : Window
    {
        public WindowCapha()
        {
            InitializeComponent();
            Capcha();
        }
        public void Capcha()
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = " ";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            CapchaLable.Content = pwd;
            textBox1.Text = pwd;
        }

        private void ConfirmationCapcha(object sender, RoutedEventArgs e)
        {
            if (TextCapcha.Text == "")
                MessageBox.Show("Пожалуйста, введите капчу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (TextCapcha.Text == textBox1.Text)
                {
                    MessageBox.Show("Успешно!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверная капча", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Capcha();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Capcha();
        }
    }
}
