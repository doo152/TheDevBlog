namespace TheDevBlog.API.Models.DTO
{
    public class AddPostRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

        public string UrlHandle { get; set; } = string.Empty;
        public string FeaturedImageUrl { get; set; } = string.Empty;
        public bool Visible { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
