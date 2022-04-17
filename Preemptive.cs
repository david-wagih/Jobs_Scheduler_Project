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
    
     public static void Priority_P(List<Process> processes)
        {
            //Tuple<int, int>[] sched_Processes = new Tuple<int, int>[100];
            //Tuple<int, int> wait;
            int[] remainingT = new int[processes.Count];
            int[] waiting_time = new int[processes.Count];
            int counter = 0;
            float time = 0;
            int most_imp = 0;
            float finish_time;
            float[] start_time = new float[40];
            int st_iter = 0;
            float min = float.MaxValue;
            int it = 0;
            bool job_flag = false;
            bool enter_flag = false;
         

            /*for(int i=0;i<p_num;i++){
                processes[i]=new Process(i+1,AT[i],BT[i],P[i]);
            }*/
            Arrival_sort(processes);
            for (int i = 0; i < processes.Count; i++)
                remainingT[i] = processes[i].burstTime;




            while (counter < processes.Count)
            {

                enter_flag = false;
                for (int i = 0; i < processes.Count; i++)
                {
                    if ((processes[i].arrivalTime <= time) && (processes[i].priority < min) && (remainingT[i] > 0))
                    {
                        most_imp = i;
                        min = processes[i].priority;
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
                   
                    output_processes.Add(new outputProcesses{processNumber=processes[most_imp].processNumber});


                }

                remainingT[most_imp]--;

                if (remainingT[most_imp] == 0)
                {
                    processes[most_imp].priority = int.MaxValue;
                    min = int.MaxValue;
                    counter++;
                    job_flag = false;
                    finish_time = time + 1.0;
                    waiting_time[most_imp] = finish_time -
                                processes[most_imp].arrivalTime -
                                processes[most_imp].burstTime;

                    if (waiting_time[most_imp] < 0)
                        waiting_time[most_imp] = 0;
                }


                time++;
            }

            start_time[st_iter] = time;
            float[] d_start_time = start_time.Distinct().ToArray();
            int it2 = 0;
            for (int i = 0; i < d_start_time.Length - 1; i++)
            {
               output_processes[it2].usingTime = d_start_time[i + 1] - d_start_time[i];
               output_processes[it2].startTime = d_start_time[i];
               it2++;


            }


            for (int i = 0; i < processes.Count; i++)
            {
                avg_waiting = avg_waiting + waiting_time[i];
            }
            avg_waiting = avg_waiting / processes.Count;

        }

}

