using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MobileProjectManager.Models
{
    public class Team
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public string Name { get; set; }

        public ObjectId ManagerID { get; set; }

        public List<ObjectId> WorkersID { get; set; }

        public Team(string Name, ObjectId managerId)
        {
            this.Name = Name;
            this.ManagerID = managerId;
            WorkersID = new List<ObjectId>();
        }
    }
}
