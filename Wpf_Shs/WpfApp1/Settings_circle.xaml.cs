using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Numerics;
using System.Text.RegularExpressions;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Settings_circle.xaml
    /// </summary>
    public partial class Settings_circle : Window 
    {
        public static string F;
        public static string deltaF;
        public static string N;
        public static int W;
        public static string isSet;
        private bool isOld = false;
        public event EventHandler<string> deviationEvent;
        public event EventHandler<string> manipulationEvent;
        public event EventHandler<string> scanspeedEvent;
        public event EventHandler<string> powerNumEvent;
        //    private int[] frequencyArr = new int[5];

        private string[] type { get; set; }

        GrozaZ1Control grozaZ1Control = new GrozaZ1Control();
      

        public Settings_circle()
        {
            InitializeComponent();
            
           
            
            this.freqTextBox.TextInput += new TextCompositionEventHandler(freqTextBox_TextInput);

            numText.AddHandler(TextBox.TextInputEvent,
                   new TextCompositionEventHandler(numText_TextInput),
                   true);
            /* numText.KeyDown += (sender, eventArgs) => {
                 if (!Char.IsDigit(eventArgs.KeyChar))
                     eventArgs.Handled = true;
             };*/
        }

        public event EventHandler<int[]> FrequencyArrChanged;

        private int[] frequencyArr = new int[5];

        public int[] FrequencyArr
        {
            get => frequencyArr;
            set
            {
                frequencyArr = value;
                FrequencyArrChanged?.Invoke(this, frequencyArr.ToArray());
            }
        }






        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  F = (sender as ComboBox).Text;

            if (Type.SelectedIndex == 1)
            {
                manipulation_mks.Visibility = Visibility.Visible;
                manipulation_Tb.Visibility = Visibility.Visible;
                deviation_khz.Visibility = Visibility.Collapsed;
                deviation_Tb.Visibility = Visibility.Collapsed;
                scanspeed_khzs.Visibility = Visibility.Collapsed;
                scanspeedApply_btn.Visibility = Visibility.Collapsed;
                deviationApply_btn.Visibility = Visibility.Collapsed;
                scanspeed_Tb.Visibility = Visibility.Collapsed;
                manipulationApply_btn.Visibility = Visibility.Visible;
                this.Height = 260;
            } else if (Type.SelectedIndex == 0) {
                manipulation_mks.Visibility = Visibility.Collapsed;
                manipulation_Tb.Visibility = Visibility.Collapsed;
                deviation_khz.Visibility = Visibility.Collapsed;
                deviation_Tb.Visibility = Visibility.Collapsed;
                manipulationApply_btn.Visibility = Visibility.Collapsed;
                scanspeedApply_btn.Visibility = Visibility.Collapsed;
                deviationApply_btn.Visibility = Visibility.Collapsed;
                scanspeed_khzs.Visibility = Visibility.Collapsed;
                scanspeed_Tb.Visibility = Visibility.Collapsed;
                this.Height = 230;
            } else if (Type.SelectedIndex == 2 || Type.SelectedIndex == 3)
            {
                manipulation_mks.Visibility = Visibility.Collapsed;
                manipulation_Tb.Visibility = Visibility.Collapsed;
                deviation_khz.Visibility = Visibility.Visible;
                deviation_Tb.Visibility = Visibility.Visible;
                scanspeedApply_btn.Visibility = Visibility.Visible;
                deviationApply_btn.Visibility = Visibility.Visible;
                manipulationApply_btn.Visibility = Visibility.Collapsed;
                scanspeed_khzs.Visibility = Visibility.Visible;
                scanspeed_Tb.Visibility = Visibility.Visible;
                this.Height = 280;
            }
          
        }

        private void freqTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
           
                
                int numOfTable = Convert.ToInt32(hz.Content);

             //   check(numOfTable);
              

          /*  if (e.Handled)
            {
                MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 4000)");
            }*/

        }

        private void widthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
         
            if (widthTextBox.Text != "0")
            {
                int numOfTable = Convert.ToInt32(hz.Content);
                checkWidth(numOfTable);
            }

           /* if (e.Handled)
            {
                MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 4000)");
            }*/
        }

        public string Data
        {
            get
            {
                return widthTextBox.Text;
            }
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
            int numOfTable = Convert.ToInt32(hz.Content);
           
            Application.Current.Resources["isSettings"] = "0";
       
        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
           
            int numOfTable = Convert.ToInt32(hz.Content);
           // check(numOfTable);

        }

        public void checkPercents(int cifra)
        {
            if (isControlLabel.Content == "0")
            {
                switch (cifra)
                {
                    case 1:
                        Application.Current.Resources["W_1"] = numText.Text + " %";
                        break;

                    case 2:
                        Application.Current.Resources["W_2"] = numText.Text + " %";
                        break;
                    case 3:
                        Application.Current.Resources["W_3"] = numText.Text + " %";
                        break;
                    case 4:
                        Application.Current.Resources["W_4"] = numText.Text + "%";
                        break;
                    case 5:
                        Application.Current.Resources["W_5"] = numText.Text + " %";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (cifra)
                {
                    case 1:
                        Application.Current.Resources["W_1s"] = numText.Text + " %";
                        break;
                    case 2:
                        Application.Current.Resources["W_2s"] = numText.Text + " %";
                        break;
                    case 3:
                        Application.Current.Resources["W_3s"] = numText.Text + " %";
                        break;
                    case 4:
                        Application.Current.Resources["W_4s"] = numText.Text + " %";
                        break;
                    case 5:
                        Application.Current.Resources["W_5s"] = numText.Text + " %";
                        break;
                    default:
                        break;
                }
            }
        }
      /*  public void check(int cifra)
        {
           
          if (isControlLabel.Content == "0")
            {
                switch (cifra)
                {
                    case 1:
                        Application.Current.Resources["txtF_1"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"]; ;
                        break;
                    case 2:
                        Application.Current.Resources["txtF_2"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        break;
                    case 3:
                        Application.Current.Resources["txtF_3"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        break;
                    case 4:
                        Application.Current.Resources["txtF_4"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        break;
                    case 5:
                        Application.Current.Resources["txtF_5"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        break;
                    default:
                        break;
                }
            } else
            {
            //    switch (cifra)
                {
                    case 1:
                   //     Application.Current.Resources["txtF_1s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"]; ;
                        Application.Current.Resources["settings_Header"] = "100..500 МГц";
                        break;

                    case 2:
                        Application.Current.Resources["txtF_2s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"]; ;
                        Application.Current.Resources["settings_Header"] = "500..2500 МГц";
                        break;
                    case 3:
                        Application.Current.Resources["txtF_3s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"]; ;
                        break;
                    case 4:
                        Application.Current.Resources["txtF_4s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"]; ;
                        break;
                    case 5:
                        Application.Current.Resources["txtF_5s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"]; ;
                        break;
                    default:
                        break;
                }
            }
        }*/
        public void checkWidth(int cifra)
        {
            if (isControlLabel.Content == "0")
            {
                switch (cifra)
                {
                    case 1:
                        Application.Current.Resources["txtdF_1"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                       
                        break;
                    case 2:
                        Application.Current.Resources["txtdF_2"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                       
                        break;
                    case 3:
                        Application.Current.Resources["txtdF_3"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    case 4:
                        Application.Current.Resources["txtdF_4"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    case 5:
                        Application.Current.Resources["txtdF_5"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    default:
                        break;
                }
            } else
            {
                switch (cifra)
                {
                    case 1:
                        Application.Current.Resources["txtdF_1s"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    case 2:
                        Application.Current.Resources["txtdF_2s"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    case 3:
                        Application.Current.Resources["txtdF_3s"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    case 4:
                        Application.Current.Resources["txtdF_4s"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    case 5:
                        Application.Current.Resources["txtdF_5s"] = widthTextBox.Text + " " + Application.Current.Resources["kHz"]; ;
                        break;
                    default:
                        break;
                }
            }
        }

        private void freqTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           /* int numOfTable = Convert.ToInt32(hz.Content);
            if (isControlLabel.Content == "0")
            {
                switch (numOfTable)
                {
                    case 1:
                        e.Handled = !IsValid1z(((TextBox)sender).Text + e.Text);
                        break;
                    case 2:
                        e.Handled = !IsValid2z(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3z(((TextBox)sender).Text + e.Text);
                        break;
                    case 4:
                        e.Handled = !IsValid4z(((TextBox)sender).Text + e.Text);
                        break;
                    case 5:
                        e.Handled = !IsValid5z(((TextBox)sender).Text + e.Text);
                        break;
                }
            }
            else
            {
                switch (numOfTable)
                {
                    case 1:
                      
                        break;
                    case 2:
                        e.Handled = !IsValid2s(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3s(((TextBox)sender).Text + e.Text);
                        break;
                  
                }
            }*/
                      

        }
        public static bool IsValid(string str)
        {
            int i;
          
            return int.TryParse(str, out i) && i >= 5 && i <= 2500;

        }
        public static bool IsValid1(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 500;

        }
        public static bool IsValid2s(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 2500;

        }
        public static bool IsValid3s(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 6000;

        }
        public static bool IsValid1z(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 440;

        }
        public static bool IsValid2z(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 928;

        }
        public static bool IsValid3z(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 1370;

        }
        public static bool IsValid4z(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 2600;

        }
        public static bool IsValid5z(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 5850;

        }
        public static bool IsValid2(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 100;

        }
        private void freqTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            
          /*  int numOfTable = Convert.ToInt32(hz.Content);
            if (isControlLabel.Content == "0")
            {
                switch (numOfTable)
                {
                    case 1:
                        e.Handled = !IsValid1z(((TextBox)sender).Text + e.Text);
                        break;
                    case 2:
                        e.Handled = !IsValid2z(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3z(((TextBox)sender).Text + e.Text);
                        break;
                    case 4:
                        e.Handled = !IsValid4z(((TextBox)sender).Text + e.Text);
                        break;
                    case 5:
                        e.Handled = !IsValid5z(((TextBox)sender).Text + e.Text);
                        break;
                }
            }
            else
            {
                switch (numOfTable)
                {
                    case 1:
                //        e.Handled = !IsValid1(((TextBox)sender).Text + e.Text);
                       
                        break;
                    case 2:
                        e.Handled = !IsValid2s(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3s(((TextBox)sender).Text + e.Text);
                        break;

                }

                // int numOfTable = Convert.ToInt32(hz.Content);
          //      check(numOfTable);
                *//*if (e.Handled)
                {
                    MessageBox.Show("Проверте корректность ввода \n(число должно соответствовать диапазону выбранной таблицы)");
                }*//*
            }*/
        }

        private void widthTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int numOfTable = Convert.ToInt32(hz.Content);
            if (isControlLabel.Content == "0")
            {
                switch (numOfTable)
                {
                    case 1:
                        e.Handled = !IsValid1z(((TextBox)sender).Text + e.Text);
                        break;
                    case 2:
                        e.Handled = !IsValid2z(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3z(((TextBox)sender).Text + e.Text);
                        break;
                    case 4:
                        e.Handled = !IsValid4z(((TextBox)sender).Text + e.Text);
                        break;
                    case 5:
                        e.Handled = !IsValid5z(((TextBox)sender).Text + e.Text);
                        break;
                }
            }
            else
            {
                switch (numOfTable)
                {
                    case 1:
                        e.Handled = !IsValid1(((TextBox)sender).Text + e.Text);

                        break;
                    case 2:
                        e.Handled = !IsValid2s(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3s(((TextBox)sender).Text + e.Text);
                        break;

                }
            }

               /* if (e.Handled)
            {
                MessageBox.Show("Проверте корректность ввода \n(число должно соответствовать диапазону выбранной таблицы)");
            }*/
        }

        private void widthTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            int numOfTable = Convert.ToInt32(hz.Content);

            if (isControlLabel.Content == "0")
            {
                switch (numOfTable)
                {
                    case 1:
                        e.Handled = !IsValid1z(((TextBox)sender).Text + e.Text);
                        break;
                    case 2:
                        e.Handled = !IsValid2z(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3z(((TextBox)sender).Text + e.Text);
                        break;
                    case 4:
                        e.Handled = !IsValid4z(((TextBox)sender).Text + e.Text);
                        break;
                    case 5:
                        e.Handled = !IsValid5z(((TextBox)sender).Text + e.Text);
                        break;
                }
            }
            else
            {
                switch (numOfTable)
                {
                    case 1:
                        e.Handled = !IsValid1(((TextBox)sender).Text + e.Text);

                        break;
                    case 2:
                        e.Handled = !IsValid2s(((TextBox)sender).Text + e.Text);
                        break;
                    case 3:
                        e.Handled = !IsValid3s(((TextBox)sender).Text + e.Text);
                        break;

                }

            }
            checkWidth(numOfTable);
           /* if (e.Handled)
            {
                MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 4000)");
            }*/
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btn_PlusNum_Click(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(numText.Text);
            powerNumEvent?.Invoke(this, numText.Text);
            if (i < 100)
              numText.Text = (i + 1).ToString();
            int numOfTable = Convert.ToInt32(hz.Content);
            checkPercents(numOfTable);
        }

        private void btn_MinNum_Click(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(numText.Text);
            powerNumEvent?.Invoke(this, numText.Text);
            if (i > 0)
                numText.Text = (i - 1).ToString();
            int numOfTable = Convert.ToInt32(hz.Content);
            checkPercents(numOfTable);
        }

       


      

        private void numText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !IsValid2(((TextBox)sender).Text + e.Text);
            powerNumEvent?.Invoke(this, numText.Text);
            int numOfTable = Convert.ToInt32(hz.Content);
            checkPercents(numOfTable);
          /*  if (e.Handled)
            {
                MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 100)");
            }*/
        }


        private void isOld_btn_Click(object sender, RoutedEventArgs e)
        {
            int numOfTable = Convert.ToInt32(hz.Content);
            checkPercents(numOfTable);
            if (!isOld)
            {
                numText.IsReadOnly = false;
                isOld = true;
                vect_Img.Visibility = Visibility.Visible;
                btn_MinNum.Visibility = Visibility.Visible;
                btn_PlusNum.Visibility = Visibility.Visible;
            } else
            {
                numText.IsReadOnly = true;
                btn_MinNum.Visibility = Visibility.Collapsed;
                btn_PlusNum.Visibility = Visibility.Collapsed;
                vect_Img.Visibility = Visibility.Collapsed;
                numText.Text = "100";
                checkPercents(numOfTable);
                isOld = false;
                //numText.Text = "100";

            }
        }

    

        private void numText_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid2(((TextBox)sender).Text + e.Text);
            powerNumEvent?.Invoke(this, numText.Text);
            int numOfTable = Convert.ToInt32(hz.Content);
            checkPercents(numOfTable);
          /*  if (e.Handled)
            {
                MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 100)");
            }*/
        }

        private void freqTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void widthTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void numText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void freqTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void freqApply_btn_Click(object sender, RoutedEventArgs e)
        {
            int numOfTable = Convert.ToInt32(hz.Content);
            if (isControlLabel.Content == "1")
            {
                switch (numOfTable)
                {
                    case 1:
                        if (int.Parse(freqTextBox.Text) < 100)
                        {
                            freqTextBox.Text = "100";
                            Application.Current.Resources["txtF_1s"] = 100 + " " + Application.Current.Resources["mHz"];
                            
                        }
                        else if (int.Parse(freqTextBox.Text) > 500)
                        {
                            freqTextBox.Text = "500";
                            Application.Current.Resources["txtF_1s"] = 500 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                            Application.Current.Resources["txtF_1s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        }
                        break;

                    case 2:
                        if (int.Parse(freqTextBox.Text) < 500)
                        {
                            freqTextBox.Text = "500";
                            Application.Current.Resources["txtF_2s"] = 500 + " " + Application.Current.Resources["mHz"];
                        }
                        else if (int.Parse(freqTextBox.Text) > 2500)
                        {
                            freqTextBox.Text = "2500";
                            Application.Current.Resources["txtF_2s"] = 2500 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                            Application.Current.Resources["txtF_2s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        }
                        break;
                    case 3:
                        if (int.Parse(freqTextBox.Text) < 2500)
                        {
                            freqTextBox.Text = "2500";
                            Application.Current.Resources["txtF_3s"] = 2500 + " " + Application.Current.Resources["mHz"];
                        }
                        else if (int.Parse(freqTextBox.Text) > 6000)
                        {
                            freqTextBox.Text = "6000";
                            Application.Current.Resources["txtF_3s"] = 6000 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                            Application.Current.Resources["txtF_3s"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                        }
                        break;

                    default:
                        break;
                }
            } else
            {
                switch (numOfTable)
                {
                    case 1:
                        if (int.Parse(freqTextBox.Text) < 430)
                        {
                            freqTextBox.Text = "430";
                            
                            Application.Current.Resources["txtF_1"] = 430 + " " + Application.Current.Resources["mHz"];
                         
                            frequencyArr[0] = 430;
                        }
                        else if (int.Parse(freqTextBox.Text) > 440)
                        {
                            freqTextBox.Text = "440";
                         
                            Application.Current.Resources["txtF_1"] = 440 + " " + Application.Current.Resources["mHz"];
                        
                        }
                        else
                        {
                            Application.Current.Resources["txtF_1"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                            string freqStr = Application.Current.Resources["txtF_1"].ToString();
                            frequencyArr[0] = int.Parse(freqStr);
                        }
                        break;
                    case 2:
                        if (int.Parse(freqTextBox.Text) < 860)
                        {
                            freqTextBox.Text = "860";
                            frequencyArr[1] = 860;
                            Application.Current.Resources["txtF_2"] = 860 + " " + Application.Current.Resources["mHz"];
                        }
                        else if (int.Parse(freqTextBox.Text) > 928)
                        {
                            freqTextBox.Text = "928";
                            frequencyArr[1] = 928;
                            Application.Current.Resources["txtF_2"] = 928 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                            Application.Current.Resources["txtF_2"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                            string freqStr = Application.Current.Resources["txtF_2"].ToString();
                            frequencyArr[1] = int.Parse(freqStr);
                        }
                        break;
                    case 3:
                        if (int.Parse(freqTextBox.Text) < 1130)
                        {
                            freqTextBox.Text = "1130";
                            frequencyArr[2] = 1130;
                            Application.Current.Resources["txtF_3"] = 1130 + " " + Application.Current.Resources["mHz"];
                        }
                        else if (int.Parse(freqTextBox.Text) > 1370)
                        {
                            freqTextBox.Text = "1370";
                            frequencyArr[2] = 1370;
                            Application.Current.Resources["txtF_3"] = 1370 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                            Application.Current.Resources["txtF_3"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                            string freqStr = Application.Current.Resources["txtF_3"].ToString();
                            frequencyArr[2] = int.Parse(freqStr);
                        }
                        break;
                    case 4:
                        if (int.Parse(freqTextBox.Text) < 2200)
                        {
                            freqTextBox.Text = "2200";
                            frequencyArr[3] = 2200;
                            Application.Current.Resources["txtF_4"] = 2200 + " " + Application.Current.Resources["mHz"];
                        }
                        else if (int.Parse(freqTextBox.Text) > 2600)
                        {
                            freqTextBox.Text = "2600";
                            frequencyArr[3] = 1370;
                            Application.Current.Resources["txtF_4"] = 2600 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                           Application.Current.Resources["txtF_4"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                            string freqStr = Application.Current.Resources["txtF_2"].ToString();
                            frequencyArr[3] = int.Parse(freqStr);
                        }
                        break;
                        
                    case 5:
                        if (int.Parse(freqTextBox.Text) < 5725)
                        {
                            freqTextBox.Text = "5725";
                            frequencyArr[3] = 5725;
                            Application.Current.Resources["txtF_5"] = 5725 + " " + Application.Current.Resources["mHz"];
                        }
                        else if (int.Parse(freqTextBox.Text) > 5850)
                        {
                            freqTextBox.Text = "5850";
                            frequencyArr[3] = 5850;
                            Application.Current.Resources["txtF_5"] = 5850 + " " + Application.Current.Resources["mHz"];
                        }
                        else
                        {
                            Application.Current.Resources["txtF_5"] = freqTextBox.Text + " " + Application.Current.Resources["mHz"];
                            string freqStr = Application.Current.Resources["txtF_2"].ToString();
                            frequencyArr[5] = int.Parse(freqStr);
                        }
                        
                        break;
                    default: break;

                }
            }
        }

        private void scanspeed_Tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является числом, отменяем ввод
            }
        }

        private void deviation_Tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является числом, отменяем ввод
            }
        }

        private void manipulation_Tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является числом, отменяем ввод
            }
        }

        private void scanspeed_Tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) { e.Handled = true; }
        }

        private void deviation_Tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) { e.Handled = true; }
        }

        private void manipulation_Tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) { e.Handled = true; }
        }

        private void manipulation_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            manipulationEvent?.Invoke(this, manipulation_Tb.Text);
        }

        private void deviation_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            deviationEvent?.Invoke(this, deviation_Tb.Text);
        }

        private void scanspeed_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            scanspeedEvent?.Invoke(this, scanspeed_Tb.Text);
        }

        private void manipulationApply_btn_Click(object sender, RoutedEventArgs e)
        {
            manipulationEvent?.Invoke(this, manipulation_Tb.Text);
        }

        private void deviationApply_btn_Click(object sender, RoutedEventArgs e)
        {
            deviationEvent?.Invoke(this, deviation_Tb.Text);
        }

        private void scanspeedApply_btn_Click(object sender, RoutedEventArgs e)
        {
            scanspeedEvent?.Invoke(this, scanspeed_Tb.Text);
        }
    }
}
