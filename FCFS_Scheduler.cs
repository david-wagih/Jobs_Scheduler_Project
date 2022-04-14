using System;
using System.Collections.Generic;

public class ProcessWrapper : JobsScheduler.Process, IComparable<ProcessWrapper>
{
    int IComparable<ProcessWrapper>.CompareTo(ProcessWrapper other)
    {
        if (this.arrivalTime >= other.arrivalTime) return -1;
        else return 1;
    }
}

public class FCFS_Scheduler
{

    List<ProcessWrapper> OrderOfExecution;
    float averageWaitingTime;

    FCFS_Scheduler(List<JobsScheduler.Process> processes)
    {
        OrderOfExecution = new List<ProcessWrapper>();
        foreach (var process in processes)
        {
            OrderOfExecution.Add((ProcessWrapper)process);
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
