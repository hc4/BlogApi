using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Configuration
{
    public static class AuthOptions
    {
        private const string Secret = "long_secret_key_for_token";

        public const string Issuer = "MyBlogAuth";
        public const string Audience = "http://localhost:5000/";
        public static readonly TimeSpan LifeTime = TimeSpan.FromDays(1);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}