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
using System.Threading;
using System.Windows.Threading;

using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GrozaZ1Control.xaml
    /// </summary>

    public partial class GrozaZ1Control : UserControl
    {
        //    private Settings_circle vm;
        private bool[] inactive = { true, true, true, true, true, true };
        private bool[] activeSettings = { false, false, false, false, false, false };
        private bool[] preselectorActive = new bool[] { true, true, true, true, true, true, true, true, true, true };
        private int[] deviationNum = new int[6];
        private byte[] speedBytes = new byte[6];
        private bool applyBool = false;
        private string[] freqStr = new string[6];
        private byte[] manipulationBytes = new byte[6];
        private string[] digitsOnlyFreq = new string[6];
        private uint[] freqTable = new uint[6];
        private byte powerNav = 100;
        private byte[] powerTables = new byte[6] {100,100,100,100,100,100};
        private uint powerSpoof = 100;
        private int[] freqArr;
        private bool activeSircle = false;
        private int comboBoxValue_1;
        private int comboBoxValue_2;
        private int comboBoxValue_1t;
        private int comboBoxValue_2t;

        public int tettaProperty { get; set; }
        // private int[] frequencyArr = new int[5];

        public string SelectedValue { get; set; }
        SHS_DLL.ComSHS comShS = new SHS_DLL.ComSHS(0x04, 0x05);
        SHS_DLL.TParamFWS paramFWS = new SHS_DLL.TParamFWS();
        SHS_DLL.TGpsGlonass gpsGlonass = new SHS_DLL.TGpsGlonass();
        SHS_DLL.TBeidouGalileo beidouGalileo = new SHS_DLL.TBeidouGalileo();

        /*  public static readonly DependencyProperty FrequencyArrProperty =
            DependencyProperty.Register("FrequencyArr", typeof(int[]), typeof(GrozaZ1Control), new PropertyMetadata(null));


          public int[] FrequencyArr
          {
              get { return (int[])GetValue(FrequencyArrProperty); }
              set { SetValue(FrequencyArrProperty, value); }
          }*/

        public GrozaZ1Control()
        {

            InitializeComponent();
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSet += ComShS_OnConfirmSet;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmStatus += ComShS_OnConfirmStatus;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmStatusAntenna += ComShS_OnConfirmStatusAntenna;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmFullStatus += ComShS_OnConfirmFullStatus;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSwitch += ComShS_OnConfirmSwitch;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSetParam += ComShS_OnConfirmSetParam;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmGetParam += ComShS_OnConfirmGetParam;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnConfirmSaveParam += ComShS_OnConfirmSaveParam;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnRadiatOff += ComShS_OnRadiatOff;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnReadByte += Instance_OnReadByte;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnWriteByte += Instance_OnWriteByte;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnStatus += ComShS_OnStatus;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnSetIRI += ComShS_OnSetIRI;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnTestGNSS += ComShS_OnTestGNSS;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnSwitchPosition += ComShS_OnSwitchPosition;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnParamNaVi += ComShS_OnParamNaVi;
            WpfApp1.MainWindow.MyWrapperClass.Instance.OnVersion += ComShS_OnVersion;
        }
        private void AddToLog(string text)
        {
            logListBox.Items.Add(text);
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

        private void Instance_OnReadByte(object sender, SHS_DLL.ByteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnVersion(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnParamNaVi(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnSwitchPosition(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnTestGNSS(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnSetIRI(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnStatus(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnRadiatOff(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmSaveParam(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmGetParam(object sender, SHS_DLL.ConfirmGetParamEventArgs[] e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmSetParam(object sender, byte e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmSwitch(object sender, SHS_DLL.ConfirmSetSwitch e)
        {
          //  byte dfsdf = e.AmpCode;
          //  e.SectorNumber
        }

        private void ComShS_OnConfirmFullStatus(object sender, SHS_DLL.ConfirmFullStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmStatusAntenna(object sender, SHS_DLL.ConfirmStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmStatus(object sender, SHS_DLL.ConfirmStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComShS_OnConfirmSet(object sender, SHS_DLL.ConfirmSetEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrozaZ1Control_Loaded(object sender, RoutedEventArgs e)
        {
           /* var comboBox = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Name == "ReceiveCom")?.FindName("combo24") as ComboBox;
            if (comboBox != null)
            {
                comboBox.SelectionChanged += ComboBox_SelectionChanged;
            }*/
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

        private void Settings_circle_deviationEvent(object sender, string e)
        {
          //  paramFWS.Deviation = int.Parse(e); //возможно сделать не в textchanged метод а по нажатию на кнопку( чтобы отслеживало не один символ)

            string deviation = e;
            if (Application.Current.Resources["table"].ToString() == "1")
            {
                deviationNum[0] = int.Parse(deviation);
            }
            else if (Application.Current.Resources["table"].ToString() == "2")
            {
                deviationNum[1] = int.Parse(deviation);
            }
            else if (Application.Current.Resources["table"].ToString() == "3")
            {
                deviationNum[2] = int.Parse(deviation);
            }
            else if (Application.Current.Resources["table"].ToString() == "4")
            {
                deviationNum[3] = int.Parse(deviation);
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                deviationNum[4] = int.Parse(deviation);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      //      SelectedValue = (sender as ComboBox)?.SelectedItem?.ToString();
        }

        public static string txtBoxValue;

       
        public void ChangeLabels()
        {
            
        }

        public static void DoEvents()
        {

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Render,
                                                  new Action(delegate { }));
        }

        private void grozas_btn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();

            
        }
        private void firmware_Button_Click(object sender, RoutedEventArgs e)
        {
            Firmware firmware = new Firmware();
            firmware.Show();
        }
        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
        
           // comShS.SendParamFwsGnss(tettaProperty,gpsGlonass,beidouGalileo,paramFWS)
            settings_circle.FrequencyArrChanged += Settings_circle_FrequencyArrChanged;
           // settings_circle.Show(); // убрать show


        }

        private void Settings_circle_FrequencyArrChanged(object sender, int[] e)
        {
            Array.Copy(e, freqArr, e.Length);
        }

        private void intelian_Button_Click(object sender, RoutedEventArgs e)
        {
            Calibration calibration = new Calibration();
            calibration.Show();


        }
        private void setting_btn_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.textBoxValueChanged += Settings_textBoxValueChanged; ;
            settings.Show();
        }

        private void Settings_textBoxValueChanged(object sender, string e)
        {
            tettaProperty = int.Parse(e);

        }

        private void settings_circle_Click(object sender, RoutedEventArgs e)
        {
            //     string letterStr = Application.Current.Resources["table"].ToString();
            //     byte numOfTable;
            //     byte.TryParse(letterStr, out numOfTable);
            //    comShS.SendFullStatus(numOfTable);

            if (!activeSircle)
            {
                activeSircle = true;
                System.Threading.Thread thread = new System.Threading.Thread(StartLoop);
                thread.Start();
            }
            else
            {
                activeSircle = false;
            }
        }

        private void StartLoop()
        {
            while (activeSircle)
            {
                for (byte i = 1; i <= 6; i++)
                {
                    comShS.SendFullStatus(i);
                }

                System.Threading.Thread.Sleep(5000);
            }
        }

        private void groupBox_1_Click(object sender, RoutedEventArgs e)
        {
          
            Application.Current.Resources["table"] = 1;
          
        }

        private void groupBox_2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 2;
        }

        private void groupBox_3_Click(object sender, RoutedEventArgs e)
        {
          
            Application.Current.Resources["table"] = 3;
        }

        private void groupBox_4_Click(object sender, RoutedEventArgs e)
        {
       
            Application.Current.Resources["table"] = 4;
        }

        private void groupBox_5_Click(object sender, RoutedEventArgs e)
        {
           
            Application.Current.Resources["table"] = 5;

        }

        private void groupBox_6_Click(object sender, RoutedEventArgs e)
        {
            
            
            Application.Current.Resources["table"] = 6;
        }

        private void navBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings_nav settings_Nav = new Settings_nav();
          //  
            settings_Nav.Show();
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

        private void GroupBox5_btn_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void GroupBox6_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void settingBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot.IsOpen = true;
        }

        private void settingsDot_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu.IsOpen = true;
        }

        private void settingsDot_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu.IsOpen = true;
        }

        private void settingsMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu.IsOpen = false;
            threeDot.IsOpen = false;
        }

        private void menu_Clear_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_1"] = "МГц";
            Application.Current.Resources["W_1"] = "100 %";
            Application.Current.Resources["txtdF_1"] = "МГц";
        }

        private void menu_Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            settingsMenu.IsOpen = false;
            threeDot.IsOpen = false;

            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.powerNumEvent += Settings_circle_powerNumEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;

            activeSettings[0] = true;
           //tabl dobavit' comShS.SendFullStatus();
            Application.Current.Resources["table"] = 1;
            Application.Current.Resources["settings_Header"] = "430..440 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void Settings_circle_powerNumEvent(object sender, string e)
        {
             if (e == "")
            {
                e = "0";
            }
            string powerStr = e;
            if (Application.Current.Resources["table"].ToString() == "6")
            {
                powerNav = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "1")
            {
                powerTables[0] = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "2")
            {
                powerTables[1] = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "3")
            {
                powerTables[2] = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "4")
            {
                powerTables[3] = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                powerTables[4] = byte.Parse(powerStr);
            }
        }

        private void settingsMenu2_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu2.IsOpen = false;
            threeDot2.IsOpen = false;
        }

        private void menu_Clear2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_2"] = "МГц";
            Application.Current.Resources["W_2"] = "100 %";
            Application.Current.Resources["txtdF_2"] = "МГц";
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
            settings_circle.powerNumEvent += Settings_circle_powerNumEvent;

            activeSettings[1] = true;
            Application.Current.Resources["table"] = 2;
            Application.Current.Resources["settings_Header"] = "860..928 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void settingsDot2_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu2.IsOpen = true;
        }

        private void settingBtn2_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot2.IsOpen = true;
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

        private void settingsMenu3_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu3.IsOpen = false;
            threeDot3.IsOpen = false;
        }

        private void menu_Clear3_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_3"] = "МГц";
            Application.Current.Resources["W_3"] = "100 %";
            Application.Current.Resources["txtdF_3"] = "МГц";
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
            settings_circle.powerNumEvent += Settings_circle_powerNumEvent;

            activeSettings[2] = true;
            Application.Current.Resources["table"] = 3;
            Application.Current.Resources["settings_Header"] = "1130..1370 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void polygon_btn3_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 3;

            freqStr[2] = (string)Application.Current.Resources["txtF_3"];
            digitsOnlyFreq[2] = Regex.Replace(freqStr[2], "[^0-9]", "");
            freqTable[2] = uint.Parse(digitsOnlyFreq[2]);

            var paramFWS3 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[2], Deviation = deviationNum[2], Duration = 0, Manipulation = manipulationBytes[2], Modulation = speedBytes[2] } };
            if (applyBool)
            {
                comShS.SendFullStatus(3);
            }
            if (inactive[2])
            {
                comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS3, powerTables, powerNav);
                groupBox_3.BorderThickness = new Thickness(3);
                groupBox_3.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[2] = false;
            }
            else if (!inactive[2])
            {
                comShS.SendPreselectorOn(3);
                groupBox_3.BorderThickness = new Thickness(1);
                groupBox_3.BorderBrush = Brushes.White;
                inactive[2] = true;
            }
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

        private void settingsMenu4_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;
        }

        private void menu_Clear4_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_4"] = "МГц";
            Application.Current.Resources["W_4"] = "100 %";
            Application.Current.Resources["txtdF_4"] = "МГц";
        }

     

        private void menu_Settings4_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            settingsMenu4.IsOpen = false;
            threeDot4.IsOpen = false;

            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;
            settings_circle.powerNumEvent += Settings_circle_powerNumEvent;

            activeSettings[3] = true;
            Application.Current.Resources["table"] = 4;
            Application.Current.Resources["settings_Header"] = "2200..2600 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void polygon_btn4_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 4;

            freqStr[3] = (string)Application.Current.Resources["txtF_4"];
            digitsOnlyFreq[3] = Regex.Replace(freqStr[3], "[^0-9]", "");
            freqTable[3] = uint.Parse(digitsOnlyFreq[3]);

            var paramFWS4 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[3], Deviation = deviationNum[3], Duration = 0, Manipulation = manipulationBytes[3], Modulation = speedBytes[3] } };
            if (applyBool)
            {
                comShS.SendFullStatus(4);
            }
            if (inactive[3])
            {
                comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS4, powerTables, powerNav);
                groupBox_4.BorderThickness = new Thickness(3);
                groupBox_4.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[3] = false;
            }
            else if (!inactive[3])
            {
                comShS.SendPreselectorOn(4);
                groupBox_4.BorderThickness = new Thickness(1);
                groupBox_4.BorderBrush = Brushes.White;
                inactive[3] = true;
            }
        }

        private void settingBtn5_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot5.IsOpen = true;
        }

        private void settingBtn5_Click(object sender, RoutedEventArgs e)
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
            Application.Current.Resources["txtF_5"] = "МГц";
            Application.Current.Resources["W_5"] = "100 %";
            Application.Current.Resources["txtdF_5"] = "МГц";
        }

        private void menu_Settings5_Click(object sender, RoutedEventArgs e)
        {
            Settings_circle settings_circle = new Settings_circle();
            settings_circle.Show();
            settingsMenu5.IsOpen = false;
            threeDot5.IsOpen = false;
            settings_circle.deviationEvent += Settings_circle_deviationEvent;
            settings_circle.scanspeedEvent += Settings_circle_scanspeedEvent;
            settings_circle.manipulationEvent += Settings_circle_manipulationEvent;
            settings_circle.powerNumEvent += Settings_circle_powerNumEvent;


            activeSettings[4] = true;
            Application.Current.Resources["table"] = 5;
            Application.Current.Resources["settings_Header"] = "5725..5850 МГц";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void polygon_btn5_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 5;

            freqStr[4] = (string)Application.Current.Resources["txtF_5"];
            digitsOnlyFreq[4] = Regex.Replace(freqStr[4], "[^0-9]", "");
            freqTable[4] = uint.Parse(digitsOnlyFreq[4]);

            var paramFWS5 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[4], Deviation = deviationNum[4], Duration = 0, Manipulation = manipulationBytes[4], Modulation = speedBytes[4] } };
            if (applyBool)
            {
                comShS.SendFullStatus(5);
            }
            if (inactive[4])
            {
                comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS5, powerTables, powerNav);
                groupBox_5.BorderThickness = new Thickness(3);
                groupBox_5.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[4] = false;
            }
            else if (!inactive[4])
            {
                comShS.SendPreselectorOn(5);
                groupBox_5.BorderThickness = new Thickness(1);
                groupBox_5.BorderBrush = Brushes.White;
                inactive[4] = true;
            }
        }

        private void settingBtn6_MouseEnter(object sender, MouseEventArgs e)
        {
            threeDot6.IsOpen = true;
        }

        private void settingBtn6_Click(object sender, RoutedEventArgs e)
        {
            threeDot6.IsOpen = true;
        }

        private void settingsDot6_MouseEnter(object sender, MouseEventArgs e)
        {
            settingsMenu6.IsOpen = true;
        }

        private void settingsDot6_Click(object sender, RoutedEventArgs e)
        {
            settingsMenu6.IsOpen = true;
        }

        private void settingsMenu6_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu6.IsOpen = false;
            threeDot6.IsOpen = false;
        }

        private void menu_Clear6_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["txtF_6"] = "МГц";
            Application.Current.Resources["W_6"] = "100 %";
            Application.Current.Resources["txtdF_6"] = "МГц";
        }

        private void menu_Settings6_Click(object sender, RoutedEventArgs e)
        {
            Settings_nav settings_Nav = new Settings_nav();
            settings_Nav.Show();
            settings_Nav.numTextEvent += Settings_Nav_numTextEvent;
            settings_Nav.percentClicked += Settings_Nav_percentClicked;
            settings_Nav.powerFalseClicked += Settings_Nav_powerFalseClicked;
            //    settings_Nav.percentClicked += Settings_nav_percentClicked;
            settingsMenu6.IsOpen = false;
            threeDot6.IsOpen = false;



            activeSettings[5] = true;
            Application.Current.Resources["table"] = 6;
            Application.Current.Resources["settings_Header"] = "НАВИГАЦИЯ";
            Application.Current.Resources["isSettings"] = 1;
        }

        private void Settings_Nav_powerFalseClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Settings_Nav_percentClicked(object sender, EventArgs e)
        {
           
        }

        private void Settings_Nav_numTextEvent(object sender, string e)
        {
            if (e == "")
            {
                e = "0";
            }
           
            string powerStr = e;
            if (Application.Current.Resources["table"].ToString() == "6")
            {
                powerNav = byte.Parse(powerStr);
            } else if (Application.Current.Resources["table"].ToString() == "1")
            {
                powerTables[0] = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "2")
            {
                powerTables[1] = byte.Parse(powerStr);
            } else if (Application.Current.Resources["table"].ToString() == "3")
            {
                powerTables[2] = byte.Parse(powerStr);
            } else if (Application.Current.Resources["table"].ToString() == "4")
            {
                powerTables[3] = byte.Parse(powerStr);
            }
            else if (Application.Current.Resources["table"].ToString() == "5")
            {
                powerTables[4] = byte.Parse(powerStr);
            }

            // логика для всех таблиц
        }

        private void polygon_btn6_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 6;

            freqStr[5] = (string)Application.Current.Resources["txtF_6"];
            digitsOnlyFreq[5] = Regex.Replace(freqStr[5], "[^0-9]", "");
            freqTable[5] = uint.Parse(digitsOnlyFreq[5]);

            var paramFWS6 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[5], Deviation = deviationNum[5], Duration = 0, Manipulation = manipulationBytes[5], Modulation = speedBytes[5] } };
            if (applyBool)
            {
                comShS.SendFullStatus(6);
            }
            if (inactive[5])
            {
                comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS6, powerTables, powerNav);
                groupBox_6.BorderThickness = new Thickness(3);
                groupBox_6.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[5] = false;
            }
            else if (!inactive[5])
            {
                comShS.SendPreselectorOn(6);
                groupBox_6.BorderThickness = new Thickness(1);
                groupBox_6.BorderBrush = Brushes.White;
                inactive[5] = true;
            }
        }

        private void polygon_btn2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 2;

            freqStr[1] = (string)Application.Current.Resources["txtF_2"];
            digitsOnlyFreq[1] = Regex.Replace(freqStr[1], "[^0-9]", "");
            freqTable[1] = uint.Parse(digitsOnlyFreq[1]);

            var paramFWS2 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[1], Deviation = deviationNum[1], Duration = 0, Manipulation = manipulationBytes[1], Modulation = speedBytes[1] } };
            if (applyBool)
            {
                comShS.SendFullStatus(2);
            }
            if (inactive[1])
            {
                comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS2, powerTables, powerNav);
                groupBox_2.BorderThickness = new Thickness(3);
                groupBox_2.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[1] = false;
            }
            else if (!inactive[1])
            {
               
                groupBox_2.BorderThickness = new Thickness(1);
                groupBox_2.BorderBrush = Brushes.White;
                comShS.SendPreselectorOn(2);
                inactive[1] = true;
            }
        }

        private void polygon_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["table"] = 1;
                
            freqStr[0] = (string)Application.Current.Resources["txtF_1"];
            digitsOnlyFreq[0] = Regex.Replace(freqStr[0], "[^0-9]", "");
            freqTable[0] = uint.Parse(digitsOnlyFreq[0]);

            var paramFWS1 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[0], Deviation = deviationNum[0], Duration = 0, Manipulation = manipulationBytes[0], Modulation = speedBytes[0] } };
            if (applyBool)
            {
                comShS.SendFullStatus(1);
            }
            if (inactive[0])
            {
                comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS1, powerTables, powerNav);
                groupBox_1.BorderThickness = new Thickness(3);
                groupBox_1.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 191, 255));
                inactive[0] = false;
            }
            else if (!inactive[0])
            {
                comShS.SendPreselectorOn(1);
                groupBox_1.BorderThickness = new Thickness(1);
                groupBox_1.BorderBrush = Brushes.White;
                inactive[0] = true;
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

        private void GroupBox5_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu5.IsOpen = false;
            threeDot5.IsOpen = false;
        }

        private void GroupBox6_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            settingsMenu6.IsOpen = false;
            threeDot6.IsOpen = false;
        }

        private void sendParamFms_btn_Click(object sender, RoutedEventArgs e)
        {
            //   comShS.SendParamFwsGnss()
            freqStr[0] = (string)Application.Current.Resources["txtF_1"];
            digitsOnlyFreq[0] = Regex.Replace(freqStr[0], "[^0-9]", "");
            freqTable[0] = uint.Parse(digitsOnlyFreq[0]);

            freqStr[1] = (string)Application.Current.Resources["txtF_2"];
            digitsOnlyFreq[1] = Regex.Replace(freqStr[1], "[^0-9]", "");
            freqTable[1] = uint.Parse(digitsOnlyFreq[1]);

            freqStr[2] = (string)Application.Current.Resources["txtF_3"];
            digitsOnlyFreq[2] = Regex.Replace(freqStr[2], "[^0-9]", "");
            freqTable[2] = uint.Parse(digitsOnlyFreq[2]);

            freqStr[3] = (string)Application.Current.Resources["txtF_4"];
            digitsOnlyFreq[3] = Regex.Replace(freqStr[3], "[^0-9]", "");
            freqTable[3] = uint.Parse(digitsOnlyFreq[3]);

            freqStr[4] = (string)Application.Current.Resources["txtF_5"];
            digitsOnlyFreq[4] = Regex.Replace(freqStr[4], "[^0-9]", "");
            freqTable[4] = uint.Parse(digitsOnlyFreq[4]);

            var paramFWS1 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[0], Deviation = deviationNum[0], Duration = 0, Manipulation = manipulationBytes[0], Modulation = speedBytes[0] } };
            var paramFWS2 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[1], Deviation = deviationNum[1], Duration = 0, Manipulation = manipulationBytes[1], Modulation = speedBytes[1] } };
            var paramFWS3 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[2], Deviation = deviationNum[2], Duration = 0, Manipulation = manipulationBytes[2], Modulation = speedBytes[2] } };
            var paramFWS4 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[3], Deviation = deviationNum[3], Duration = 0, Manipulation = manipulationBytes[3], Modulation = speedBytes[3] } };
            var paramFWS5 = new SHS_DLL.TParamFWS[] { new SHS_DLL.TParamFWS() { Freq = freqTable[4], Deviation = deviationNum[4], Duration = 0, Manipulation = manipulationBytes[4], Modulation = speedBytes[4] } };
         

            comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS1, powerTables, powerNav);
            comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS2, powerTables, powerNav);
            comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS3, powerTables, powerNav);
            comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS4, powerTables, powerNav);
            comShS.SendParamFwsGnss(tettaProperty, gpsGlonass, beidouGalileo, paramFWS5, powerTables, powerNav);
          
        }

        private void preselector_On_Click(object sender, RoutedEventArgs e)
        {
            string letterStr = Application.Current.Resources["table"].ToString();
            byte numOfTable;
            byte.TryParse(letterStr, out numOfTable);
            comShS.SendPreselectorOn(numOfTable); // куда это в треугольники
        }

        private void receiveSwitch_btn_Click(object sender, RoutedEventArgs e)
        {
            // comShS.SendSetReceivingSwitches();
            ReceiveCom receiveCom = new ReceiveCom();
            receiveCom.Show();

            if (receiveCom != null)
            {
                receiveCom.ComboBoxValueChanged += ReceiveCom_ComboBoxValueChanged;
                receiveCom.ComboBox2ValueChanged += ReceiveCom_ComboBox2ValueChanged;
            }
            /*
                        string selectedValue = receiveCom.GetSelectedValue();
                        int value1 = Convert.ToInt32(comboBox1.SelectedValue);
                        int value2 = Convert.ToInt32(comboBox2.SelectedValue);*/

            // объединить два значения в один массив байтов
          

            byte[] bytes = new byte[2];
            bytes[0] = (byte)comboBoxValue_1;
            bytes[1] = (byte)comboBoxValue_2;
            /*  bytes[0] = (byte)value1;

              bytes[1] = (byte)value2;*/

           comShS.SendSetReceivingSwitches(bytes);

            
        }

        private void ReceiveCom_ComboBox2ValueChanged(object sender, string e)
        {
            comboBoxValue_2 = Convert.ToInt32(e);

            byte[] bytes = new byte[2];
            bytes[0] = (byte)comboBoxValue_1;
            bytes[1] = (byte)comboBoxValue_2;
            comShS.SendSetReceivingSwitches(bytes);

        }

        private void ReceiveCom_ComboBoxValueChanged(object sender, string e)
        {
            comboBoxValue_1 = Convert.ToInt32(e);

            byte[] bytes = new byte[2];
            bytes[0] = (byte)comboBoxValue_1;
            bytes[1] = (byte)comboBoxValue_2;
            comShS.SendSetReceivingSwitches(bytes);
        }

        private void transmissionSwitch_btn_Click(object sender, RoutedEventArgs e)
        {
            //   comShS.SendSetTransmissionSwitches();
            TransmissionCom transmissionCom = new TransmissionCom();
            transmissionCom.Show();

            if (transmissionCom != null)
            {
                transmissionCom.ComboBoxValueChanged_t += TransmissionCom_ComboBoxValueChanged_t;
                transmissionCom.ComboBox2ValueChanged_t += TransmissionCom_ComboBox2ValueChanged_t;
            }

            byte[] bytes = new byte[2];
            bytes[0] = (byte)comboBoxValue_1t;
            bytes[1] = (byte)comboBoxValue_2t;

            comShS.SendSetReceivingSwitches(bytes);
        }

        private void TransmissionCom_ComboBox2ValueChanged_t(object sender, string e)
        {
            comboBoxValue_2t = Convert.ToInt32(e);
        }

        private void TransmissionCom_ComboBoxValueChanged_t(object sender, string e)
        {
            comboBoxValue_1t = Convert.ToInt32(e);
        }

        private void tgfirst_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (tgfirst_btn.IsChecked == false)
            {
                preselectorActive[0] = true;  
                comShS.SendParamPreselector(preselectorActive);
            } else
            {
                preselectorActive[0] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgsecond_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgsecond_btn.IsChecked == false)
            {
                preselectorActive[1] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[1] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgthird_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgthird_btn.IsChecked == false)
            {
                preselectorActive[2] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[2] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgfourth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgfourth_btn.IsChecked == false)
            {
                preselectorActive[3] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[3] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgfifth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgfifth_btn.IsChecked == false)
            {
                preselectorActive[4] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[4] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgsixth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgsixth_btn.IsChecked == false)
            {
                preselectorActive[5] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[5] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgseventh_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgseventh_btn.IsChecked == false)
            {
                preselectorActive[6] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[6] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgeighth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgeighth_btn.IsChecked == false)
            {
                preselectorActive[7] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[7] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgninth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgninth_btn.IsChecked == false)
            {
                preselectorActive[8] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[8] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void tgtenth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tgtenth_btn.IsChecked == false)
            {
                preselectorActive[9] = true;
                comShS.SendParamPreselector(preselectorActive);
            }
            else
            {
                preselectorActive[9] = false;
                comShS.SendParamPreselector(preselectorActive);
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gpls1_tg_Click(object sender, RoutedEventArgs e)
        {
          
            if (gpls1_tg.IsChecked == false)
            {
                gpsGlonass.gpsL1 = true;
            } else
            {
                gpsGlonass.gpsL1 = false;
            }
        }

        private void gpls2_tg_Click(object sender, RoutedEventArgs e)
        {
          
            if (gpls2_tg.IsChecked == false)
            {
                gpsGlonass.gpsL2 = true;
            }
            else
            {
                gpsGlonass.gpsL2 = false;
            }
        }

        private void glnsl2_tg_Click(object sender, RoutedEventArgs e)
        {
           
            if (glnsl2_tg.IsChecked == false)
            {
                gpsGlonass.glnssL2 = true;
            }
            else
            {
                gpsGlonass.glnssL2 = false;
            }
        }

        private void glnsl1_tg_Click(object sender, RoutedEventArgs e)
        {
           
            if (glnsl1_tg.IsChecked == false)
            {
                gpsGlonass.glnssL1 = true;
            }
            else
            {
                gpsGlonass.glnssL1 = false;
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

        private void fullStatus_btn_Click(object sender, RoutedEventArgs e)
        {
           
            for (byte i = 1; i <= 6; i++)
            {
                comShS.SendFullStatus(i);
                System.Threading.Thread.Sleep(10);
            }
        }

        private void fullStatus_btn_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void statusApply_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!applyBool)
            {
                vectorApply_img.Visibility = Visibility.Visible;
                applyBool = true;
            }
            else
            {
                vectorApply_img.Visibility = Visibility.Collapsed;
                applyBool = false;
            }
        }
    }
}
