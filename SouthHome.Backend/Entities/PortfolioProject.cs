using SouthHome.Backend.Common;
using SouthHome.Backend.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    /// <summary>
    /// The display  projects in the system.
    /// </summary>
    [Table("portfolio_projects")]
    public class PortfolioProject : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user who owns the project.
        /// </summary>
        [Column("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the edit type of the project, indicating whether it is in Markdown or plain text format.
        /// </summary>
        [Column("edit_type")]
        public EditType EditType { get; set; } = EditType.Text;

        /// <summary>
        /// Gets or sets the status of the project description, indicating whether it is a draft, published, deleted, or hidden.
        /// </summary>
        [Column("post_status")]
        public PostStatus PostStatus { get; set; } = PostStatus.Editing;

        /// <summary>
        /// Gets or sets the status of the project, indicating whether it is a draft, published, deleted, or hidden.
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start time of the project.
        /// </summary>
        [Column("start_at")]
        public DateTime StartAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the end time of the project.
        /// </summary>
        [Column("end_at")]
        public DateTime EndAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the URL associated with the project, which can be a link to the project's website or repository.
        /// </summary>
        [Column("url")]
        public string? Url { get; set; } = null;

        public List<ProtfolioProjectTag> Tags { get; set; } = [];

#pragma warning disable CS8618
        protected PortfolioProject() { }
#pragma warning restore CS8618

        public static PortfolioProject CreateInstance(long userId, string name)
        {
            return new PortfolioProject
            {
                UserId = userId,
                Name = name
            };
        }
    }
}
