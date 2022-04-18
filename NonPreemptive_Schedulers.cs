using System;
using System.Diagnostics;
using System.Collections.Generic;

public class NonPreemptive_Schedulers
{
    public List<JobsScheduler.outputProcesses> outputList = new();
    public float averageWaitingT;

    public NonPreemptive_Schedulers(string schedulerType, List<JobsScheduler.Process> readyProcesses)
    {
        Debug.Assert(schedulerType == "SJF" || schedulerType == "Priority");

        averageWaitingT = 0;

        execute(schedulerType, readyProcesses);
    }

    private void execute(string type, List<JobsScheduler.Process> processes)
    {
        bool job_flag, condition;
        int process_id = 0, p_num = processes.Count;
        float currentTime = 0, min, next_arrivalT;

        while (processes.Count > 0)
        {
            job_flag = false;
            min = float.MaxValue;
            next_arrivalT = float.MaxValue;

            for (int i = 0; i < processes.Count; i++)
            {
                condition = (type == "SJF") ? (processes[i].burstTime < min) : (processes[i].priority < min);
                if ((processes[i].arrivalTime <= currentTime) && condition)
                {
                    process_id = i;
                    min = (type == "SJF") ? processes[i].burstTime : (float)processes[i].priority;
                    job_flag = true;
                }
                else if ((!job_flag) && (processes[i].arrivalTime < next_arrivalT))
                {
                    //in case of idle state
                    process_id = i;
                    next_arrivalT = processes[i].arrivalTime;
                }
            }

            //assign the suitable attribute values to the scheduled process(if it exists) and add it to the outputList
            JobsScheduler.outputProcesses schedP = new ();
            if (job_flag)
            {
                schedP.processNumber = processes[process_id].processNumber;
                schedP.startTime = currentTime;
                schedP.usingTime = processes[process_id].burstTime;

                averageWaitingT += (currentTime - processes[process_id].arrivalTime);
                currentTime += processes[process_id].burstTime;

                //remove the scheduled process from the input list of processes
                processes.RemoveAt(process_id);
            }
            else
            {
                //in case of idle state
                schedP.processNumber = "idle";
                schedP.startTime = currentTime;
                schedP.usingTime = processes[process_id].arrivalTime - currentTime;

                currentTime = processes[process_id].arrivalTime;
            }
            outputList.Add(schedP);
        }
        averageWaitingT /= p_num;
    }
}