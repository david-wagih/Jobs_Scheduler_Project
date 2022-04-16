using System;
using System.Collections.Generic;
using System.Linq;


class Proc
{
    public int index;
    public float arrival;
    public float burst_time;

    public Proc(int i, float value, float b)
    {
        index = i;
        arrival = value;
        burst_time = b;
    }
}


public class RR_Scheduler_Function
{
	public static int quantum;
	public static int prcs_no;
	public static float[] arrival_time;
	public static float[] cpu_brustTime;
	public static int[] prc;
	public static float[] start;
	public static float[] end;
	public static float[] wait;
	public static int last;
	public const int MAXX = 1000;

	public static List<JobsScheduler.outputProcesses> outputList;
	public static float avg_wait;

	public static void RR_Scheduler_Function(List<JobsScheduler.Process> processes, int q)
	{
		prcs_no = processes.Count;
		arrival_time = new float[MAXX];
		cpu_brustTime = new float[MAXX];

		for (int i = 0; i < prcs_no; i++)
		{
			arrival_time[i] = processes[i].arrivalTime;
			cpu_brustTime[i] = processes[i].burstTime;
		}

		prc = new int[MAXX];
		start = new float[MAXX];
		end = new float[MAXX];
		wait = new float[MAXX];
		quantum = q;

		for (int i = 0; i < MAXX; i++)
		{
			prc[i] = -1;
			start[i] = 0;
			end[i] = 0;
			wait[i] = 0;
		}

		List<Proc> p = new List<Proc>(prcs_no);

		for (int i = 0; i < prcs_no; i++)
		{
			int x = (int)Char.GetNumericValue((processes[i].processNumber)[1]);
			p.Add(new Proc(x, arrival_time[i], cpu_brustTime[i]));
		}

		sort(p);

		float clk = 0.0f;
		int idx = 0;

		while (p.Count != 0)
		{
			List<Proc> ready = new List<Proc>(prcs_no);

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
				Proc readyProcess = ready[0];
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
			float w = 0.0f;

			for (int i = 0; i <= idx; i++)
			{
				if ((prc[i] - 1) == j)
				{
					if (!k)
					{
						wait[j] = start[i] - arrival_time[j];
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

		outputList = new List<JobsScheduler.outputProcesses>();

		for (int i = 0; i < last; i++)
		{
			JobsScheduler.outputProcesses op = new JobsScheduler.outputProcesses();
			if (prc[i] == -1) op.processNumber = "idle";
			else op.processNumber = "P" + prc[i];
			op.startTime = start[i];
			op.usingTime = end[i] - start[i];
			outputList.Add(op);
		}

	}

	static void sort(List<Proc> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            for (int j = 0; j < l.Count; j++)
            {
                if (l[i].arrival < l[j].arrival)
                {
                    Proc temp = l[i];
                    l[i] = l[j];
                    l[j] = temp;
                }
            }
        }
    }
}
