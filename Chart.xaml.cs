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
        public Chart(List<Process> ourProcesses)
        {
            InitializeComponent();
            CreateChart(ourProcesses);

        }

        public void CreateChart(List<Process> listOfProcesses)
        {
            double ctr =0;
            
            for(int i = 0; i < listOfProcesses.Count; i++)
            {
                StackPanel stackPanel = new StackPanel();
                Process process = listOfProcesses[i];
                Rectangle rectangle = new();
                rectangle.Width = process.burstTime * 10;
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
                //startTime.Text = process.arrivalTime.ToString();

                TextBlock usingTime = new();
                usingTime.HorizontalAlignment = HorizontalAlignment.Center;
                usingTime.VerticalAlignment = VerticalAlignment.Center;
                usingTime.FontSize = 15;
                usingTime.Text = (process.burstTime).ToString();

                stackPanel.Children.Add(usingTime);
                stackPanel.Children.Add(rectangle);
                stackPanel.Children.Add(startTime);
                stackPanel.Children.Add(ProcessName);
                chartGrid.Children.Add(stackPanel);
                Canvas.SetTop(stackPanel, 100);

                if (i == 0)
                {
                    Canvas.SetLeft(stackPanel, 0);

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
