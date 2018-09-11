using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Configuration;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlogApi.Domain
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(IOptions<MongoSettings> settings)
        {
            _context = new BlogContext(settings);
        }

        public async Task EnsureIndex()
        {
            await _context.Posts.Indexes.DropAllAsync();

            var index = Builders<BlogPostModel>.IndexKeys.Text(x => x.Body);
            var indexModel = new CreateIndexModel<BlogPostModel>(index);
            await _context.Posts.Indexes.CreateOneAsync(indexModel);
        }

        public Task RemoveAllPosts()
        {
            return _context.Posts.DeleteManyAsync(FilterDefinition<BlogPostModel>.Empty);
        }

        public Task AddPost(BlogPostModel item)
        {
            return _context.Posts.InsertOneAsync(item);
        }

        public async Task<IEnumerable<BlogPostModel>> FindPosts(BlogPostSearchModel options)
        {
            var filters = new List<FilterDefinition<BlogPostModel>>();

            if (!string.IsNullOrEmpty(options.User))
            {
                filters.Add(Builders<BlogPostModel>.Filter.Where(x => x.User == options.User));
            }

            if (!string.IsNullOrEmpty(options.Tag))
            {
                filters.Add(Builders<BlogPostModel>.Filter.Where(x => x.Tags.Contains(options.Tag)));
            }

            if (!string.IsNullOrEmpty(options.Text))
            {
                filters.Add(Builders<BlogPostModel>.Filter.Text(options.Text));
            }

            var filter = filters.Count > 0
                ? Builders<BlogPostModel>.Filter.And(filters)
                : FilterDefinition<BlogPostModel>.Empty;

            return await _context.Posts
                .Find(filter)
                .ToListAsync();
        }
    }
}