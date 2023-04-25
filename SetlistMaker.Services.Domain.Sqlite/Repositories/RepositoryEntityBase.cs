using Microsoft.EntityFrameworkCore;
using SetlistMaker.Services.Domain.Interfaces;
using SetlistMaker.Services.Domain.Models;
using SetlistMaker.Services.Domain.Sqlite.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Sqlite.Repositories
{
    public partial class RepositoryEntityBase<TEntity> : RepositoryBase, IRepositoryEntityBase<TEntity> where TEntity : Entity
    {
        protected DbSet<TEntity> _entity;
        protected readonly TimeSpan DefaultTimeout = new TimeSpan(0, 1, 30);

        public RepositoryEntityBase(EntityContext context) : base(context)
        {
            _entity = _context.Set<TEntity>();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public virtual TEntity GetById(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (id == Guid.Empty) return null;

            return _entity.FirstOrDefault(entity => entity.Id == id);
        }

        public virtual IList<TEntity> GetListById(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (id == Guid.Empty) return null;

            return _entity.Where(entity => entity.Id == id).ToList();
        }

        public virtual IQueryable<TEntity> GetByIdAsQueryable(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (id == Guid.Empty) return null;

            return _entity.Where(entity => entity.Id == id).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllAsQueryable(CancellationToken ct = default(CancellationToken))
        {
            return _entity.AsQueryable();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (id == Guid.Empty)
            {
                return default(TEntity);
            }

            return await _entity.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<IList<TEntity>> GetListByIdAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (id == Guid.Empty) return null;

            return await _entity.Where(entity => entity.Id == id).ToListAsync();
        }

        public virtual IList<TEntity> GetByIds(IList<Guid> ids, CancellationToken ct = default(CancellationToken))
        {
            if (!ids.Any()) return null;

            return _entity
                    .Where(entity => ids.Contains(entity.Id))
                    .ToList();
        }

        public IQueryable<TEntity> GetByIdsAsQueryable(IList<Guid> ids, CancellationToken ct = default(CancellationToken))
        {
            if (!ids.Any()) return null;

            return _entity
                    .Where(entity => ids.Contains(entity.Id));
        }

        public virtual async Task<IList<TEntity>> GetByIdsAsync(IList<Guid> ids, CancellationToken ct = default(CancellationToken))
        {
            if (!ids.Any()) return null;

            return await _entity
                   .Where(entity => ids.Contains(entity.Id))
                   .ToListAsync();
        }

        public virtual void Insert(TEntity entity, CancellationToken ct = default(CancellationToken))
        {
            _context.Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken ct = default(CancellationToken))
        {
            await _context.AddAsync(entity);
        }

        public virtual void Insert(IList<TEntity> entities, CancellationToken ct = default(CancellationToken))
        {
            _context.AddRange(entities);
        }

        public virtual async Task InsertAsync(IList<TEntity> entities, CancellationToken ct = default(CancellationToken))
        {
            await _context.AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity, CancellationToken ct = default(CancellationToken))
        {
            _context.Update(entity);
        }

        public virtual void Update(IList<TEntity> entities, CancellationToken ct = default(CancellationToken))
        {
            _context.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity, CancellationToken ct = default(CancellationToken))
        {
            _context.Remove(entity);
        }

        public virtual void Delete(IList<TEntity> entities, CancellationToken ct = default(CancellationToken))
        {
            _context.RemoveRange(entities);
        }

        public void SaveChanges(CancellationToken ct = default(CancellationToken))
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken ct = default(CancellationToken))
        {
            await _context.SaveChangesAsync();
        }

        public virtual TEntity GetByIdAsNoTracking(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (id == Guid.Empty)
            {
                return default(TEntity);
            }

            return _entity.AsNoTracking().FirstOrDefault(entity => entity.Id == id);
        }

        public async Task LockRowAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            var tableName = _context.Model.FindEntityType(typeof(TEntity)).GetTableName();
            await ExecuteSQLCommand(string.Format("SELECT * FROM {0} WHERE Id = '{1}' FOR UPDATE", tableName, id.ToString()), ct);
        }
    }
}
