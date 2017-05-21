namespace Zags.Domain
{
    public interface IEntity
    {
        int Id { get; set; }

        IExtension Extension { get; set; } 
    }
}
