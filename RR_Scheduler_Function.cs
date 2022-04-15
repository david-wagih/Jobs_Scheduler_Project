using System;
using System.Collections.Generic;

public class RR_Scheduler_Function
{
    public static float avg_wait;

    public static Tuple<int, int>[] RR_Scheduler_Function(int[] arrival, int[] burst, int quantum)
    {
        int prcs_no = arrival.Length;
        int[] tmp_arrival = new int[prcs_no];
        int[] tmp_burst = new int[prcs_no];

        for (int i = 0; i < prcs_no; i++)
        {
            tmp_arrival[i] = arrival[i];
            tmp_burst[i] = burst[i];
        }

        int clk = 0;
        int it = 0;
        int[] wait = new int[prcs_no];
        Tuple<int, int>[] res = new Tuple<int, int>[100];

        while (true)
        {
            bool flag = true;
            for (int i = 0; i < prcs_no; i++)
            {
                if (tmp_arrival[i] <= clk)
                {
                    if (tmp_arrival[i] <= quantum)
                    {
                        if (tmp_burst[i] > 0)
                        {
                            flag = false;
                            if (tmp_burst[i] > quantum)
                            {

                                clk += quantum;
                                res[it++] = new Tuple<int, int>(i + 1, quantum);
                                tmp_burst[i] -= quantum;
                                tmp_arrival[i] += quantum;
                            }
                            else
                            {

                                clk += tmp_burst[i];

                                wait[i] = clk - burst[i] - arrival[i];

                                res[it++] = new Tuple<int, int>(i + 1, tmp_burst[i]);
                                tmp_burst[i] = 0;
                            }
                        }
                    }
                    else if (tmp_arrival[i] > quantum)
                    {
                        for (int j = 0; j < prcs_no; j++)
                        {
                            if (tmp_arrival[j] < tmp_arrival[i])
                            {
                                if (tmp_burst[j] > 0)
                                {
                                    flag = false;
                                    if (tmp_burst[j] > quantum)
                                    {
                                        clk += quantum;
                                        res[it++] = new Tuple<int, int>(j + 1, quantum);
                                        tmp_burst[j] -= quantum;
                                        tmp_arrival[j] += quantum;
                                    }
                                    else
                                    {
                                        clk += tmp_burst[j];
                                        wait[j] = clk - burst[j] - arrival[j];
                                        res[it++] = new Tuple<int, int>(j + 1, tmp_burst[j]);
                                        tmp_burst[j] = 0;
                                    }
                                }
                            }
                        }

                        if (tmp_burst[i] > 0)
                        {
                            flag = false;
                            if (tmp_burst[i] > quantum)
                            {
                                clk += quantum;
                                res[it++] = new Tuple<int, int>(i + 1, quantum);
                                tmp_burst[i] -= quantum;
                                tmp_arrival[i] += quantum;
                            }
                            else
                            {
                                clk += tmp_burst[i];
                                wait[i] = clk - burst[i] - arrival[i];
                                res[it++] = new Tuple<int, int>(i + 1, tmp_burst[i]);
                                tmp_burst[i] = 0;
                            }
                        }
                    }
                }

                else if (tmp_arrival[i] > clk)
                {
                    clk++;
                    i--;
                }
            }

            if (flag) break;
        }

        avg_wait = 0.0f;
        for (int i = 0; i < prcs_no; i++)
            avg_wait += wait[i];

        avg_wait /= prcs_no;

        return res;

    }
}
