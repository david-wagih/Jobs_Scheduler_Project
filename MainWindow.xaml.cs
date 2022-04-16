using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace JobsScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int noOfProcesses = 0;
        public string schedulerType = null;
        public int? quantumTime;
        public MainWindow()
        {

            InitializeComponent();
        }


        


        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int NumberOfProcesses = Int32.Parse(ProcessesNo_Input.Text);
                if (NumberOfProcesses < 1 || NumberOfProcesses > 5)
                {
                    errorMessage.Text = "please enter numbers only between 1 and 5";


                }
                // want to check if the user has chosen something from the combo box
                else if (Combo_Value.Text == "")
                {
                    errorMessage.Text = "please choose one from the above Schedulers";
                }
                else
                {
                    // need to navigate to new window after updating the public variable of no of processes
                    noOfProcesses = NumberOfProcesses;
                    schedulerType = Combo_Value.Text;
                    quantumTime = Int32.Parse(quantumValue.Text);
                    var newForm = new Details(noOfProcesses, schedulerType , quantumTime); //create your new form.
                    newForm.Show(); //show the new form.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            
        }

        // this to ensure that user can't type any string in the text Box or any number greater than 5
        private void ProcessesNo_Input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String value = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            if (value == "Round Robin")
            {
                quantumBlock.Visibility = Visibility.Visible;
                quantumValue.Visibility = Visibility.Visible;
            }
        }
    }
}
