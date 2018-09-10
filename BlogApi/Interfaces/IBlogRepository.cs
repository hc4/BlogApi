using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Interfaces
{
    public interface IBlogRepository
    {
        Task EnsureIndex();
        Task RemoveAllPosts();
        Task AddPost(BlogPostModel item);
        Task<IEnumerable<BlogPostModel>> FindPosts(BlogPostSearchModel options);
    }
}