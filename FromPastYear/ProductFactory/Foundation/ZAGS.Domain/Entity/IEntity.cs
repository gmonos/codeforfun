namespace Zags.Domain.Entity
{
    public interface IEntity
    {
        int Id { get; set; }

        bool IsNew { get; }

        bool IsDeleted { get; set; }
        //IExtension Extension { get; set; } 
    }
}
