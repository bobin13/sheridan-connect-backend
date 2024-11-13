using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sheridan_connect_backend.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("post_user_id")]
        public string PostUserId { get; set; }

        [BsonElement("post_image_url")]
        public string PostImageUrl { get; set; }

        [BsonElement("title")]
        public string title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("like_count")]
        public int LikeCount { get; set; }

        [BsonElement("comments")]
        public List<string> Comments { get; set; }

        [BsonElement("likes")]
        public List<string> Likes { get; set; }

        [BsonElement("time_posted")]
        public DateTime TimePosted { get; set; }
    }
}