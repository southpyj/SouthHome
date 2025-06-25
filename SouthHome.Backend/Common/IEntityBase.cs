namespace SouthHome.Backend.Common
{
    public interface IEntityBase
    {
        long Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime CreateAt { get; set; }

        DateTime UpdateAt { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
