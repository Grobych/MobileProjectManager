using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProjectManager.Models
{
    public class Task
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Cost { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime FinishedTime { get; set; }

        internal TaskStatus Status { get; set; }

        public ObjectId Implementer { get; set; }

        public ObjectId ProjectID { get; set; }
    }
}
