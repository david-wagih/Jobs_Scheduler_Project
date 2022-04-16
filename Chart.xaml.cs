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

namespace JobsScheduler
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : Window
    {
        public List<Rectangle> rectList = new List<Rectangle>();
        public Chart(List<outputProcesses> ourProcesses , float averageWaitingTime)
        {
            InitializeComponent();
            CreateChart(ourProcesses , averageWaitingTime);

        }

        public void CreateChart(List<outputProcesses> listOfProcesses , float averageWait)
        {
            double ctr =0;
            
            for(int i = 0; i < listOfProcesses.Count; i++)
            {
                StackPanel stackPanel = new StackPanel();
                outputProcesses process = listOfProcesses[i];
                Rectangle rectangle = new();
                rectangle.Width = process.usingTime * 10;
                rectangle.Fill = new SolidColorBrush(System.Windows.Media.Colors.Violet);
                rectangle.Stroke = new SolidColorBrush(System.Windows.Media.Colors.Black);
                rectangle.StrokeThickness = 1;
                rectangle.Height = 100;
                rectList.Add(rectangle);

                TextBlock ProcessName = new();
                ProcessName.HorizontalAlignment = HorizontalAlignment.Center;
                ProcessName.VerticalAlignment = VerticalAlignment.Center;
                ProcessName.FontSize = 15;
                ProcessName.Text = process.processNumber.ToString();

                TextBlock startTime = new();
                startTime.HorizontalAlignment = HorizontalAlignment.Left;
                startTime.VerticalAlignment = VerticalAlignment.Center;
                startTime.FontSize = 15;
                startTime.Text = process.startTime.ToString();

                TextBlock usingTime = new();
                usingTime.HorizontalAlignment = HorizontalAlignment.Center;
                usingTime.VerticalAlignment = VerticalAlignment.Center;
                usingTime.FontSize = 15;
                usingTime.Text = process.usingTime.ToString();

                TextBlock averageWaitingTimeBlock = new();
                averageWaitingTimeBlock.HorizontalAlignment = HorizontalAlignment.Center;
                averageWaitingTimeBlock.VerticalAlignment = VerticalAlignment.Center;
                averageWaitingTimeBlock.FontSize = 15;
                averageWaitingTimeBlock.Text = "Average Waiting time is: " + averageWait.ToString();

                stackPanel.Children.Add(usingTime);
                stackPanel.Children.Add(rectangle);
                stackPanel.Children.Add(startTime);
                stackPanel.Children.Add(ProcessName);
                chartGrid.Children.Add(stackPanel);
                chartGrid.Children.Add(averageWaitingTimeBlock);
                Canvas.SetTop(averageWaitingTimeBlock, 300);
                Canvas.SetLeft(averageWaitingTimeBlock, 10);

                Canvas.SetTop(stackPanel, 100);

                if (i == 0)
                {
                    Canvas.SetLeft(stackPanel, 10);

                }
                else
                {
                    ctr += rectList[i-1].Width;
                    Canvas.SetLeft(stackPanel, ctr);
                }
            }
        }
    }
}
