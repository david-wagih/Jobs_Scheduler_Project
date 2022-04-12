using System;
using System.Collections.Generic;

public class RR_Scheduler_Function
{
    public static Tuple<int, int>[] RR_Scheduler_Function(int[] burst_time, int time_slice)
    {
        int ctr = 0;
        int it = 0;
        Tuple<int, int>[] result = new Tuple<int, int>[100];

        for (int i = 0; ctr < burst_time.Length; i = (i + 1) % burst_time.Length)
        {
            if (burst_time[i] <= 0) continue;

            else
            {
                Tuple<int, int> t = new Tuple<int, int>(i + 1, Math.Min(burst_time[i], time_slice));
                result[it++] = t;

                burst_time[i] -= time_slice;
            }

            if (burst_time[i] <= 0) ctr++;
        }

        return result;

    }
}
