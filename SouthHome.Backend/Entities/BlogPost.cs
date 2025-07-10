using SouthHome.Backend.Common;
using SouthHome.Backend.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    /// <summary>
    /// Represents a blog post entity with metadata, content, and associated tags.
    /// </summary>
    /// <remarks>This class is mapped to the "blog_posts" table in the database and includes properties for
    /// identifying the author, categorizing the post, tracking its status, and storing its content and metadata. It
    /// also supports associating tags with the blog post.</remarks>
    [Table("blog_posts")]
    public class BlogPost : EntityBase
    {
        /// <summary>
        /// Gets the unique identifier for the user.
        /// </summary>
        [Required]
        [Column("user_id")]
        public long UserId { get; protected set; }

        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        [Column("category_id")]
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the edit type of the blog post, indicating whether it is in Markdown or plain text format.
        /// </summary>
        [Column("edit_type")]
        [Required]
        public EditType EditType { get; set; } = EditType.Text;

        /// <summary>
        /// Gets or sets the status of the blog post, indicating whether it is a draft, published, deleted, or hidden.
        /// </summary>
        [Column("status")]
        public PostStatus Status { get; set; } = PostStatus.Editing;

        /// <summary>
        /// Gets or sets the title of the blog post.
        /// </summary>
        [Column("title")]
        [MaxLength(64)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the blog post, which can be in Markdown or plain text format.
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tags of the blog post, providing a brief overview of its content.
        /// </summary>
        public ICollection<BlogPostTag> BlogPostTags { get; set; } = [];

#pragma warning disable CS8618
        protected BlogPost() { }
#pragma warning restore CS8618

        public static BlogPost CreateInstance(long userId, long categoryId, string title)
        {
            return new BlogPost
            {
                UserId = userId,
                CategoryId = categoryId,
                Title = title
            };
        }
    }
}
