using SouthHome.Backend.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthHome.Backend.Entities
{
    [Table("protfolio_project_tags")]
    public class ProtfolioProjectTag : EntityBase
    {
        [Column("portfolio_project_id")]
        public long PortfolioProjectId { get; protected set; }

        [Column("tag_id")]
        public long TagId { get; protected set; }

        [Column("tag_name")]
        public string TagName { get; protected set; }

#pragma warning disable CS8618
        protected ProtfolioProjectTag() { }
#pragma warning restore CS8618

        public static ProtfolioProjectTag CreateInstance(long projectId, Tag tag)
        {
            return new ProtfolioProjectTag
            {
                PortfolioProjectId = projectId,
                TagId = tag.Id,
                TagName = tag.Name
            };
        }
    }
}
