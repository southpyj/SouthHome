using SouthHome.Backend.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    [Table("post_categories")]
    public class PostCategory : EntityBase
    {
        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description associated with the object.
        /// </summary>
        public string Description { get; set; }

#pragma warning disable CS8618
        protected PostCategory() { }
#pragma warning restore CS8618

        /// <summary>
        /// Creates a new instance of the <see cref="PostCategory"/> class with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the post category. Cannot be null or empty.</param>
        /// <param name="description">The description of the post category. Cannot be null or empty.</param>
        /// <returns>A new <see cref="PostCategory"/> instance initialized with the specified name and description.</returns>
        public static PostCategory Create(string name, string description)
        {
            return new PostCategory
            {
                Name = name,
                Description = description
            };
        }
    }
}
