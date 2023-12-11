namespace Shared.Entity
{
    public interface ILogUpdatedBy
    {
        DateTime? Updated { get; set; }

        Guid? UpdatedById { get; set; }

        string? UpdatedByName { get; set; }
    }
}
