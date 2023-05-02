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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Firmware.xaml
    /// </summary>
    public partial class Firmware : Window
    {
        private int f = 0;
        public Firmware()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.MaxHeight = 760;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (f == 0)
            {
            
                rect_tohide.Visibility = Visibility.Collapsed;
                f = 1;
                arrow_Swap.Source = BitmapFrame.Create(new Uri(@"C:\arrowDown.png"));
                Application.Current.MainWindow = this;
                Application.Current.MainWindow.Height = 555;

                /*  lastCh.MaxHeight = 10;
                  preLast.MaxHeight = 120;*/
                rect_try.Margin = new Thickness(53, 57, 0, 0);

                line1.X1 = 15;
                line1.X2 = 15;
                line1.Y1 = 480;
                line1.Y2 = 500;

               
                line2.X1 = 15;
                line2.X2 = 1400;
                line2.Y1 = 480;
                line2.Y2 = 480;

               
                line3.X1 = 15;
                line3.X2 = 1400;
                line3.Y1 = 500;
                line3.Y2 = 500;
              
                line4.X1 = 40;
                line4.X2 = 40;
                line4.Y1 = 0;
                line4.Y2 = 480;

            }
            else
            {
                rect_tohide.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Height = 760;
                rect_try.Margin = new Thickness(50, 262, 0, 0);
              
                line1.X1 = 15;
                line1.X2 = 15;
                line1.Y1 = 685;
                line1.Y2 = 705;
              

                line2.X1 = 15;
                line2.X2 = 1400;
                line2.Y1 = 685;
                line2.Y2 = 685;


                line3.X1 = 15;
                line3.X2 = 1400;
                line3.Y1 = 705;
                line3.Y2 = 705;

                line4.X1 = 40;
                line4.X2 = 40;
                line4.Y1 = 0;
                line4.Y2 = 684;
                arrow_Swap.Source = BitmapFrame.Create(new Uri(@"C:\arrowUp.png"));
                f = 0;
               

            }
        }
    }
}
