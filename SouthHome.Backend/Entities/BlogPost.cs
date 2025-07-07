using SouthHome.Backend.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    [Table("blog_posts")]
    public class BlogPost
    {
        [Column("user_id")]
        public long UserId { get; }

        [Column("category_id")]
        public long CategoryId { get; set; }

        [Column("edit_type")]
        public EditType EditType { get; set; } = EditType.Text;

        [Column("status")]
        public PostStatus Status { get; set; } = PostStatus.Draft;

        [Column("title")]
        public string Title { get; set; }

        public ICollection<BlogPostTag> BlogPostTags = [];

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("timestamp")]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
