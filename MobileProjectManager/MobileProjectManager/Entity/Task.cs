using System;
using System.Collections.Generic;
using System.Text;

namespace STPMP.Entity
{
    class Task
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Cost { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime FinishedTime { get; set; }

        internal TaskStatus Status { get; set; }
    }
}
