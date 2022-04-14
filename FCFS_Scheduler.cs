using System.Collections.Generic;

static class FCFS_Scheduler {
    static int[] FCFS_Scheduler_Handler(int[] arrival_time, int[] burst_time) {
        SortedDictionary<int, int> Processes = new SortedDictionary<int, int>();
        for (int i = 0; i < arrival_time.Length; i++)
            Processes[arrival_time[i]] = burst_time[i];

        int c = 0;
        int[] result = new int[arrival_time.Length];
        foreach (var process in Processes)
            result[c++] = process.Value;

        return result;
    }
}
