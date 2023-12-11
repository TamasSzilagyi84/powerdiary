namespace Shared.Repository.EfBaseRepository
{
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using Shared.Entity;
    using Shared.Exceptions;

    public class EfRepositoryBase<T> : IEfRepositoryBase<T> 
        where T : EntityBase
    {
        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        public EfRepositoryBase(DbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._dbSet = context.Set<T>();
        }

        public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
        {
            EntityEntry<T> newEntity = await this._dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return newEntity.Entity.Id;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            T? entity = await this._dbSet.FindAsync(id, cancellationToken).ConfigureAwait(false);

            return entity ?? throw new EntityNotFoundException($"Could not find {typeof(T)} with id: {id}");
        }

        public async Task<IList<T>> ListAsync(
            Expression<Func<T, object>> orderBy, 
            int page, 
            int take, 
            CancellationToken cancellationToken)
        {
            return await this._dbSet
                .OrderBy(orderBy)
                .AsNoTracking()
                .Skip(page * take)
                .Take(take)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            T entity = await this.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

            this._dbSet.Remove(entity);

            await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
