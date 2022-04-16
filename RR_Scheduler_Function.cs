using System;
using System.Collections.Generic;
using System.Linq;


public class Process
{
    public int arrival;
    public int index;
    public int burst_time;
    public int priority;

    public Process() { }

    public Process(int i, int value, int b)
    {
        index = i;
        arrival = value;
        burst_time = b;
    }

    public Process(int i, int value, int b, int p)
    {
        index = i;
        arrival = value;
        burst_time = b;
        priority = p;
    }

    public static void sort(List<Process> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            for (int j = 0; j < l.Count; j++)
            {
                if (l[i].arrival < l[j].arrival)
                {
                    Process temp = l[i];
                    l[i] = l[j];
                    l[j] = temp;
                }
            }
        }
    }
}


public class RR_Scheduler_Function
{
    public static int quantum;
    public static int prcs_no; //
    public static int[] arrivalTime; //
    public static int[] cpu_brustTime; //
    public static int[] prc; //
    public static int[] start;
    public static int[] end;
    public static int[] wait; //
    public static float avg_wait; //
    public static int last;
    public const int MAXX = 1000;


    public static void RR_Scheduler_Function(int[] a, int[] b, int q)
    {
        prcs_no = a.Length;
        arrivalTime = a;
        cpu_brustTime = b;
        prc = new int[MAXX];
        start = new int[MAXX];
        end = new int[MAXX];
        wait = new int[MAXX];
        quantum = q;

        for (int i = 0; i < MAXX; i++)
        {
            prc[i] = -1;
            start[i] = 0;
            end[i] = 0;
            wait[i] = 0;
        }

        List<Process> p = new List<Process>(prcs_no);
        for (int i = 0; i < prcs_no; i++)
            p.Add(new Process(i, arrivalTime[i], cpu_brustTime[i]));
        
        Process.sort(p);

        int clk = 0;
        int idx = 0;

        while (p.Count != 0)
        {
            List<Process> ready = new List<Process>(prcs_no);

            if (p.First().arrival > clk)
            {
                start[idx] = clk;
                end[idx] = p.First().arrival;
                prc[idx] = -1;
                idx++;
                clk = p.First().arrival;
            }

            for (int i = 0, j = 0; j < p.Count; i++)
            {
                if (p[j].arrival <= clk)
                {
                    ready.Add(p[j]);
                    p.RemoveAt(j);
                }
                else j++;
            }

            while (ready.Count != 0)
            {
                Process readyProcess = ready[0];
                ready.RemoveAt(0);

                if (quantum < readyProcess.burst_time)
                {
                    start[idx] = clk;
                    end[idx] = clk + quantum;
                    prc[idx] = readyProcess.index;
                    clk += quantum;
                    readyProcess.burst_time -= quantum;

                    for (int i = 0, j = 0; j < p.Count; i++)
                    {
                        if (p[j].arrival <= clk)
                        {
                            ready.Add(p[j]);
                            p.RemoveAt(j);
                        }
                        else j++;
                    }

                    ready.Add(readyProcess);
                    idx++;
                }
                else
                {
                    start[idx] = clk;
                    end[idx] = clk + readyProcess.burst_time;
                    prc[idx] = readyProcess.index;
                    clk += readyProcess.burst_time;

                    for (int i = 0, j = 0; j < p.Count; i++)
                    {
                        if (p[j].arrival <= clk)
                        {
                            ready.Add(p[j]);
                            p.RemoveAt(j);
                        }
                        else j++;
                    }

                    idx++;
                }
            }
        }

        for (int j = 0; j < prcs_no; j++)
        {
            bool k = false;
            int w = 0;
            for (int i = 0; i <= idx; i++)
            {
                if (prc[i] == j)
                {
                    if (!k)
                    {
                        wait[j] = start[i] - arrivalTime[j];
                        k = true;
                        w = end[i];
                    }
                    else if (k)
                    {
                        wait[j] += start[i] - w;
                        w = end[i];
                    }
                }
            }
        }

        avg_wait = 0.0f;
        for (int i = 0; i < prcs_no; i++)
            avg_wait += wait[i];

        avg_wait /= prcs_no;

        last = idx;

    }
}
