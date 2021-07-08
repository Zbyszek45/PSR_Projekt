using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    class TimeManager
    {
        int[] times;
        internal void create_list(int v)
        {
            times = new int[v];
            for (int i = 0; i < times.Length; i++)
            {
                times[i] = 0;
            }
        }

        internal void add(int index, int time)
        {
            times[index] += time;
        }

        internal int get_best_time()
        {
            for (int i = 0; i < times.Length; i++)
            {
                Console.WriteLine(times[0] + " time\n");
            }
            return times.Max();
        }
    }
}
