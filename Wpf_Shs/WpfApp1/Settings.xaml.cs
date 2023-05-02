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
using SHS_DLL;
using System.IO.Ports;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public string[] langs{ get; set; }
        public string[] speeds { get; set; }
        private string[] ports { get; set; }
        private bool isOpenPort = false;
       
        SHS_DLL.ComSHS comShS = new SHS_DLL.ComSHS(0x04, 0x05);
        public Settings()
        {
            InitializeComponent();
            DataContext = this;
            ports = SerialPort.GetPortNames();
            speeds = new string[] { "110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200", "128000", "256000" };
            foreach (string s in ports)
                port_ComboBox.Items.Add(s);
            //   langs = new string[] { "Русский", "Английский" };
            //  DataContext = this;
            /*   foreach (string s in langs)
                   language_Combobox.Items.Add(s);*/
        }
         public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ButtonClicked;
        public event EventHandler CancelClicked;
        public event EventHandler<string> textBoxValueChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _globalString;
        public string GlobalString
        {
            get { return _globalString;  }
            set
            {
                _globalString = value;
                OnPropertyChanged(nameof(GlobalString));
            }
        }

        private void language_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // получаем ссылку на главное окно из свойства Tag у UserControl
            //MainWindow mainWindow = new MainWindow();
            // MainWindow mainWindow = this.Tag as MainWindow;

            // получаем выбранный элемент в Combobox как строку (ru или en)
            //  string language = language_Combobox.SelectedItem as string;

            // вызываем метод для изменения языка главного окна с параметром language 
            // mainWindow.ChangeLanguage("lang.en-US");
       //     OnPropertyChanged(nameof(Application.Current.Resources["lang_Change"]);
            if (language_Combobox.SelectedIndex == 0)
            {
                Application.Current.Resources["lang_Change"] = "Rus";
                GlobalString = "Rus";
                OnPropertyChanged(nameof(GlobalString));
            } else
            {
                Application.Current.Resources["lang_Change"] = "Eng";
                GlobalString = "Eng";
                OnPropertyChanged(GlobalString);
                
            }

           
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(speed_ComboBox.SelectedItem.ToString(), out int portSpeed);
            if (!isOpenPort)
            {
                isOpenPort = MainWindow.MyWrapperClass.Instance.OpenPort(port_ComboBox.SelectedItem.ToString(), Convert.ToInt32(portSpeed), Parity.None, 8, StopBits.One);
             
               // isOpenPort = true;
            }
            Cancel.IsEnabled = true;
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
            comShS.ClosePort();
            isOpenPort = false;
           
        }

        private void time_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void time_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }


        private void time_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxValueChanged?.Invoke(this, e.ToString());
        }
    }
}
