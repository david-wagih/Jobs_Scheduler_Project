using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace JobsScheduler
{

    class SJF

    {
        public static void Arrival_sort(List<Process> processes)
        {
            Process[] temp = new Process[1];

            for (int i = 0; i < processes.Count; i++)
            {
                for (int n = i + 1; n < processes.Count - 1; n++)
                {
                    if (processes[n].arrivalTime < processes[i].arrivalTime)
                    {
                        temp[0] = processes[i];
                        processes[i] = processes[n];
                        processes[n] = temp[0];
                    }
                }
            }
        }


        public static Tuple<int, int>[] SJF_P(List<Process> processes,ref float avg_waiting)
        {
            Tuple<int, int>[] sched_Processes = new Tuple<int, int>[100];
            Tuple<int, int> wait;
            int[] start_time = new int[40];
            int[] remainingT = new int[processes.Count];
            int[] waiting_time = new int[processes.Count];
            int counter = 0;
            int time = 0;
            int shortest_index = 0, finish_time;
            int st_iter = 0;
            int it = 0;
            int min = int.MaxValue;
            bool job_flag = false;
            bool enter_flag = false;

            //set array values to class attributes
            /*for (int i = 0; i < p_num; i++)
            {
                processes[i] = new Process(i + 1, AT[i], BT[i]);
            }*/

            Arrival_sort(processes);

            for (int i = 0; i < processes.Count; i++)
                remainingT[i] = processes[i].burstTime;


            while (counter < processes.Count)
            {

                enter_flag = false;
                for (int i = 0; i < processes.Count; i++)
                {
                    if ((processes[i].arrivalTime <= time) && (remainingT[i] < min) && (remainingT[i] > 0))
                    {
                        shortest_index = i;
                        min = remainingT[i];
                        job_flag = true;
                        enter_flag = true;
                        start_time[st_iter++] = time;

                    }

                }


                if (job_flag == false)
                {
                    time++;
                    continue;
                }

                if (enter_flag)
                {
                    wait = new Tuple<int, int>(processes[shortest_index].processNumber, 0);
                    sched_Processes[it++] = wait;
                }

                remainingT[shortest_index]--;
                min = remainingT[shortest_index];
                if (min == 0)
                    min = int.MaxValue;

                if (remainingT[shortest_index] == 0)
                {


                    counter++;
                    job_flag = false;


                    finish_time = time + 1;



                    waiting_time[shortest_index] = finish_time -
                                processes[shortest_index].arrivalTime -
                                processes[shortest_index].burstTime;

                    if (waiting_time[shortest_index] < 0)
                        waiting_time[shortest_index] = 0;
                }


                time++;
            }

            start_time[st_iter] = time;
            int[] d_start_time = start_time.Distinct().ToArray();
            int it2 = 0;
            for (int i = 0; i < d_start_time.Length - 1; i++)
            {
                wait = new Tuple<int, int>(sched_Processes[it2].Item1, d_start_time[i + 1] - d_start_time[i]);
                sched_Processes[it2++] = wait;

            }

            for (int i = 0; i < processes.Count; i++)
            {
                avg_waiting = avg_waiting + waiting_time[i];
            }

            avg_waiting = avg_waiting / processes.Count;

            return sched_Processes;
        }


    }
}

