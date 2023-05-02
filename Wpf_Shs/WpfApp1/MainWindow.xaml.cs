using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
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
using System.Globalization;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
       
        private int f = 0;
        public bool grozaSflag = true;
        public static bool isMainClose = true;
        public string lang = "ru-RU";
        public string[] speeds { get; set; }
        private string[] ports { get; set; }
        public bool isFlag { get; set; }
        private bool ToggleButtonsState = true;
        private bool isOpenPort = false;
 //       SHS_DLL.ComSHS comShS = new SHS_DLL.ComSHS(0x04, 0x05);
        public string checkS { get; set; }
        private Int16 language = 1;
        public Int16 ChangeLang
        {
            get
            {
                return language;
            }
            set
            {
                language = value;

            }
        }
        public class MyWrapperClass
        {
            private static readonly Lazy<SHS_DLL.ComSHS> lazy = new Lazy<SHS_DLL.ComSHS>(() => new SHS_DLL.ComSHS(0x04, 0x05));

            public static SHS_DLL.ComSHS Instance => lazy.Value;
        }
        /*       private string ConvertLang(Int16 Lan)
               {
                   switch (Lan)
                   {
                       case 0: return "ru-Ru";
                       case 1: return "en-US";
                       default: return "ru-RU";
                   }
               }
               public new void Language(string value)
               {
                   ResourceDictionary dict = new ResourceDictionary();
                   switch (value)
                   {
                       case "Rus":
                           dict.Source = new Uri(String.Format("/WpfApp1;component/ResourcesLang/lang.ru-RU.xaml", value), UriKind.Relative);
                           break;
                       case "Eng":
                           dict.Source = new Uri(String.Format("/WpfApp1;component/ResourcesLang/lang.en-US1.xaml", value), UriKind.Relative);
                           break;
                   }

                   Resources.MergedDictionaries.Add(dict);

               }*/


        public MainWindow()
        {
            InitializeComponent();
            speeds = new string[] { "110","300", "600", "1200", "2400", "4800", "9600","14400", "19200","38400","56000","57600","115200","128000","256000" };
            DataContext = this;
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Height = 710;
            Application.Current.MainWindow.MaxHeight = 710;
            Application.Current.MainWindow.MinHeight = 710;
            
            ports = SerialPort.GetPortNames();
            //    Settings settings = new Settings();
            //   settings.PropertyChanged += App_PropertyChanged;

           // ((Settings)Application.Current).PropertyChanged += App_PropertyChanged;
            isFlag = true;
        /*    foreach (string s in ports)
                comPorts.Items.Add(s);*/
            grozaS_control.isFlag = true;
            Application.Current.Resources["isControl"] = "1";
            
            
        }

        public void ChangeLanguage(string lang)
        {

            var dict = new ResourceDictionary();
            switch (lang)

            {
                case "Eng":
                    dict.Source = new Uri("/WpfApp1;component/ResourcesLang/lang.en-US.xaml", UriKind.Relative);
                    break;
                case "Rus":
                    dict.Source = new Uri("/WpfApp1;component/ResourcesLang/lang.ru-RU.xaml", UriKind.Relative);
                    break;

                default:
                    dict.Source = new Uri("/WpfApp1;component/ResourcesLang/lang.en-US.xaml", UriKind.Relative);
                    break;
            }

            /* ResourceDictionary oldDict = (from d in Resources.MergedDictionaries
                                           where d.Source != null && d.Source.OriginalString.StartsWith("/WpfApp1;component/ResourcesLang/lang.")
                                           select d).First();*/

         /*   if (oldDict != null)
            {
                int ind = Resources.MergedDictionaries.IndexOf(oldDict);
                Resources.MergedDictionaries.Remove(oldDict);
                Resources.MergedDictionaries.Insert(ind, dict);

            }
            else
            {*/
                Application.Current.Resources.MergedDictionaries.Add(dict);
           // }
        
    }

        private void settings_Button_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            //   settings.PropertyChanged += App_PropertyChanged;
            settings.PropertyChanged += App_PropertyChanged;
            settings.ButtonClicked += settings_ButtonClicked;
            settings.CancelClicked += Settings_CancelClicked;
            settings.Show();
        }

        private void Settings_CancelClicked(object sender, EventArgs e)
        {
            btnPort.Background = ColorDisconnect;
        }

        private void settings_ButtonClicked(object sender, EventArgs e)
        {
            btnPort.Background = ColorConnect;
        }
        private void App_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GlobalString")
            {
                ChangeLanguage(Application.Current.Resources["lang_Change"].ToString());
            }
        }
      
        private void settings_Circlebtn_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void firmware_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["isNav"] = "1";
            Application.Current.Resources["settings_Header"] = "НАВИГАЦИЯ";
            if (ToggleButtonsState == true)
            {
                firmware_Button.IsChecked = true;
                manage_Button.IsChecked = false;
                ToggleButtonsState = false;

             
            }
            else
            {
                firmware_Button.IsChecked = true;

            }
            firmware_control.Margin = new Thickness(10, 30, 0, 5);     
            intelian_Button.BorderThickness = new Thickness(0);
            
            calibration_control.Visibility = Visibility.Hidden;
            grozaZ1_control.Visibility = Visibility.Hidden;
            grozaS_control.Visibility = Visibility.Hidden;
            firmware_control.Visibility = Visibility.Visible;
           
         //   rect_tohide.Visibility = Visibility.Visible;
          //  f = 0;

        }

        private void arrow_btn_Click(object sender, RoutedEventArgs e)
        {
            if (f == 0 )
            {
                rect_tohide.Visibility = Visibility.Collapsed;
               
                f = 1;
                arrow_Swap.Source = BitmapFrame.Create(new Uri("Images/arrowDown.png", UriKind.Relative));
                Application.Current.MainWindow = this;
                Application.Current.MainWindow.MaxHeight = 560;
                Application.Current.MainWindow.MinHeight = 560;
                Application.Current.MainWindow.Height = 560;
            
                log_Table.Margin = new Thickness(15,-10 , 0, 0);

                rect_try.Margin = new Thickness(0, -167, 0, 0);


                line1.X1 = -23;
                line1.X2 = -23;
                line1.Y1 = 589;
                line1.Y2 = 610;


                line2.X1 = 0;
                line2.X2 = 1400;
                line2.Y1 = 489;
                line2.Y2 = 489;


                line3.X1 = -23;
                line3.X2 = 2000;
                line3.Y1 = 510;
                line3.Y2 = 510;

                line4.X1 = 57;
                line4.X2 = 57;
                line4.Y1 = 0;
                line4.Y2 = 490;

            }
            else if (f == 1)
            {
                rect_tohide.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Height = 710;
                Application.Current.MainWindow.MaxHeight = 710;
                Application.Current.MainWindow.MinHeight = 710;
                rect_try.Margin = new Thickness(0, -16, 0, 0);
                log_Table.Margin = new Thickness(15, 0, 0, 0);
                line1.X1 = -23;
                line1.X2 = -23;
                line1.Y1 = 7220;
                line1.Y2 = 741;


                line2.X1 = -30;
                line2.X2 = 2000;
                line2.Y1 = 640;
                line2.Y2 = 640;


                line3.X1 = -23;
                line3.X2 = 2000;
                line3.Y1 = 660;
                line3.Y2 = 660;

                line4.X1 = 57;
                line4.X2 = 57;
                line4.Y1 = 0;
                line4.Y2 = 640;
              //  arrow_Swap.Source = BitmapFrame.Create(new Uri(@"C:\arrowUp.png"));
                arrow_Swap.Source = BitmapFrame.Create(new Uri("Images/arrowUp.png", UriKind.Relative));
                
                f = 0;


            }
            
        }
            private  void intelian_Button_Click(object sender, RoutedEventArgs e)
        {
            grozaS_control.Visibility = Visibility.Hidden;
            manage_Button.Visibility = Visibility.Hidden;
            firmware_Button.Visibility = Visibility.Hidden;
            firmware_control.Visibility = Visibility.Hidden;
            grozaZ1_control.Visibility = Visibility.Hidden;
            calibration_control.Visibility = Visibility.Visible;
            rect_tohide.Visibility = Visibility.Collapsed;
            log_Table.Visibility = Visibility.Collapsed;
            grozaSflag = false ;
             grozaS_control.isFlag = false;
            isFlag = false;
            intelian_Button.Background = Brushes.White;
            intelian_Button.BorderThickness = new Thickness(3);
            intelian_Button.BorderBrush = Brushes.Aqua;

            grozaZ1_button.BorderThickness = new Thickness(0);
         
            grozaS_button.BorderThickness = new Thickness(0);
            Application.Current.MainWindow.Height = 710;
            Application.Current.MainWindow.MaxHeight = 710;
            Application.Current.MainWindow.MinHeight = 710;
            log_Table.Margin = new Thickness(14, -15, 0, 0);
          
            rect_try.Margin = new Thickness(0, -16, 0, 0);
            log_Table.Margin = new Thickness(15, -23, 0, 0);
            line1.X1 = -23;
            line1.X2 = -23;
            line1.Y1 = 7220;
            line1.Y2 = 741;


            line2.X1 = -30;
            line2.X2 = 2000;
            line2.Y1 = 640;
            line2.Y2 = 640;


            line3.X1 = -23;
            line3.X2 = 2000;
            line3.Y1 = 660;
            line3.Y2 = 660;

            line4.X1 = 57;
            line4.X2 = 57;
            line4.Y1 = 0;
            line4.Y2 = 640;

        }
        public static void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }
        private static void ReadInfoPorts(string[] ports)
        {
        

        }


        private void grozaZ1_button_Click(object sender, RoutedEventArgs e)
        {
          
            Application.Current.MainWindow = this;
            grozaSflag = false;
            Application.Current.Resources["isControl"] = "0";
            firmware_Button.IsChecked = false;
            manage_Button.IsChecked = true;
            ToggleButtonsState = true;
            grozaS_control.isFlag = false;
            isFlag = false;
            intelian_Button.BorderThickness = new Thickness(0);
            calibration_control.Visibility = Visibility.Hidden;
            manage_Button.Visibility = Visibility.Visible;
            firmware_Button.Visibility = Visibility.Visible;
            firmware_control.Visibility = Visibility.Hidden;
            log_Table.Visibility = Visibility.Visible;
            Application.Current.MainWindow.MinHeight = 710;
            Application.Current.MainWindow.MaxHeight = 710;
          
            Application.Current.MainWindow.Height = 710;
         
            grozaS_control.Visibility = Visibility.Hidden;
            grozaZ1_control.Margin = new Thickness(0, 0, 0, 160);
            log_Table.Margin = new Thickness(7, 50, 0, 0);
            grozaZ1_control.Visibility = Visibility.Visible;
            rect_tohide.Visibility = Visibility.Visible;
            f = 0;
       

            grozaZ1_button.BorderThickness = new Thickness(3);
            grozaZ1_button.BorderBrush = Brushes.Aqua;
            grozaS_button.BorderThickness = new Thickness(0);

         
            rect_try.Margin = new Thickness(0, -16, 0, 0);
            log_Table.Margin = new Thickness(15, 0, 0, 0);
            line1.X1 = -23;
            line1.X2 = -23;
            line1.Y1 = 7220;
            line1.Y2 = 741;


            line2.X1 = -30;
            line2.X2 = 2000;
            line2.Y1 = 640;
            line2.Y2 = 640;


            line3.X1 = -23;
            line3.X2 = 2000;
            line3.Y1 = 660;
            line3.Y2 = 660;

            line4.X1 = 57;
            line4.X2 = 57;
            line4.Y1 = 0;
            line4.Y2 = 640;
            arrow_Swap.Source = BitmapFrame.Create(new Uri("Images/arrowUp.png", UriKind.Relative));
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            isMainClose = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isMainClose = true;
        }

        private void grozaS_button_Click(object sender, RoutedEventArgs e)
        {
            grozaSflag = true;
            grozaS_control.isFlag = true;
            firmware_Button.IsChecked = false;
            manage_Button.IsChecked = true;
            ToggleButtonsState = true;
            Application.Current.Resources["isControl"] = "1";
            isFlag = true;
            log_Table.Visibility = Visibility.Visible;
            intelian_Button.BorderThickness = new Thickness(0);
            calibration_control.Visibility = Visibility.Hidden;
            grozaZ1_control.Visibility = Visibility.Hidden;
            manage_Button.Visibility = Visibility.Visible;
            firmware_Button.Visibility = Visibility.Visible;
            firmware_control.Visibility = Visibility.Hidden;
            grozaS_control.Visibility = Visibility.Visible;
            grozaS_button.BorderThickness = new Thickness(3);
            grozaS_button.BorderBrush = Brushes.Aqua;
            grozaZ1_button.BorderThickness = new Thickness(0);
            rect_tohide.Visibility = Visibility.Visible;
            f = 0;
            Application.Current.MainWindow.Height = 710;
            Application.Current.MainWindow.MaxHeight = 710;
            Application.Current.MainWindow.MinHeight = 710;
            firmware_control.Margin = new Thickness(10, 20, 0, 110);
            rect_try.Margin = new Thickness(0, -16, 0, 0);
            log_Table.Margin = new Thickness(15, 0, 0, 0);
            line1.X1 = -23;
            line1.X2 = -23;
            line1.Y1 = 7220;
            line1.Y2 = 741;


            line2.X1 = -30;
            line2.X2 = 2000;
            line2.Y1 = 640;
            line2.Y2 = 640;


            line3.X1 = -23;
            line3.X2 = 2000;
            line3.Y1 = 660;
            line3.Y2 = 660;

            line4.X1 = 57;
            line4.X2 = 57;
            line4.Y1 = 0;
            line4.Y2 = 640;
            arrow_Swap.Source = BitmapFrame.Create(new Uri("Images/arrowUp.png", UriKind.Relative));


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public LinearGradientBrush ColorConnect = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
        {
            new GradientStop(Color.FromRgb(15, 91, 19), 1.0),
            new GradientStop(Color.FromRgb(0, byte.MaxValue, 31), 0.002)
        }), new Point(0.5, 0.0), new Point(0.5, 1.0));

        public LinearGradientBrush ColorDisconnect = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
        {
            new GradientStop(Color.FromRgb(66, 3, 3), 1.0),
            new GradientStop(Color.FromRgb(byte.MaxValue, 0, 0), 0.002)
        }), new Point(0.5, 0.0), new Point(0.5, 1.0));


        private void btnPort_Click(object sender, RoutedEventArgs e)
        {
            
           
            /*int.TryParse(comSpeeds.SelectedItem.ToString(), out int portSpeed);
            if (!isOpenPort)
            {
                comShS.OpenPort(comPorts.SelectedItem.ToString(), portSpeed, Parity.None, 8, StopBits.Two);
                btnPort.Background = ColorConnect;
                isOpenPort = true;
            } else
            {
                comShS.ClosePort();
                isOpenPort = false;
                btnPort.Background = ColorDisconnect;
            }*/
        }

        private void manage_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["isNav"] = "0";
            if (ToggleButtonsState  == false)
            {
                firmware_Button.IsChecked = false;
                manage_Button.IsChecked = true;
                ToggleButtonsState = true;
            } else
            {
                manage_Button.IsChecked = true;

            }
            if (grozaSflag)
            {
                
                intelian_Button.BorderThickness = new Thickness(0);
                calibration_control.Visibility = Visibility.Hidden;
                firmware_control.Visibility = Visibility.Hidden;
                grozaZ1_control.Visibility = Visibility.Hidden;
                grozaS_control.Visibility = Visibility.Visible;
                grozaS_button.BorderThickness = new Thickness(3);
                grozaS_button.BorderBrush = Brushes.Aqua;
                grozaZ1_button.BorderThickness = new Thickness(0);
            }
            else if (!grozaSflag)
            {

                intelian_Button.BorderThickness = new Thickness(0);
                calibration_control.Visibility = Visibility.Hidden;
                firmware_control.Visibility = Visibility.Hidden;
                grozaZ1_control.Visibility = Visibility.Visible;
                grozaS_control.Visibility = Visibility.Hidden;
                grozaS_button.BorderThickness = new Thickness(0);
                grozaZ1_button.BorderBrush = Brushes.Aqua;
                grozaZ1_button.BorderThickness = new Thickness(3);
            }
        }
    }
}
