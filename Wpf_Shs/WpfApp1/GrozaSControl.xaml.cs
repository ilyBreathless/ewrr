using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GrozaSControl.xaml
    /// </summary>
    /// 
    public partial class GrozaSControl : UserControl
    {
        private bool isConnected = true;
        private bool isConnected2 = false;
        private bool[] inactive = { true, true, true, true, true, true };
        private bool[] activeSettings = { false, false, false, false, false, false };
        private bool ToggleButtonsState = true;
        private bool ToggleButtonsState2 = false;
        private bool applyBool = false;
        private bool activeSircle = false;
        

        public int tettaProperty { get; set; }

        private int[] deviationNum = new int[4];
        private byte[] speedBytes = new byte[4];
        private string[] freqStr = new string[4];
        private byte[] manipulationBytes = new byte[4];
        private string[] digitsOnlyFreq = new string[4];
        private uint[] freqTable = new uint[4];
        private uint powerNav = 100;
        private uint powerSpoof = 100;
        private string manipulation;
       
        private bool[] isPower = new bool[2];
        SHS_DLL.ComSHS comShS = new SHS_DLL.ComSHS(0x04, 0x05);
        SHS_DLL.AmpCodes ampCodes = new SHS_DLL.AmpCodes();


     

        //   private SHS_DLL.TParamFWS paramFWS = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = (uint)e.Freq, Deviation = 0, Duration = 0, Manipulation = 0, Modulation = 0 } };

        SHS_DLL.TGpsGlonass gpsGlonass = new SHS_DLL.TGpsGlonass();
        SHS_DLL.TBeidouGalileo beidouGalileo = new SHS_DLL.TBeidouGalileo();
 
        public GrozaSControl()
        {
            
            InitializeComponent();
            isFlag = true;

            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSet += ComShS_OnConfirmSet;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmStatus += ComShS_OnConfirmStatus;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmStatusAntenna += ComShS_OnConfirmStatusAntenna;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmFullStatus += ComShS_OnConfirmFullStatus;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSwitch += ComShS_OnConfirmSwitch;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSetParam += ComShS_OnConfirmSetParam;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmGetParam += ComShS_OnConfirmGetParam;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSaveParam += ComShS_OnConfirmSaveParam;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnRadiatOff += ComShS_OnRadiatOff;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnStatus += ComShS_OnStatus;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnReadByte += ComShS_OnReadByte;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnWriteByte += Instance_OnWriteByte;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnSetIRI += ComShS_OnSetIRI;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnTestGNSS += ComShS_OnTestGNSS;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnSwitchPosition += ComShS_OnSwitchPosition;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnParamNaVi += ComShS_OnParamNaVi;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnVersion += ComShS_OnVersion;
        }

        private void Instance_OnWriteByte(object sender, SHS_DLL.ByteEventArgs e)
        {
            try
            {
                
                string a = "";
                foreach (var i in e.Byte)
                {
                    a += i.ToString("X2");
                    a += " ";
                }
                AddToLog(a);
            }
            catch { }
        }

        private void ComShS_OnReadByte(object sender, SHS_DLL.ByteEventArgs e)
        {
            try
            {
               
                string a = "";
                foreach (var i in e.Byte)
                {
                    a += i.ToString("X2");
                    a += " ";
                }
                Dispatcher.Invoke(() => { AddToLog(a); });
            }
            catch
            {

            }
        }
        //AddTextToLog($"Код ошибки: {e}", Brushes.Green); });

        private void ComShS_OnVersion(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void ComShS_OnParamNaVi(object sender, byte e)
        {
            
            string logText = e.ToString();

            AddToLog(logText);
        }

        private void ComShS_OnSwitchPosition(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }
         
        private void ComShS_OnTestGNSS(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void ComShS_OnSetIRI(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void ComShS_OnStatus(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void ComShS_OnRadiatOff(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void ComShS_OnConfirmSaveParam(object sender, byte e)
        {
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void ComShS_OnConfirmGetParam(object sender, SHS_DLL.ConfirmGetParamEventArgs[] e)
        {
            
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmSetParam(object sender, byte e)
        {
            throw new NotImplementedException();
            //comShS.OnReadByte()
        }

        private void ComShS_OnConfirmSwitch(object sender, SHS_DLL.ConfirmSetSwitch e)
        {
            //e.SectorNumber
            byte[] secNum = e.SectorNumber;
           
            //e.AmpCode
        }

     

        private void ComShS_OnConfirmStatusAntenna(object sender, SHS_DLL.ConfirmStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangeImageColorToGreen(Image image)
        {
            BitmapImage bitmapImage = image.Source as BitmapImage;
            if (bitmapImage != null)
            {
                FormatConvertedBitmap newBitmap = new FormatConvertedBitmap(bitmapImage, PixelFormats.Bgra32, null, 0);

                int width = newBitmap.PixelWidth;
                int height = newBitmap.PixelHeight;
                int stride = width * 4;
                byte[] pixels = new byte[height * stride];

                newBitmap.CopyPixels(pixels, stride, 0);

                for (int i = 0; i < pixels.Length; i += 4)
                {
                    pixels[i + 1] = 255;
                    pixels[i] = pixels[i + 2] = pixels[i + 3] = 0;
                }

                BitmapSource newSource = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, stride);
                image.Source = newSource;
            }
        }
        public void ChangeImageColorToRed(Image image)
        {
            BitmapImage bitmapImage = image.Source as BitmapImage;
            if (bitmapImage != null)
            {
                FormatConvertedBitmap newBitmap = new FormatConvertedBitmap(bitmapImage, PixelFormats.Bgra32, null, 0);

                int width = newBitmap.PixelWidth;
                int height = newBitmap.PixelHeight;
                int stride = width * 4;
                byte[] pixels = new byte[height * stride];

                newBitmap.CopyPixels(pixels, stride, 0);

                for (int i = 0; i < pixels.Length; i += 4)
                {
                    pixels[i + 2] = 255;
                    pixels[i] = pixels[i + 1] = pixels[i + 3] = 0;
                }

                BitmapSource newSource = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, stride);
                image.Source = newSource;
            }
        }
        public Color GetImageOriginalColor(Image image)
        {
            BitmapImage bitmapImage = image.Source as BitmapImage;
            if (bitmapImage != null)
            {
                FormatConvertedBitmap newBitmap = new FormatConvertedBitmap(bitmapImage, PixelFormats.Bgra32, null, 0);

                int width = newBitmap.PixelWidth;
                int height = newBitmap.PixelHeight;
                int stride = width * 4;
                byte[] pixels = new byte[height * stride];

                newBitmap.CopyPixels(pixels, stride, 0);

                int r = 0;
                int g = 0;
                int b = 0;

                for (int i = 0; i < pixels.Length; i += 4)
                {
                    r += pixels[i + 2];
                    g += pixels[i + 1];
                    b += pixels[i];
                }

                r /= (height * width);
                g /= (height * width);
                b /= (height * width);

                return Color.FromRgb((byte)r, (byte)g, (byte)b);
            }

            return Colors.Transparent;
        }
        private void ComShS_OnConfirmStatus(object sender, SHS_DLL.ConfirmStatusEventArgs e)
        {
            // e.ParamAmp

            /*  foreach (var i in e.ParamAmp)
              {
                  Dispatcher.Invoke(() => { SFreqTable.Items.Add(new STableFreq() { Lite = count.ToString(), LitDescrip = RangeLit[count - 1], Signal = Convert.ToBoolean(i.Snt), Emitting = Convert.ToBoolean(i.Rad), Degree = i.Temp.ToString(), Amperage = (i.Current / 10).ToString(), Power = Convert.ToBoolean(i.Power), Error = Convert.ToBoolean(i.Error) }); });
                  count++;
              }*/
            /*  foreach(var i in e.ParamAmp)
              {


              }*/

           // SHS_DLL.TParamAmp paramAmp[5] = new SHS_DLL.TParamAmp(); 

           byte[] snt = new byte[5];
        
           Image[] snt_x = new Image[] { snt_1, snt_2, snt_3,snt_4, snt_5 };
           Image[] rad_x = new Image[] { rad_1, rad_2, rad_3, rad_4, rad_5 };
           Image[] power_x = new Image[] { power_1, power_2, power_3, power_4, power_5 };
           Image[] error_x = new Image[] { err_1, err_2, err_3, err_4, err_5 };
            byte[] rad = new byte[5];
           sbyte[] temp = new sbyte[5];
           byte[] power = new byte[5];
           byte[] error = new byte[5];

            bool[] stateOfParamsSnt = new bool[5];
            bool[] stateOfParamsRad = new bool[5];
            bool[] stateOfParamsPow = new bool[5];
            bool[] stateOfParamsErr = new bool[5];

            for (int i = 1; i <= 5; i++)
            {
                snt[i] = e.ParamAmp[i].Snt;
                rad[i] = e.ParamAmp[i].Rad;
                temp[i] = e.ParamAmp[i].Temp;
                power[i] = e.ParamAmp[i].Power;
                error[i] = e.ParamAmp[i].Error;


                if (snt[i].ToString() == "1")
                {
                    stateOfParamsSnt[i] = true;
                }
                if (rad[i].ToString() == "1")
                {
                    stateOfParamsRad[i] = true;
                }
                if (power[i].ToString() == "1")
                {
                    stateOfParamsPow[i] = true;
                }
                if (error[i].ToString() == "1")
                {
                    stateOfParamsErr[i] = true;
                }

            }
         
            for (int i = 0; i < snt_x.Length; i++ )
            {
                if (stateOfParamsSnt[i] == true)
                {
                    ChangeImageColorToGreen(snt_x[i]); 
                } else if (stateOfParamsSnt[i] == false)
                {
                    ChangeImageColorToRed(snt_x[i]);
                }

                if (stateOfParamsRad[i] == true)
                {
                    ChangeImageColorToGreen(rad_x[i]);
                }
                else if (stateOfParamsRad[i] == false)
                {
                    ChangeImageColorToRed(rad_x[i]);
                }

                if (stateOfParamsPow[i] == true)
                {
                    ChangeImageColorToGreen(power_x[i]);
                }
                else if (stateOfParamsPow[i] == false)
                {
                    ChangeImageColorToRed(power_x[i]);
                }

                if (stateOfParamsErr[i] == true)
                {
                    ChangeImageColorToGreen(error_x[i]);
                }
                else if (stateOfParamsErr[i] == false)
                {
                    GetImageOriginalColor(error_x[i]);
                }
            }
          
            
           
/*
      
            temp_1.Content = temp;
            temp_2.Content = temp;
            temp_3.Content = temp;
            temp_4.Content = temp;
            temp_5.Content = temp;*/

           
          
            string logText = e.ToString();
            AddToLog(logText);
        }

        private void AddToLog(string text)
        {
            logListBox.Items.Add(text);
        }
        private void ComShS_OnConfirmSet(object sender, SHS_DLL.ConfirmSetEventArgs e)
        {
        //    string ampTxt = e.AmpCode.ToString();
          //  string logText =  e.CodeError.ToString() + ampTxt;

            Dispatcher.Invoke(() => { AddToLog($"Команда: {e.AmpCode}"); });
            Dispatcher.Invoke(() => { AddToLog($"Код ошибки: {e.CodeError}"); });
           // AddToLog($"Код ошибки: {logText}");
            
            /*SHS_DLL.AmpCodes.PARAM_FWS_APP = e.AmpCode;*/
 
        }

        private void ComShS_OnConfirmFullStatus(object sender, SHS_DLL.ConfirmFullStatusEventArgs e)
        {
           // e.Amplifiers
             
            string logText = e.CodeError.ToString();
            AddToLog(logText);
        }

        public  bool isFlag { get; set; }
        

        private void groupBox1_Click(object sender, System.EventArgs e)
        {

        }
        private void settings_Button_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            /*    Settings myWindow = Application.Current.Windows.OfType<Settings>().SingleOrDefault(x => x.IsActive);
                tettaProperty = ((MainWindow)myWindow).myIntVariable;*/
            settings.textBoxValueChanged += Settings_textBoxValueChanged;
            settings.Show();
        }

        private void Settings_textBoxValueChanged(object sender, string e)
        {
            tettaProperty = int.Parse(e);
        }

        private void settings_Circlebtn_Click(object sender, RoutedEventArgs e)
        {
            //litteru comShS.SendStatus();
            string letterStr = Application.Current.Resources["table"].ToString();
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);

          
            if (!activeSircle)
            {
                activeSircle = true;
                System.Threading.Thread thread = new System.Threading.Thread(StartLoop);
                thread.Start();
            } else
            {
                activeSircle = false;
            }
         
           // добавить еще изменение стиля когда кнопка нажата ( border поменять например как с групбокс)
        }
        private void StartLoop()
        {
            while (activeSircle)
            {
                for (byte i = 1; i <= 5; i++)
                {
                   comShS.SendStatus(i);   
                }

                System.Threading.Thread.Sleep(5000);
            }
        }
        private void Settings_circle_manipulationEvent(object sender, string e)
        {
            int test;
            test = int.Parse(e);
      
            if (Application.Current.Resources["table"].ToString() == "1")
            {
                manipulationBytes[0] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "2")
            {
                manipulationBytes[1] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "3")
            {
                manipulationBytes[2] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "4")
            {
                manipulationBytes[3] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                manipulationBytes[4] = (byte)test;
            }

            
        }

        private void Settings_circle_scanspeedEvent(object sender, string e)
        {
            int test;
            test = int.Parse(e);
          
            if (Application.Current.Resources["table"].ToString() == "1")
            {
                speedBytes[0] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "2")
            {
                speedBytes[1] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "3")
            {
                speedBytes[2] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "4")
            {
                speedBytes[3] = (byte)test;
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                speedBytes[4] = (byte)test;
            }
            // paramFWS.
            //    paramFWS.Modulation = speedByte;
        }

        private void Settings_circle_deviationEvent(object sender, string e)
        {
            string deviation = e;
            if (Application.Current.Resources["table"].ToString() == "1")
            {
                deviationNum[0] = int.Parse(deviation);
            } else if (Application.Current.Resources["table"].ToString() == "2")
            {
                deviationNum[1] = int.Parse(deviation);
            }
            else if (Application.Current.Resources["table"].ToString() == "3")
            {
                deviationNum[2] = int.Parse(deviation);
            } else if (Application.Current.Resources["table"].ToString() == "4")
            {
                deviationNum[3] = int.Parse(deviation);
            } else if (Application.Current.Resources["table"].ToString() == "5")
            {
                deviationNum[4] = int.Parse(deviation);
            }
            //  paramFWS.Deviation = deviationNum;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
         

        }

        private void groupBox_Click(object sender, RoutedEventArgs e)
        {
        
            Application.Current.Resources["table"] = 1;
        
        }
    
    

        private void groupBox_4_Click(object sender, RoutedEventArgs e)
        {
      
            Application.Current.Resources["table"] = 4;
        }

        private void groupBox_3_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Resources["table"] = 3;
        }

        private void groupBox_2_Click(object sender, RoutedEventArgs e)
        {
           
            Application.Current.Resources["table"] = 2;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {  
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + "//Images/Ellipse 3.png";
            string path2 = dir + "//Images/Ellipse 4.png";
            if (!isConnected)
            {
                silver_first.Source = BitmapFrame.Create(new Uri(path, UriKind.Relative));
                isConnected = true;
            } else
            {
                silver_first.Source = BitmapFrame.Create(new Uri(path2, UriKind.Relative));
                isConnected = false;
            }
           
        }

        private void ToggleButton_Click_1(object sender, RoutedEventArgs e)
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + "//Images/Ellipse 3.png";
            string path2 = dir + "//Images/Ellipse 4.png";
            if (!isConnected2)
            {
                silver_second.Source = BitmapFrame.Create(new Uri(path, UriKind.Relative));
                isConnected2 = true;
            }
            else
            {
                silver_second.Source = BitmapFrame.Create(new Uri(path2, UriKind.Relative));
                isConnected2 = false;
            }

        }

        private void navBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings_nav settings_nav = new Settings_nav();
            settings_nav.Show();
          
            
           
        }

      

        private void Settings_nav_percentClicked(object sender, EventArgs e)
        {

            // в случае если нажата галочка для процентов
            /* if (Application.Current.Resources["table"] == "5")
             {
                 string spoofPercent = Application.Current.Resources["W_5s"].ToString();
                 byte numOfPercent;
                 byte.TryParse(spoofPercent, out numOfPercent);
                 comShS.SendSetSPOOF(numOfPercent);
             } else
             {
                 // param peredat' comShS.SendSetGNSS();
                 //comShS.SendSetParamNaViRadOn();

             }*/
            if (Application.Current.Resources["table"].ToString() == "4")
            {
                isPower[0] = true;
            } else if (Application.Current.Resources["table"].ToString() == "5")
            {
                isPower[1] = true;
            }
        }

        private void GroupBox_btn_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void GroupBox2_btn_Click(object sender, RoutedEventArgs e)
        {
       
        }

        private void GroupBox3_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GroupBox4_btn_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void logToggle_Click(object sender, RoutedEventArgs e)
        {
            comShS.SendRelaySwitching(0); 
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + "//Images/Ellipse 3.png";
            string path2 = dir + "//Images/Ellipse 4.png";

        
            isConnected2 = false;
            silver_second.Source = BitmapFrame.Create(new Uri(path2, UriKind.Relative));
            ToggleButtonsState2 = false;
            omniToggle.IsChecked = false;

            if (!ToggleButtonsState)
            {
                if (!isConnected)
                {
                    silver_first.Source = BitmapFrame.Create(new Uri(path, UriKind.Relative));
                    isConnected = true;
                   
                    ToggleButtonsState = true;
                }
                else if (isConnected)
                {
                    silver_first.Source = BitmapFrame.Create(new Uri(path2, UriKind.Relative));
                    isConnected = false;
                }
            } else
            {
                logToggle.IsChecked = true;
            }
        }

        private void omniToggle_Click(object sender, RoutedEventArgs e)
        {
            comShS.SendRelaySwitching(1);

            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + "//Images/Ellipse 3.png";
            string path2 = dir + "//Images/Ellipse 4.png";
          
            isConnected = false;
            ToggleButtonsState = false;
            logToggle.IsChecked = false;
                
            silver_first.Source = BitmapFrame.Create(new Uri(path2, UriKind.Relative));
            if (!ToggleButtonsState2)
            {
                if (!isConnected2)
                {
                    silver_second.Source = BitmapFrame.Create(new Uri(path, UriKind.Relative));
                    isConnected2 = true;
                    ToggleButtonsState2 = true;
                }
                else
                {
                    silver_second.Source = BitmapFrame.Create(new Uri(path2, UriKind.Relative));
                    isConnected2 = false;
                }
            } else
            {
                omniToggle.IsChecked = true;
            }
        }

        private void settingBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot.IsOpen = true;
        }

        private void settingsDot_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu.IsOpen = true;
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;
        }

        private void settingsDot_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu.IsOpen = true;
        }


        private void menu_Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;
            settingsMenu.IsOpen = false;
            threeDot.IsOpen = false;

       

            activeSettings[0] = true;
            Application.Current.Resources["table"] = 1;
            Application.Current.Resources["settings_Header"] = "100..500 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }



        private void menu_Clear_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_1s"] = "МГц";
            Application.Current.Resources["W_1s"] = "100 %";
            Application.Current.Resources["txtdF_1s"] = "МГц";
        }

        private void settingsDot2_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu2.IsOpen = true;
        }

        private void settingsDot2_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu2.IsOpen = true;
        }

        private void settingBtn2_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot2.IsOpen = true;
        }

        private void settingBtn2_Click(object sender, RoutedEventArgs e)
        {
            threeDot2.IsOpen = true;
        }

    

        private void menu_Clear2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_2s"] = "МГц";
            Application.Current.Resources["W_2s"] = "100 %";
            Application.Current.Resources["txtdF_2s"] = "МГц";
        }

        private void menu_Settings2_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            settingsMenu2.IsOpen = false;
            threeDot2.IsOpen = false;

            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;

            activeSettings[1] = true;
            Application.Current.Resources["table"] = 2;
            Application.Current.Resources["settings_Header"] = "500..2500 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void settingBtn3_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot3.IsOpen = true;
        }

        private void settingBtn3_Click(object sender, RoutedEventArgs e)
        {
            threeDot3.IsOpen = true;
        }

        private void settingsDot3_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu3.IsOpen = true;
        }

        private void settingsDot3_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu3.IsOpen = true;
        }

        private void menu_Clear3_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_3s"] = "МГц";
            Application.Current.Resources["W_3s"] = "100 %";
            Application.Current.Resources["txtdF_3s"] = "МГц";
        }

        private void menu_Settings3_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            settingsMenu3.IsOpen = false;
            threeDot3.IsOpen = false;

            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;

            activeSettings[2] = true;
            Application.Current.Resources["table"] = 3;
            Application.Current.Resources["settings_Header"] = "2500..6000 МГц";
            Application.Current.Resources["isSettings"] = 1;
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
        }

        private void menu_Clear4_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_4s"] = "МГц";
            Application.Current.Resources["W_4s"] = "100 %";
            Application.Current.Resources["txtdF_4s"] = "МГц";
        }

        private void menu_Settings4_Click(object sender, RoutedEventArgs e)
        {
            Settings_nav settings_Nav = new Settings_nav();
            settings_Nav.Show();
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;

           
            settings_Nav.numTextEvent += Settings_Nav_numTextEvent;
            settings_Nav.percentClicked += Settings_nav_percentClicked;
            settings_Nav.powerFalseClicked += Settings_Nav_powerFalseClicked;
            activeSettings[3] = true;
            Application.Current.Resources["table"] = 4;
            Application.Current.Resources["isNav"] = 0;
            Application.Current.Resources["settings_Header"] = "НАВИГАЦИЯ";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void Settings_Nav_powerFalseClicked(object sender, EventArgs e)
        {
            if (Application.Current.Resources["table"].ToString() == "4")
            {
                isPower[0] = false;
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                isPower[1] = false;
            }
            
        }

        private void Settings_Nav_numTextEvent(object sender, string e)
        {
            if (e == "")
            {
                e = "0";
            }
            string powerStr = e;
          
            if (Application.Current.Resources["table"].ToString() == "4")
            {
                powerNav = uint.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                powerSpoof = uint.Parse(powerStr);
            }
           
        }

        private void settingsMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu.IsOpen = false;
            threeDot.IsOpen = false;
        }

        private void settingsMenu2_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu2.IsOpen = false;
            threeDot2.IsOpen = false;
        }

        private void settingsMenu3_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu3.IsOpen = false;
            threeDot3.IsOpen = false;
        }

        private void settingsMenu4_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;
        }

      
        private void polygon_btn3_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 3;
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);

            freqStr[2] = (string)Application.Current.Resources["txtF_3s"];
            digitsOnlyFreq[2] = Regex.Replace(freqStr[2], "[^0-9]", "");
            freqTable[2] = uint.Parse(digitsOnlyFreq[2]);
            //   comShS.SendReset(numOfTable);
            if (applyBool)
            {
                comShS.SendStatus(1);
            }
            if (inactive[2])
            {
                groupBox_3.BorderThickness = new Thickness(3);
                groupBox_3.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                var paramFWS3 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[2], Deviation = deviationNum[2], Duration = 0, Manipulation = manipulationBytes[2], Modulation = speedBytes[2] } };
                comShS.SendSetParamFWS(tettaProperty, paramFWS3);
                inactive[2] = false;
            } else if (!inactive[2])
            {
                groupBox_3.BorderThickness = new Thickness(1);
                groupBox_3.BorderBrush = Brushes.White;
                inactive[2] = true;
                comShS.SendRadiatOff(numOfTable);
            }

          }

        private void polygon_btn2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 2;
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);
            // comShS.SendReset(numOfTable);
            if (applyBool)
            {
                comShS.SendStatus(2);
            }

            freqStr[1] = (string)Application.Current.Resources["txtF_2s"];
            digitsOnlyFreq[1] = Regex.Replace(freqStr[1], "[^0-9]", "");
            freqTable[1] = uint.Parse(digitsOnlyFreq[1]);

            if (inactive[1])
            {
                groupBox_2.BorderThickness = new Thickness(3);
                groupBox_2.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                var paramFWS2 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[1], Deviation = deviationNum[1], Duration = 0, Manipulation = manipulationBytes[1], Modulation = speedBytes[1] } };
                comShS.SendSetParamFWS(tettaProperty, paramFWS2);
                inactive[1] = false;
            }
            else if (!inactive[1])
            {
                groupBox_2.BorderThickness = new Thickness(1);
                groupBox_2.BorderBrush = Brushes.White;
                inactive[1] = true;
                comShS.SendRadiatOff(numOfTable);
            }
        }

        private void polygon_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 1;
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);
            if (applyBool)
            {
                    comShS.SendStatus(1);
            }
            //   comShS.SendReset(numOfTable);

            freqStr[0] = (string)Application.Current.Resources["txtF_1s"];
            digitsOnlyFreq[0] = Regex.Replace(freqStr[0], "[^0-9]", "");
            freqTable[0] = uint.Parse(digitsOnlyFreq[0]);

            if (inactive[0])
            {
                groupBox.BorderThickness = new Thickness(3);
                groupBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                
                var paramFWS1 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[0] * 10000, Deviation = deviationNum[0], Duration = 0, Manipulation = manipulationBytes[0], Modulation = speedBytes[0] } };
                WpfApp1.MainWindow.MyWrapperClass.Instance.SendSetParamFWS(tettaProperty, paramFWS1);
                inactive[0] = false;
            }
            else if (!inactive[0])
            {
                groupBox.BorderThickness = new Thickness(1);
                groupBox.BorderBrush = Brushes.White;
                inactive[0] = true;
                comShS.SendRadiatOff(numOfTable);
            }
        }

   /*     public struct TGpsGlonass
        {
            public bool gpsL1;
            public bool gpsL2;
            public bool glnssL1;
            public bool glnssL2;
        }*/


        private void polygon_btn4_Click(object sender, RoutedEventArgs e)
        {
            //   comShS.SendSetParamNaViRadOn()
            Application.Current.Resources["table"] = 4;
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);
            comShS.SendReset(numOfTable);

            if (applyBool)
            {
                comShS.SendStatus(4);
            }

            if (inactive[3])
            {
                groupBox_4.BorderThickness = new Thickness(3);
                groupBox_4.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[3] = false;

                if (isPower[0])
                {
                    comShS.SendSetGNSS(gpsGlonass, (byte)powerNav);
                }
                else if (!isPower[0])
                {
                    comShS.SendSetGNSS(gpsGlonass, beidouGalileo);
                }
                //  comShS.SendSetGNSS(gpsGlonass);
                //  comShS.SendSetG// добавить flag на мощность . если есть то 18. но GALILEO ?
            }
            else if (!inactive[3])
            {
                groupBox_4.BorderThickness = new Thickness(1);
                groupBox_4.BorderBrush = Brushes.White;
                inactive[3] = true;
                comShS.SendRadiatOff(numOfTable);
            }
        }

        private void GroupBox_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu.IsOpen = false;
            threeDot.IsOpen = false;
        }

        private void GroupBox2_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu2.IsOpen = false;
            threeDot2.IsOpen = false;
        }

        private void GroupBox3_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu3.IsOpen = false;
            threeDot3.IsOpen = false;
        }

        private void GroupBox4_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;
        }

        private void polygon_btn5_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 5;
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);
            comShS.SendReset(numOfTable);
            //comShS.SendSetSPOOF();
            if (applyBool)
            {
                comShS.SendStatus(5);
            }
            if (inactive[4])
            {
                groupBox_5.BorderThickness = new Thickness(3);
                groupBox_5.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                comShS.SendSetSPOOF();
                
                inactive[4] = false;
            }
            else if (!inactive[4])
            {
                groupBox_5.BorderThickness = new Thickness(1);
                groupBox_5.BorderBrush = Brushes.White;
                inactive[4] = true;
                comShS.SendRadiatOff(numOfTable);
            }
        }

        private void settingBtn5_Click(object sender, RoutedEventArgs e)
        {
            threeDot5.IsOpen = true;

        }

        private void settingBtn5_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot5.IsOpen = true;
        }

        private void settingsDot5_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu5.IsOpen = true;
        }

        private void settingsDot5_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu5.IsOpen = true;
        }

        private void settingsMenu5_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu5.IsOpen = false;
            threeDot5.IsOpen = false;
        }

        private void menu_Clear5_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["W_5s"] = "100 %";
        }

        private void menu_Settings5_Click(object sender, RoutedEventArgs e)
        {
            Settings_nav settings_Nav = new Settings_nav();
            settings_Nav.Show();
            settingsMenu5.IsOpen = false;
            threeDot5.IsOpen = false;
            settings_Nav.numTextEvent += Settings_Nav_numTextEvent;
            activeSettings[4] = true;
            Application.Current.Resources["table"] = 5;
            Application.Current.Resources["isNav"] = 2;
            Application.Current.Resources["settings_Header"] = "СПУФИНГ";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void groupBox_5_MouseLeave(object sender, MouseEventArgs e)
        {
         /*   settingsMenu5.IsOpen = false;
            threeDot5.IsOpen = false;*/
        }

        private void settingsMenu5_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot5.IsOpen = true;
        }

        private void GroupBox5_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu5.IsOpen = false;
            threeDot5.IsOpen = false;
        }

        private void turnOn_btn_Click(object sender, RoutedEventArgs e)
        {
            // comShS.SendSetParamFWS(tettaProperty, ??? )
            var paramFWS = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = 0, Deviation = 0, Duration = 0, Manipulation = 0, Modulation = 0 } };
           
            freqStr[0] = (string)Application.Current.Resources["txtF_1s"];
            digitsOnlyFreq[0] = Regex.Replace(freqStr[0], "[^0-9]", "");
            freqTable[0] = uint.Parse(digitsOnlyFreq[0]);

            freqStr[1] = (string)Application.Current.Resources["txtF_2s"];
            digitsOnlyFreq[1] = Regex.Replace(freqStr[1], "[^0-9]", "");
            freqTable[1] = uint.Parse(digitsOnlyFreq[1]);

            freqStr[2] = (string)Application.Current.Resources["txtF_3s"];
            digitsOnlyFreq[2] = Regex.Replace(freqStr[2], "[^0-9]", "");
            freqTable[2] = uint.Parse(digitsOnlyFreq[2]);



          
             var paramFWS1 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[0] * 1000, Deviation = deviationNum[0], Duration = 0, Manipulation = manipulationBytes[0] , Modulation = speedBytes[0] } };
             var paramFWS2 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[1], Deviation = deviationNum[1], Duration = 0, Manipulation = manipulationBytes[1], Modulation = speedBytes[1] } };
             var paramFWS3 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[2], Deviation = deviationNum[2], Duration = 0, Manipulation = manipulationBytes[2], Modulation = speedBytes[2] } };
           //  var paramFWS3 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[2], Deviation = deviationNum[2], Duration = 0, Manipulation = manipulationBytes[2], Modulation = speedBytes[2] } };
            // с deviation готово. аналогичная логика с manipulation и modulation (работает - протестировано)
            // TO DO: duration fix
            // сделать для такой таблицы paramfws и вызов соответствующего метода. продумать как этот // tettaProperty (в парамах и в методе sendset)
            // 
            // UI update
            groupBox.BorderThickness = new Thickness(3);
            groupBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
            groupBox_2.BorderThickness = new Thickness(3);
            groupBox_2.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
            groupBox_3.BorderThickness = new Thickness(3);
            groupBox_3.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
            groupBox_4.BorderThickness = new Thickness(3);
            groupBox_4.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
            groupBox_5.BorderThickness = new Thickness(3);
            groupBox_5.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));

            comShS.SendSetParamFWS(tettaProperty, paramFWS1);
            System.Threading.Thread.Sleep(10);
            comShS.SendSetParamFWS(tettaProperty, paramFWS2);
            System.Threading.Thread.Sleep(10);
            comShS.SendSetParamFWS(tettaProperty, paramFWS3);
            System.Threading.Thread.Sleep(10);

            if (isPower[0])
            {
                comShS.SendSetGNSS(gpsGlonass, (byte)powerNav);
            }
            else if (!isPower[0])
            {
                comShS.SendSetGNSS(gpsGlonass, beidouGalileo);
            }
            System.Threading.Thread.Sleep(10);
            if (isPower[1])
            {
                comShS.SendSetSPOOF((byte)powerSpoof);
            } else if (!isPower[1])
            {
                comShS.SendSetSPOOF();
            }

            if (applyBool)
            {
                for (byte i = 1; i <= 5; i++)
                {
                    comShS.SendStatus(i);
                }
            }

        }

        private void radiatoff_Btn_Click(object sender, RoutedEventArgs e)
        {
            // numOfTable  comShS.SendRadiatOff();
            string letterStr = Application.Current.Resources["table"].ToString();

            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);
            /* for (byte i = 1; i <= 5; i++)
             {
                 comShS.SendRadiatOff(i);
             }*/
            comShS.SendRadiatOff(0);
            groupBox_5.BorderThickness = new Thickness(1);
            groupBox_5.BorderBrush = Brushes.White;
            groupBox_4.BorderThickness = new Thickness(1);
            groupBox_4.BorderBrush = Brushes.White;
            groupBox_3.BorderThickness = new Thickness(1);
            groupBox_3.BorderBrush = Brushes.White;
            groupBox_2.BorderThickness = new Thickness(1);
            groupBox_2.BorderBrush = Brushes.White;
            groupBox.BorderThickness = new Thickness(1);
            groupBox.BorderBrush = Brushes.White;

        }

        private void circle2_btn_Click(object sender, RoutedEventArgs e)
        {
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);

          /*  if (applyBool)
            {*/
                for (byte i = 1; i <= 5; i++)
                {
                    comShS.SendStatus(i);
                    System.Threading.Thread.Sleep(10);
                }
            //}
          //  comShS.SendFullStatus(numOfTable); // насколько понимаю сюда fullstatus слать 
        }

        private void gpsl1_Tg_Click(object sender, RoutedEventArgs e)
        {
           
            if (gpsl1_Tg.IsChecked == false)
            {
                gpsGlonass.gpsL1 = true;
            } else
            {
                gpsGlonass.gpsL1 = false;
            }
        }

        private void gpsl2_Tg_Click(object sender, RoutedEventArgs e)
        {
          
            if (gpsl2_Tg.IsChecked == false)
            {
                gpsGlonass.gpsL2 = true;
            }
            else
            {
                gpsGlonass.gpsL2 = false;
            }
        }

        private void glns3_Tg_Click(object sender, RoutedEventArgs e)
        {
           
            if (glns3_Tg.IsChecked == false)
            {
                gpsGlonass.glnssL1 = true;
            }
            else
            {
                gpsGlonass.glnssL1 = false;
            }
        }

        private void glns4_Tg_Click(object sender, RoutedEventArgs e)
        {
           
            if (glns4_Tg.IsChecked == false)
            {
                gpsGlonass.glnssL2 = true;
            }
            else
            {
                gpsGlonass.glnssL2 = false;
            }
        }

        private void gnss2_Tg_Click(object sender, RoutedEventArgs e)
        {
            if (gnss2_Tg.IsChecked == false)
            {
                beidouGalileo.galileoL2 = true;
                beidouGalileo.beidouL2 = true;
            }
            else
            {
                beidouGalileo.galileoL2 = false;
                beidouGalileo.beidouL2 = false;
            }
            // if (gpsGlonass.glnssL1 == true && beidouGalileo.)
        }

        private void gnss1_Tg_Click(object sender, RoutedEventArgs e)
        {
            if (gnss1_Tg.IsChecked == false)
            {
                beidouGalileo.galileoL1 = true;
                beidouGalileo.beidouL1 = true;
            }
            else
            {
                beidouGalileo.galileoL1 = false;
                beidouGalileo.beidouL1 = false;
            }
        }

        private void statusApply_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (!applyBool)
            {
                vectorApply_img.Visibility = Visibility.Visible;
                applyBool = true;
            } else
            {
                vectorApply_img.Visibility = Visibility.Collapsed;
                applyBool = false;
            }
        }
    }


}
