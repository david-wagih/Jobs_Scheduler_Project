using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace JobsScheduler
{

    class Preemptive

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


        public static float getAvgWaiting(int iterator, float[] waiting_time)
        {
            float average_waiting = 0;
            for (int i = 0; i < iterator; i++)
            {
                average_waiting += waiting_time[i];
            }

            return average_waiting = average_waiting / iterator;
        }
        public static void assignUsingTime(float[] start_time, List<outputProcesses> output_processes)
        {
            for (int i = 0; i < output_processes.Count; i++)
            {
                output_processes[i].usingTime = start_time[i + 1] - start_time[i];
            }
        }


        public static void Preemptive_Scheduler(List<Process> processes, string type)
        {
            float[] start_time = new float[40];
            float[] remainingT = new float[processes.Count];
            float[] waiting_time = new float[processes.Count];
            int counter = 0;
            float time = 0;
            int proc_index = 0;
            float finish_time;
            int st_iter = 0;
            float min = float.MaxValue;
            bool job_flag = false;
            bool enter_flag = false;

            Arrival_sort(processes);

            for (int i = 0; i < processes.Count; i++)
                remainingT[i] = processes[i].burstTime;

            if (processes[0].arrivalTime > time)
            {
                start_time[st_iter++] = 0;
                output_processes.Add(new outputProcesses { processNumber = "idle", startTime = 0 });
            }

            while (counter < processes.Count)
            {

                enter_flag = false;
                for (int i = 0; i < processes.Count; i++)
                {
                    var condition = (type == "SJF Preemptive") ? (remainingT[i] < min) : (processes[i].priority < min);

                    if ((processes[i].arrivalTime <= time) && condition && (remainingT[i] > 0))
                    {

                        min = (type == "SJF Preemptive") ? remainingT[i] : (float)processes[i].priority;
                        proc_index = i;
                        job_flag = true;
                        enter_flag = true;

                    }

                }


                if (job_flag == false)
                {
                    time++;
                    continue;
                }


                if (enter_flag)
                {
                    start_time[st_iter++] = time;
                    output_processes.Add(new outputProcesses { processNumber = processes[proc_index].processNumber, startTime = time });
                }


                remainingT[proc_index]--;

                if (type == "SJF Preemptive")
                {
                    min = remainingT[proc_index];
                    if (min == 0)
                        min = int.MaxValue;
                }

                if (remainingT[proc_index] == 0)
                {

                    if (type == "Priority Preemptive")
                    {
                        processes[proc_index].priority = int.MaxValue;
                        min = int.MaxValue;
                    }

                    counter++;
                    job_flag = false;
                    finish_time = time + 1;
                    waiting_time[proc_index] = finish_time -
                                processes[proc_index].arrivalTime -
                                processes[proc_index].burstTime;

                    if (waiting_time[proc_index] < 0)
                        waiting_time[proc_index] = 0;
                }


                time++;
            }

            start_time[st_iter] = time;
            assignUsingTime(start_time, output_processes);
            avg_waiting = getAvgWaiting(processes.Count, waiting_time);


        }
    
    
}

