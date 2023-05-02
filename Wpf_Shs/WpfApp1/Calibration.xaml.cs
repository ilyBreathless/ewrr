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
    /// Interaction logic for Calibration.xaml
    /// </summary>
    /// 


    public partial class Calibration : Window
    {
        public static bool flag = true;
      

        public Calibration()
        {
            InitializeComponent();
           
        }

    
       

        private void Calibration_Unloaded(object sender, RoutedEventArgs e)
        {
          
            if (!MainWindow.isMainClose)
            {
                ((Grozaz1)System.Windows.Application.Current.MainWindow).intelian_Button.BorderThickness = new Thickness(0);
            } else
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).intelian_Button.BorderThickness = new Thickness(0);
               
            }
        
           
        }
    }
}
