using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sheridan_connect_backend.Models
{
    public class Community
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("admins")]
        public List<ObjectId> Admins { get; set; }

        [BsonElement("members")]
        public List<ObjectId> Members { get; set; }

    }
}