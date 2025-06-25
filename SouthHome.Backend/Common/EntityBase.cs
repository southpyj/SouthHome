using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SouthHome.Backend.Common
{
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; } = Util.GenerateUniqueGuid();  // id

        [Column("create_at")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;  // 数据的添加时间

        [Column("update_at")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;  // 数据的更新时间

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false; // 是否被删除

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; } = null;  // 删除日期
    }
}
