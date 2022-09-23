namespace TheDevBlog.API.Models.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; }= string.Empty;

        public string UrlHandle { get; set; } = string.Empty;
        public string FeaturedImageUrl  { get; set; }=string.Empty;
        public bool Visible { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
