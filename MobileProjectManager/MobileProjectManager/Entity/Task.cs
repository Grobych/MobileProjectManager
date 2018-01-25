using System;
using System.Collections.Generic;
using System.Text;

namespace STPMP.Entity
{
    class Task
    {
        private int ID;
        private string name;
        private string description;
        private TaskStatus status;
        private float cost;
        private DateTime startTime;
        private DateTime deadline;
        private DateTime finishedTime;

        public int getID { get => ID; set => ID = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public float Cost { get => cost; set => cost = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime Deadline { get => deadline; set => deadline = value; }
        public DateTime FinishedTime { get => finishedTime; set => finishedTime = value; }
        internal TaskStatus Status { get => status; set => status = value; }
    }
}
