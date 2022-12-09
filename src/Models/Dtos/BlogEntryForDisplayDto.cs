﻿namespace src.Models.Dtos
{
    public class BlogEntryForDisplayDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = "The description of the blog post";
        public string Title { get; set; } = "The title of the blog post";
        public string PathToImage { get; set; } = "C://SomePath/VeryUniqueInFact.jpg";
        public string Url { get; set; } = "http://blogpost.com";
        public string Tag { get; set; } = "Reputation Management";
    }
}