using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class BlogEntryForCreationDto
    {
        public string Description { get; set; } = "The description of the blog post";
        public string Title { get; set; } = "The title of the blog post";
        public string Url { get; set; } = "http://blogpost.com";
        public IFormFile Image { get; set; }
    }
}