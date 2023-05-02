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
    /// Interaction logic for Grozaz1.xaml
    /// </summary>
    public partial class Grozaz1 : Window
    {
        private int f = 0;
        public Grozaz1()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Height = 900;
            grozaz1_btn.Background = Brushes.White;
            grozaz1_btn.BorderThickness = new Thickness(1);
            grozaz1_btn.BorderBrush = Brushes.Aqua;
            
        }
        

        private void settings_circle_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
        }

        private void setting_btn_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void firmware_Button_Click(object sender, RoutedEventArgs e)
        {
            Firmware firmware = new Firmware();
            firmware.Show();
        }

        private void arrow_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (f == 0)
            {

                rect_tohide.Visibility = Visibility.Collapsed;
                f = 1;
                arrow_Swap.Source = BitmapFrame.Create(new Uri(@"C:\arrowDown.png"));
                Application.Current.MainWindow = this;
                Application.Current.MainWindow.Height = 770;

                /*  lastCh.MaxHeight = 10;
                  preLast.MaxHeight = 120;*/
                rect_try.Margin = new Thickness(-3, -250, 0, 0);
                rect_two.Margin = new Thickness(95, -250, 0, 0);
                //rect_try2.Margin = new Thickness(0, -305, 0, 0);

                line1.X1 = -23;
                line1.X2 = -23;
                line1.Y1 = 699;
                line1.Y2 = 720;


                line2.X1 = -23;
                line2.X2 = 1400;
                line2.Y1 = 699;
                line2.Y2 = 699;


                line3.X1 = -23;
                line3.X2 = 2000;
                line3.Y1 = 720;
                line3.Y2 = 720;

                line4.X1 = 10;
                line4.X2 = 10;
                line4.Y1 = 0;
                line4.Y2 = 699;

            }
            else
            {
                rect_tohide.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Height = 900;
                rect_try.Margin = new Thickness(-3, 10, 0, 0);
                rect_two.Margin = new Thickness(95, 12, 0, 0);
                //  rect_try2.Margin = new Thickness(0, , 0, 0);
                line1.X1 = -23;
                line1.X2 = -23;
                line1.Y1 = 830;
                line1.Y2 = 850;


                line2.X1 = -23;
                line2.X2 = 2400;
                line2.Y1 = 830;
                line2.Y2 = 830;


                line3.X1 = -23;
                line3.X2 = 2400;
                line3.Y1 = 850;
                line3.Y2 = 850;

                line4.X1 = 10;
                line4.X2 = 10;
                line4.Y1 = 0;
                line4.Y2 = 1100;
                arrow_Swap.Source = BitmapFrame.Create(new Uri(@"C:\arrowUp.png"));
                f = 0;
            }
        }

        private void grozas_btn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Application.Current.MainWindow = this;
            this.Close();
        }

        private void intelian_Button_Click(object sender, RoutedEventArgs e)
        {
            Calibration calibration = new Calibration();
            calibration.Show();
            Application.Current.MainWindow = this;
            intelian_Button.Background = Brushes.White;
            intelian_Button.BorderThickness = new Thickness(1);
            intelian_Button.BorderBrush = Brushes.Aqua;

        }

       
    }
}
