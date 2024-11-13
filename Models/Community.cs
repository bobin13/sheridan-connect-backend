using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sheridan_connect_backend.Models
{
    public class Member
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
    }
    public class Community
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }


        [BsonElement("admins")]
        public string Admins { get; set; }

        [BsonElement("members")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Members { get; set; }

    }


}