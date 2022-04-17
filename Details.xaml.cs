using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace JobsScheduler
{

    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        public List<Process> myProcesses = new();
        public List<outputProcesses > outputProcesses = new(); // this list is for the output to be drawn on the chart
        public int processesNumber;
        public string schedulerType;
        public int quantumValue;
        public float avgWait;


        public Details(int Number, string type , int? quantumTime)
        {
            processesNumber = Number;
            schedulerType = type;
            quantumValue = (int)quantumTime;
            InitializeComponent();
            try
            {
                ourGrid.ItemsSource = new List<Process>();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetChart_Click(object sender, RoutedEventArgs e)
        {
            foreach (var process in ourGrid.Items.OfType<Process>())
            {
                myProcesses.Add(new Process()
                {
                    processNumber = process.processNumber,
                    arrivalTime = process.arrivalTime,
                    burstTime = process.burstTime,
                    priority = process.priority,
                });
                // here myProcesses variable contains the User Input data for each Process and this list should be used in each algo according to Scheduler type
                if(schedulerType == "FCFS")
                {
                    FCFS_Scheduler s1 = new FCFS_Scheduler(myProcesses);
                    outputProcesses = s1.outputList;
                    avgWait = s1.averageWaitingTime;


                }else if(schedulerType == "SJF Preemptive"){

                }else if(schedulerType == "SJF Non-Preemptive")
                {

                }else if (schedulerType == "Priority Non-Preemptive") { 
                }
                else if (schedulerType == "Priority Preemptive")
                {
                }
                else
                {
                    // call the method responsible for doing the logic
                    RR_Scheduler_Function.RRSchedulerFunction(myProcesses , quantumValue);
                    outputProcesses = RR_Scheduler_Function.outputList;
                    avgWait = RR_Scheduler_Function.avg_wait;

                    // Round Robin Case
                }
            }

            // here we need to write the Drawing logic for the Gantt Chart
            var newForm = new Chart(outputProcesses , avgWait);
            newForm.Show();
        }
    }
}

