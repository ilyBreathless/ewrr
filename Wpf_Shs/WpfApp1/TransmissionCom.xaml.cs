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
    /// Interaction logic for TransmissionCom.xaml
    /// </summary>
    public partial class TransmissionCom : Window
    {
        public string[] sectorNumbers { get; set; }
        public event EventHandler<string> ComboBoxValueChanged_t;
        public event EventHandler<string> ComboBox2ValueChanged_t;
     
        public TransmissionCom()
        {
            InitializeComponent();
            sectorNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7" };
            DataContext = this;
        }

        private void combo58_trans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxValueChanged_t?.Invoke(this, combo24_trans.SelectedValue.ToString());
        }

        private void combo24_trans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox2ValueChanged_t?.Invoke(this, combo58_trans.SelectedValue.ToString());
        }
    }
}
