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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalculatorClassLibrary;

namespace CalculatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Evaluator evaluator;
        ResultBuilder resultBuilder;

        //For save, recall, and last buttons
        string tempSavedResult = "";
        string tempLastEquation = "";

        string savedResult = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Everytime the text gets updated run the ProofCheck method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputBox.Text != "")
            {
                try
                {
                    errorLabel.Content = "";
                    evaluator = new Evaluator(inputBox.Text);
                    //starts the evaluation process, ends when an equal sign is found
                    evaluator.Parse();
                    if (evaluator.IsComplete == true)
                    {
                        //the calculation succeeded
                        //save the equation and result, in case the user wants them later.
                        tempLastEquation = inputBox.Text;
                        tempSavedResult = evaluator.Value.ToString();

                        resultBuilder.BuildOutput(tempLastEquation, tempSavedResult);
                        resultsBox.Text = resultBuilder.Output;
                        RefreshUI();
                    }
                }
                catch (Exception ex)
                {
                    errorLabel.Content = ErrorHandler.InnermostExceptionMessager(ex);
                }
            }
        }

        void RefreshUI()
        {
            errorLabel.Content = "";
            inputBox.Text = "";
        }

        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "0";

        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "1";

        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "2";

        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "3";

        }

        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "4";

        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "5";

        }

        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "6";

        }

        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "7";

        }

        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "8";

        }

        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "9";

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshUI();
            inputBox.Focus();

        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "=";

        }

        private void LeftParenthesesButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "(";

        }

        private void RightParenthesesButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += ")";

        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "*";

        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "/";

        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "-";

        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "+";

        }

        private void CaretButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text += "^";

        }

        private void PeriodButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText(".");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inputBox.Focus();
            resultBuilder = new ResultBuilder();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputBox.Text.Length > 0)
            {
                inputBox.Text = inputBox.Text.Remove(inputBox.Text.Length-1);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            savedResult = tempSavedResult;
        }

        private void recallButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText(savedResult);
        }

        private void lastButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempLastEquation.Length > 0)
            {
                inputBox.Text = tempLastEquation.Remove(tempLastEquation.Length - 1);
            }
        }

        private void constantsButton_Click(object sender, RoutedEventArgs e)
        {
            ConstantsWindow dialog = new ConstantsWindow();
            if (dialog.ShowDialog() == true)
            {
                inputBox.AppendText(dialog.Constant);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                inputBox.AppendText("=");
            }
            else if (e.Key == Key.Back)
            {
                if (inputBox.Text.Length > 0)
                {
                    inputBox.Text = inputBox.Text.Remove(inputBox.Text.Length - 1);
                }
            }
        }
    }
}
