using SouthHome.Backend.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    /// <summary>
    /// Represents the owner of a site, including personal and contact information.
    /// </summary>
    /// <remarks>This class provides properties to store details about a site owner, such as their nickname,
    /// name, avatar,  birth date, phone number, and description. It is mapped to the "site_owner" table in the
    /// database.</remarks>
    [Table("site_owners")]
    public class SiteOwner : EntityBase
    {
        /// <summary>
        /// Gets or sets the nickname associated with the entity.
        /// </summary>
        [Required]
        [MaxLength(32)]
        [Column("nick_name")]
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the entity.
        /// </summary>
        [Required]
        [MaxLength(15)]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL or file path to the user's avatar image.
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Column("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the entity.
        /// </summary>
        [Column("birth")]
        public DateTime? Birth { get; set; }

        /// <summary>
        /// Gets or sets the phone number associated with the entity.
        /// </summary>
        [MaxLength(11)]
        [RegularExpression(@"^\d(11)$")]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description associated with the entity.
        /// </summary>
        [MaxLength(1024)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteOwner"/> class with the specified nickname, name, avatar,
        /// and birth date.
        /// </summary>
        /// <remarks>This constructor is protected and intended to be used by derived classes to
        /// initialize the properties of a <see cref="SiteOwner"/> instance.</remarks>
        /// <param name="nickName">The nickname of the site owner. Cannot be null or empty.</param>
        /// <param name="name">The full name of the site owner. Cannot be null or empty.</param>
        /// <param name="avatar">The URL or path to the avatar image of the site owner. Cannot be null or empty.</param>
        /// <param name="birth">The birth date of the site owner.</param>
        protected SiteOwner(string nickName, string name, string avatar)
        {
            this.NickName = nickName;
            this.Name = name;
            this.Avatar = avatar;
        }

        protected SiteOwner()
        {
            // Parameterless constructor for EF Core
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SiteOwner"/> class with the specified details.
        /// </summary>
        /// <param name="nickName">The nickname of the site owner. Cannot be null or empty.</param>
        /// <param name="name">The full name of the site owner. Cannot be null or empty.</param>
        /// <param name="avatar">The URL or path to the avatar image of the site owner. Cannot be null or empty.</param>
        /// <param name="birth">The birth date of the site owner.</param>
        /// <returns>A new <see cref="SiteOwner"/> instance initialized with the provided details.</returns>
        public static SiteOwner CreateInsctance(string nickName, string name, string avatar)
        {
            return new SiteOwner(nickName, name, avatar);
        }
    }
}
