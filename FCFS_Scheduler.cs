using System;
using System.Collections.Generic;

public class ProcessWrapper : JobsScheduler.Process, IComparable<ProcessWrapper>
{
    public ProcessWrapper(JobsScheduler.Process p)
    {
        this.arrivalTime = p.arrivalTime;
        this.burstTime = p.burstTime;
        this.processNumber = p.processNumber;
    }

    int IComparable<ProcessWrapper>.CompareTo(ProcessWrapper other)
    {
        if (this.arrivalTime >= other.arrivalTime) return 1;
        else return -1;
    }
}

public class FCFS_Scheduler
{

    public List<JobsScheduler.outputProcesses> outputList  = new ();
    public float averageWaitingTime = 0;

    public FCFS_Scheduler(List<JobsScheduler.Process> processes)
    {

        List<ProcessWrapper> OrderOfExecution = new ();
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

        float startingTime = 0;
        foreach (var process in OrderOfExecution)
        {
            JobsScheduler.outputProcesses op = new ();
            op.processNumber = process.processNumber;
            op.startTime = startingTime;
            op.usingTime = process.burstTime;
            outputList.Add(op);
            startingTime += process.burstTime;
        }

    }
}