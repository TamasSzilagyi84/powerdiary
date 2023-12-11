namespace Shared.Entity
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
        }

        public Guid Id { get; set; }
    }
}
