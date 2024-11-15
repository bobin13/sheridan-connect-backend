using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sheridan_connect_backend.Models
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("profile_picture_url")]
        public string Profile_picture_url { get; set; }

        [BsonElement("interests")]
        public List<string> Interests { get; set; }

        [BsonElement("joined_communities")]
        public List<JoinedCommunity> JoinedCommunities { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }


    }

    public class JoinedCommunity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("community_id")]
        public string CommunityId { get; set; }

        [BsonElement("joined_at")]
        public DateTime JoinedAt { get; set; }

    }
}