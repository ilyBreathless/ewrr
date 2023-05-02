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
using System.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Settings_nav.xaml
    /// </summary>
    public partial class Settings_nav : Window
    {
        private bool isOld = false;
        public event EventHandler percentClicked;
        public event EventHandler powerFalseClicked;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<string> numTextEvent;
        public Settings_nav()
        {
            InitializeComponent();
            numText.AddHandler(TextBox.TextInputEvent,
             new TextCompositionEventHandler(numText_TextInput),
             true);
        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        public static bool IsValid2(string str)
        {
            int i;

            return int.TryParse(str, out i) && i >= 1 && i <= 100;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Resources["isSettings"] = "0";
        }

     

        private void isOld_btn_Click(object sender, RoutedEventArgs e)
        {
          
            if (!isOld)
            {
                numText.IsReadOnly = false;
                isOld = true;
                vect_Img.Visibility = Visibility.Visible;
                btn_MinNum.Visibility = Visibility.Visible;
                btn_PlusNum.Visibility = Visibility.Visible;
                percentClicked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                numText.IsReadOnly = true;
                btn_MinNum.Visibility = Visibility.Collapsed;
                btn_PlusNum.Visibility = Visibility.Collapsed;
                vect_Img.Visibility = Visibility.Collapsed;
                isOld = false;
                numText.Text = "100";
                powerFalseClicked?.Invoke(this, EventArgs.Empty);
            }
        }
        public void checkPercents(int cifra)
        {
         
            if (isControlLabel.Content.ToString() == "0")
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
                        Application.Current.Resources["W_spoof"] = numText.Text + " %";
                        break;
                    case 6:
                        Application.Current.Resources["W_6"] = numText.Text + " %";
                        break;
                    case 8:
                        Application.Current.Resources["W_frame"] = numText.Text + " %";
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
                        Application.Current.Resources["W_spoof"] = numText.Text + " %";
                        break;

                    case 8:
                        Application.Current.Resources["W_frame"] = numText.Text + " %";
                        break;
                    default:
                        break;
                }
            }
        }

        private void btn_PlusNum_Click(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(numText.Text);
            numTextEvent?.Invoke(this, numText.Text);
            if (i < 100)
                numText.Text = (i + 1).ToString();
            int numOfTable = Convert.ToInt32(hz.Content);
            if (numOfTable == 6)
            {
                checkPercents(6);
            }
            else
            {
                if (isNavLabel.Content.ToString() == "1")
                {
                    checkPercents(8);
                }
                else if (isNavLabel.Content.ToString() == "2")
                {
                    checkPercents(5);
                }
                else if (isNavLabel.Content.ToString() == "0")
                {
                    checkPercents(4);
                }
            }
                
        }

        private void btn_MinNum_Click(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(numText.Text);
            numTextEvent?.Invoke(this, numText.Text);
            if (i > 0)
                numText.Text = (i - 1).ToString();
            int numOfTable = Convert.ToInt32(hz.Content);
            if (numOfTable == 6)
            {
                checkPercents(6);
            }
            else
            {
                if (isNavLabel.Content.ToString() == "1")
                {
                    checkPercents(8);
                }
                else if (isNavLabel.Content.ToString() == "2")
                {
                    checkPercents(5);
                }
                else if (isNavLabel.Content.ToString() == "0")
                {
                    checkPercents(4);
                }
            }
        }

        private void numText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid2(((TextBox)sender).Text + e.Text);
            numTextEvent?.Invoke(this, numText.Text);
            int numOfTable = Convert.ToInt32(hz.Content);
            if (numOfTable == 6)
            {
                checkPercents(6);
            }
            else
            {
                if (isNavLabel.Content.ToString() == "1")
                {
                    checkPercents(8);
                }
                else if (isNavLabel.Content.ToString() == "2")
                {
                    checkPercents(5);
                }
                else if (isNavLabel.Content.ToString() == "0")
                {
                    checkPercents(4);
                }
             /*   if (e.Handled)
                {
                    MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 100)");
                }*/
            }
        }

        private void numText_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid2(((TextBox)sender).Text + e.Text);
            numTextEvent?.Invoke(this, numText.Text);
            int numOfTable = Convert.ToInt32(hz.Content);
            if (numOfTable == 6)
            {
                checkPercents(6);
            }
            else
            {
                if (isNavLabel.Content.ToString() == "1")
                {
                    checkPercents(8);
                }
                else if (isNavLabel.Content.ToString() == "2")
                {
                    checkPercents(5);
                }
                else if (isNavLabel.Content.ToString() == "0")
                {
                    checkPercents(4);
                }
              /*  if (e.Handled)
                {
                    MessageBox.Show("Проверте корректность ввода \n(допустим ввод цифр; число должно быть не более 100)");
                }*/
            }
        }

        private void numText_TextChanged(object sender, TextChangedEventArgs e)
        {
            numTextEvent?.Invoke(this, numText.Text);
        }
    }
}
