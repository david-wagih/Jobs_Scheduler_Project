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
        public static float avg_waiting = 0;
        public static List<outputProcesses> output_processes = new List<outputProcesses>();

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


        public static void SJF_P(List<Process> processes)
        {
            float[] start_time = new float[40];
            float[] remainingT = new float[processes.Count];
            float[] waiting_time = new float[processes.Count];
            int counter = 0;
            float time = 0;
            int shortest_index = 0;
            float finish_time;
            int st_iter = 0;
            float min = float.MaxValue;
            bool job_flag = false;
            bool enter_flag = false;
            bool idle_flag = false;

            Arrival_sort(processes);

            for (int i = 0; i < processes.Count; i++)
                remainingT[i] = processes[i].burstTime;

            if (processes[0].arrivalTime > time)
            {
                idle_flag = true;
            }

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

                    }

                }


                if (job_flag == false)
                {
                    time++;
                    continue;
                }

                if (idle_flag)
                {
                    start_time[st_iter++] = 0;
                    output_processes.Add(new outputProcesses { processNumber = "idle", startTime = 0 });
                    idle_flag = false;

                }
                if (enter_flag && !idle_flag)
                {
                    start_time[st_iter++] = time;
                    output_processes.Add(new outputProcesses { processNumber = processes[shortest_index].processNumber, startTime = time });
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
            for (int i = 0; i < output_processes.Count; i++)
            {
                output_processes[i].usingTime = start_time[i + 1] - start_time[i];
            }

            for (int i = 0; i < processes.Count; i++)
            {
                avg_waiting = avg_waiting + waiting_time[i];
            }

            avg_waiting = avg_waiting / processes.Count;

        }
    }

}

