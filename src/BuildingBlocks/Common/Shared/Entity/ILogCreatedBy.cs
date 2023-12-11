namespace Shared.Entity
{
    public interface ILogCreatedBy
    {
        DateTime Created { get; }

        Guid CreatedById { get; }

        string CreatedByName { get; }
    }
}
