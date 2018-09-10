using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogApi.Models
{
    public class BlogPostModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string User { get; set; }
        public string Body { get; set; } = string.Empty;
        public string[] Tags { get; set; } = new string[0];
    }
}