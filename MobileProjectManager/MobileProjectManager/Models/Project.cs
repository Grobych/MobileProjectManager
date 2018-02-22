using System;

namespace MobileProjectManager.Models
{
    public class Project : ICloneable
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }

        public DateTime Deadline { get; set; }

        public bool Status { get; set; }

        public float Price { get; set; }

        //public int TeamID { get; set; }

        //public int TaskListID { get; set; }

       public Project()
        {
            ID = new Random().Next();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
