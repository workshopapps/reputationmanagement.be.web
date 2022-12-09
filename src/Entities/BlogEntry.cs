using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class BlogEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = "The description of the blog post";
        [Required]
        public string Title { get; set; } = "The title of the blog post";

        public string? PathToImage { get; set; }
        [Required]
        public string Url { get; set; }
        public string Tag { get; set; } = "Reputation Management";
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
