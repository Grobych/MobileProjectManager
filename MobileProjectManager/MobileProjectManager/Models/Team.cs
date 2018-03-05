using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MobileProjectManager.Models
{
    class Team
    {
        [BsonId]
        ObjectId Id { get; set; }

        string Name { get; set; }

        ObjectId ManagerID { get; set; }

        Collection<ObjectId> WorkersId { get; set; }
    }
}
