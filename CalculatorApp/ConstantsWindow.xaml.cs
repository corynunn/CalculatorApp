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

namespace CalculatorApp
{
    /// <summary>
    /// Interaction logic for ConstantsWindow.xaml
    /// </summary>
    public partial class ConstantsWindow : Window
    {
        //To add to the inputBox in the main window.
        public string Constant { get; set; }

        public ConstantsWindow()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void piButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "π";
            DialogResult = true;
        }

        private void eButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "e";
            DialogResult = true;
        }

        private void square2Button_Click(object sender, RoutedEventArgs e)
        {
            Constant = "(1.41421)";
            DialogResult = true;
        }

        private void alphaButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "α";
            DialogResult = true;
        }

        private void deltaButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "δ";
            DialogResult = true;
        }

        private void zetaButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "ζ";
            DialogResult = true;
        }

        private void phiButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "φ";
            DialogResult = true;
        }

        private void gammaButton_Click(object sender, RoutedEventArgs e)
        {
            Constant = "γ";
            DialogResult = true;
        }
    }
}
