using SouthHome.Backend.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    [Table("blog_post_tags")]
    public class BlogPostTag : EntityBase
    {
        /// <summary>
        /// Gets the unique identifier for the blog post.
        /// </summary>
        [Column("blog_post_id")]
        public long BlogPostId { get; protected set; }

        /// <summary>
        /// Gets the unique identifier for the tag.
        /// </summary>
        [Column("tag_id")]
        public long TagId { get; protected set; }

        [Column("tag_name")]
        public string TagName { get; protected set; }

        protected BlogPostTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="BlogPostTag"/> class with the specified blog post ID and tag ID.
        /// </summary>
        /// <param name="blogPostId">The unique identifier of the blog post associated with the tag.</param>
        /// <param name="tagId">The unique identifier of the tag to associate with the blog post.</param>
        /// <returns>A new <see cref="BlogPostTag"/> instance initialized with the specified blog post ID and tag ID.</returns>
        public static BlogPostTag Create(long blogPostId, Tag tag)
        {
            return new BlogPostTag
            {
                BlogPostId = blogPostId,
                TagId = tag.Id,
                TagName = tag.Name
            };
        }
    }
}
