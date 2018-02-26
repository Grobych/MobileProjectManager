using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;

namespace MobileProjectManager.Models
{
    public class User
    {
        public bool IsOnline { get; set; }

        public ObjectId ID { get; set; }

        public string Name { get; set; }

        public byte[] Img { get; set; }

        public User()
        {
            IsOnline = false;
            Name = "Ivan";
        }
    }
}
