using System;
using System.Collections.Generic;
using System.Text;

namespace STPMP.Entity
{
    class Project
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }

        public DateTime Deadline { get; set; }

        public bool Status { get; set; }

        public float Price { get; set; }

        public int TeamID { get; set; }

        public int TaskListID { get; set; }
    }
}
