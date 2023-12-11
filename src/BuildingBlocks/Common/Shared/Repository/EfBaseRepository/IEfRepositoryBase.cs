namespace Shared.Repository.EfBaseRepository
{
    using System.Linq.Expressions;

    using Shared.Entity;

    public interface IEfRepositoryBase<T> 
        where T : EntityBase
    {
        Task<Guid> AddAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IList<T>> ListAsync(Expression<Func<T, object>> orderBy, int page, int take, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
