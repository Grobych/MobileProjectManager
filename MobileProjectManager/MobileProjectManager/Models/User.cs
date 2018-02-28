using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MobileProjectManager.Models
{
    public class User
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public bool IsOnline { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public byte[] Img { get; set; }

    }
}
