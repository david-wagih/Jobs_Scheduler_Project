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

    public List<JobsScheduler.outputProcesses> outputList;
    public float averageWaitingTime;

    public FCFS_Scheduler(List<JobsScheduler.Process> processes)
    {
        outputList = new List<JobsScheduler.outputProcesses>();
        averageWaitingTime = 0;

        List<ProcessWrapper> OrderOfExecution = new List<ProcessWrapper>();
        foreach (var process in processes)
        {
            ProcessWrapper pw = new ProcessWrapper(process);
            OrderOfExecution.Add(pw);
        }

        OrderOfExecution.Sort();

        float acc = 0;
        foreach (var process in OrderOfExecution)
        {
            acc += process.burstTime;
            averageWaitingTime += acc;
        }
        averageWaitingTime -= acc;
        averageWaitingTime = (float)averageWaitingTime / OrderOfExecution.Count;

        int startingTime = 0;
        foreach (var process in OrderOfExecution)
        {
            JobsScheduler.outputProcesses op = new JobsScheduler.outputProcesses();
            op.processNumber = process.processNumber;
            op.startTime = startingTime;
            op.usingTime = process.burstTime;
            this.outputList.Add(op);
            startingTime += process.burstTime;
        }

    }
}
