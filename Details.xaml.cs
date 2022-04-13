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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {

        public Details(int Number , string type)
        {
            InitializeComponent();
            try
            {
                RenderRows(Number, type);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RenderRows(int number, string type)
        {
            for(int i = 0; i < number; i++)
            {
                if (type == "FCFS" || type == "SJF Preemptive" || type == "SJF Non-Preemptive")
                {
                    for (int j = 0; j < ourGrid.ColumnDefinitions.Count-2; j++)
                    {


                        Border panel = new Border();
                        Grid.SetColumn(panel, j);
                        Grid.SetRow(panel, i);
                        // Text Box Properties
                        TextBox textBox = new TextBox();
                        if(j == 0)
                        {
                            textBox.Text = "P" + i;
                            textBox.IsReadOnly = true;
                        }
                        textBox.Width = 100;
                        textBox.TextAlignment = TextAlignment.Center;
                        textBox.HorizontalAlignment = HorizontalAlignment.Center;
                        textBox.VerticalAlignment = VerticalAlignment.Center;
                        panel.Child = textBox;
                        ourGrid.Children.Add(panel);

                    }
                }
                else if( type == "Priority Preemptive" || type == "Priority Non-Preemptive")
                {
                    for (int j = 0; j < ourGrid.ColumnDefinitions.Count-1; j++)
                    {

                        Border panel = new Border();
                        Grid.SetColumn(panel, j);
                        Grid.SetRow(panel, i);
                        // Text Box Properties
                        TextBox textBox = new TextBox();
                        if (j == 0)
                        {
                            textBox.Text = "P" + i;
                            textBox.IsReadOnly = true;
                        }
                        textBox.Width = 100;
                        textBox.TextAlignment = TextAlignment.Center;
                        textBox.HorizontalAlignment = HorizontalAlignment.Center;
                        textBox.VerticalAlignment = VerticalAlignment.Center;
                        panel.Child = textBox;
                        ourGrid.Children.Add(panel);

                    }

                }
                else
                // this is the case for Round Robin
                {
                    for (int j = 0; j < ourGrid.ColumnDefinitions.Count; j++)
                    {
                        if(j == 3)
                        {
                            continue;
                        }
                        Border panel = new Border();
                        Grid.SetColumn(panel, j);
                        Grid.SetRow(panel, i);
                        // Text Box Properties
                        TextBox textBox = new TextBox();
                        if (j == 0)
                        {
                            textBox.Text = "P" + i;
                            textBox.IsReadOnly = true;
                        }
                        textBox.Width = 100;
                        textBox.TextAlignment = TextAlignment.Center;
                        textBox.HorizontalAlignment = HorizontalAlignment.Center;
                        textBox.VerticalAlignment = VerticalAlignment.Center;
                        panel.Child = textBox;
                        ourGrid.Children.Add(panel);

                    }

                }

            }

        }

 
    }
}
