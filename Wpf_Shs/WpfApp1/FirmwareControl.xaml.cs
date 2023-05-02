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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for FirmwareControl.xaml
    /// </summary>
    
    public partial class FirmwareControl : UserControl
    {
        public FirmwareControl()
        {
            InitializeComponent();
        }
        private bool[] inactive = { true, true, true, true, true, true };
        private bool[] activeSettings = { false, false, false, false, false, false };
        private void navTable_Click(object sender, RoutedEventArgs e)
        {
         /*   Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            navTable.BorderThickness = new Thickness(1);
            navTable.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));*/
        }

        private void navBtn_Click(object sender, RoutedEventArgs e)
        {
          /*  navTable.BorderThickness = new Thickness(1);
            navTable.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));*/
        }

        private void settingBtn4_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot4.IsOpen = true;
        }

        private void settingBtn4_Click(object sender, RoutedEventArgs e)
        {
            threeDot4.IsOpen = true;
        }

        private void settingsDot4_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu4.IsOpen = true;
        }

        private void settingsDot4_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu4.IsOpen = true;
            Application.Current.Resources["table"] = 8;
        }

        private void settingsMenu4_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;
        }

 /*       private void menu_Clear4_Click(object sender, RoutedEventArgs e)
        {

        }*/

        private void menu_Settings4_Click(object sender, RoutedEventArgs e)
        {
            Settings_nav settings_Nav = new Settings_nav();
            settings_Nav.Show();
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;



            activeSettings[3] = true;
       /*     Application.Current.Resources["table"] = 4;
            Application.Current.Resources["isSettings"] = 1;*/
        }

        private void groupBox_4_Click(object sender, RoutedEventArgs e)
        {
         //   Application.Current.Resources["table"] = 8;
        }

        private void polygon_btn4_Click(object sender, RoutedEventArgs e)
        {
          
            if (inactive[3])
            {
                groupBox_4.BorderThickness = new Thickness(3);
                groupBox_4.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[3] = false;
            }
            else if (!inactive[3])
            {
                groupBox_4.BorderThickness = new Thickness(1);
                groupBox_4.BorderBrush = Brushes.White;
                inactive[3] = true;
            }
        }

       

        private void GroupBox4_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            threeDot4.IsOpen = false;
        }

        private void GroupBox4_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 8;
        }
    }
}
