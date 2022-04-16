using System;
using System.Collections.Generic;

public class ProcessWrapper : JobsScheduler.Process, IComparable<ProcessWrapper>
{
    public ProcessWrapper(JobsScheduler.Process p)
    {
        this.arrivalTime = p.arrivalTime;
        this.burstTime = p.burstTime;
    }

    int IComparable<ProcessWrapper>.CompareTo(ProcessWrapper other)
    {
        if (this.arrivalTime >= other.arrivalTime) return 1;
        else return -1;
    }
}

public class FCFS_Scheduler
{

    public List<ProcessWrapper> OrderOfExecution;
    public float averageWaitingTime;

    public FCFS_Scheduler(List<JobsScheduler.Process> processes)
    {
        OrderOfExecution = new List<ProcessWrapper>();
        foreach (var process in processes)
        {
            ProcessWrapper pw = new ProcessWrapper(process);
            OrderOfExecution.Add(pw);
        }

        OrderOfExecution.Sort();

        averageWaitingTime = 0;
        float acc = 0;
        foreach (var process in OrderOfExecution)
        {
            acc += process.burstTime;
            averageWaitingTime += acc;
        }
        averageWaitingTime -= acc;
        averageWaitingTime = (float)averageWaitingTime / OrderOfExecution.Count;
    }
}
