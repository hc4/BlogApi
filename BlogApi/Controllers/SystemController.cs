using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApi.Domain;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public SystemController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // api/system/token
        [HttpGet("token")]
        [AllowAnonymous]
        public string GetToken([FromQuery(Name = "user")] string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId),
            };
            
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: claims,
                expires: now.Add(AuthOptions.LifeTime),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
        
        // api/system/init
        [HttpGet("init")]
        public async Task<string> Init()
        {
            await _blogRepository.RemoveAllPosts();

            await _blogRepository.EnsureIndex();

            await _blogRepository.AddPost(new BlogPostModel
            {
                Body = "Test Post 1",
                User = "1"
            });

            await _blogRepository.AddPost(new BlogPostModel
            {
                Body = "Test Post 2",
                User = "1"
            });

            await _blogRepository.AddPost(new BlogPostModel
            {
                Body = "Test Post 3",
                User = "2"
            });

            await _blogRepository.AddPost(new BlogPostModel
            {
                Body = "Test Post 4",
                User = "2"
            });

            return "Done";
        }
    }
}