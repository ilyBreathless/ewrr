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
    /// Interaction logic for ReceiveCom.xaml
    /// </summary>
    public partial class ReceiveCom : Window
    {
        public string[] sectorNumbers { get; set; }
        public event EventHandler<string> ComboBoxValueChanged;
        public event EventHandler<string> ComboBox2ValueChanged;
        public ReceiveCom()
        {
            InitializeComponent();
            sectorNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7"};
            DataContext = this;
        }

   

        private void combo24_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxValueChanged?.Invoke(this, combo24.SelectedValue.ToString());
        }

        private void combo58_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox2ValueChanged?.Invoke(this, combo58.SelectedValue.ToString());
        }

        private void freqApply_btn_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
