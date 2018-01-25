using System;
using System.Collections.Generic;
using System.Text;

namespace STPMP.Entity
{
    class Project
    {
        private int ID;
        private string name;
        private string description;
        private string client;
        private DateTime start;
        private DateTime finish;
        private DateTime deadline;
        private bool status;
        private float price;
        private int teamID;
        private int taskListID;

        public int GetID { get => ID; set => ID = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Client { get => client; set => client = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime Finish { get => finish; set => finish = value; }
        public DateTime Deadline { get => deadline; set => deadline = value; }
        public bool Status { get => status; set => status = value; }
        public float Price { get => price; set => price = value; }
        public int TeamID { get => teamID; set => teamID = value; }
        public int TaskListID { get => taskListID; set => taskListID = value; }
    }
}
