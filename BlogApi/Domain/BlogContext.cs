using BlogApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlogApi.Domain
{
    public class BlogContext
    {
        private readonly IMongoDatabase _db;

        public BlogContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _db = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<BlogPostModel> Posts => _db.GetCollection<BlogPostModel>("posts");
    }
}