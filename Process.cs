using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsScheduler
{
    public class Process
    {
        public string processNumber { get; set; }
        public float arrivalTime { get; set; }
        public float burstTime { get; set; }
        public Int32? priority { get; set; }
    }
}
