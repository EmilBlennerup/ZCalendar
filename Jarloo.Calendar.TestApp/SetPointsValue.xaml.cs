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

namespace Jarloo.Calendar.TestApp
{
    /// <summary>
    /// Interaction logic for SetPointsValue.xaml
    /// </summary>
    public partial class SetPointsValue : Window
    {
        public SetPointsValue()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var salary = int.Parse(tbSalary.Text);
                var hours = double.Parse(tbHours.Text);
                if (salary != 0 && hours != 0)
                {
                    double pointsVal = salary / hours;
                    tbCalculated.Text = pointsVal.ToString();
                    btnSetValue.IsEnabled = true;
                }
    
            }
            catch(Exception ex){}
        } 
    }
}
