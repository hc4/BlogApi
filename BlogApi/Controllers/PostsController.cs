using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public PostsController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // GET api/posts/find
        [HttpPost("find")]
        public Task<IEnumerable<BlogPostModel>> Find([FromBody] BlogPostSearchModel options)
        {
            return _blogRepository.FindPosts(options);
        }

        // POST api/posts
        [HttpPost]
        [Authorize]
        public Task Post([FromBody] BlogPostCreateModel model)
        {
            var userId = User.Identity.Name;
            var postModel = new BlogPostModel
                {
                    User = userId,
                    Body = model.Body,
                    Tags = model.Tags
                };

            return _blogRepository.AddPost(postModel);
        }
    }
}
