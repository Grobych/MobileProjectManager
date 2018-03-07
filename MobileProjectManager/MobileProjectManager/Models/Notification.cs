using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProjectManager.Models
{
    public class Notification
    {

        [BsonId]
        public ObjectId ID { get; set; }

        public ObjectId To { get; set; }

        public ObjectId From { get; set; }

        public NotificationType Type { get; set; }

        public BsonDocument Line { get; set; }
    }
}
