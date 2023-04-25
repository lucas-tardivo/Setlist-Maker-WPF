using SetlistMaker.Services.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Interfaces
{
    public partial interface IRepositoryEntityBase<TEntity> : IRepositoryBase where TEntity : Entity
    {
        TEntity GetById(Guid id, CancellationToken ct = default(CancellationToken));

        IList<TEntity> GetListById(Guid id, CancellationToken ct = default(CancellationToken));

        IQueryable<TEntity> GetByIdAsQueryable(Guid id, CancellationToken ct = default(CancellationToken));

        IQueryable<TEntity> GetAllAsQueryable(CancellationToken ct = default(CancellationToken));

        Task<TEntity> GetByIdAsync(Guid id, CancellationToken ct = default(CancellationToken));

        Task<IList<TEntity>> GetListByIdAsync(Guid id, CancellationToken ct = default(CancellationToken));

        IList<TEntity> GetByIds(IList<Guid> ids, CancellationToken ct = default(CancellationToken));

        IQueryable<TEntity> GetByIdsAsQueryable(IList<Guid> ids, CancellationToken ct = default(CancellationToken));

        Task<IList<TEntity>> GetByIdsAsync(IList<Guid> ids, CancellationToken ct = default(CancellationToken));

        void Insert(TEntity entity, CancellationToken ct = default(CancellationToken));

        Task InsertAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        void Insert(IList<TEntity> entities, CancellationToken ct = default(CancellationToken));

        Task InsertAsync(IList<TEntity> entities, CancellationToken ct = default(CancellationToken));

        void Update(TEntity entity, CancellationToken ct = default(CancellationToken));

        void Update(IList<TEntity> entities, CancellationToken ct = default(CancellationToken));

        void Delete(TEntity entity, CancellationToken ct = default(CancellationToken));

        void Delete(IList<TEntity> entities, CancellationToken ct = default(CancellationToken));

        void SaveChanges(CancellationToken ct = default(CancellationToken));

        Task SaveChangesAsync(CancellationToken ct = default(CancellationToken));

        TEntity GetByIdAsNoTracking(Guid id, CancellationToken ct = default(CancellationToken));

        Task LockRowAsync(Guid id, CancellationToken ct = default(CancellationToken));
    }
}
