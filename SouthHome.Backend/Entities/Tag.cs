using SouthHome.Backend.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    [Table("tags")]
    public class Tag : EntityBase
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        [Required]
        public string Name { get; set; }

        protected Tag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="Tag"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the tag. Must not be null, empty, or consist only of whitespace.</param>
        /// <returns>A new <see cref="Tag"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is null, empty, or consists only of whitespace.</exception>
        public static Tag CreateInstance(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name cannot be null or empty.", nameof(name));
            return new Tag
            {
                Name = name
            };
        }
    }
}
