namespace BlogApi.Models
{
    public class BlogPostCreateModel
    {
        public string[] Tags { get; set; }
        public string Body { get; set; }
    }
}