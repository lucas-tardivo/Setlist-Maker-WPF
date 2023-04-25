using Microsoft.EntityFrameworkCore;
using SetlistMaker.Services.Domain.Sqlite.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SetlistMaker.Services.Domain.Sqlite.Repositories
{
    public partial class RepositoryBase
    {
        protected readonly EntityContext _context;

        public RepositoryBase(EntityContext context)
        {
            _context = context;
        }

        protected TransactionScope CreateTransactionScopeWithIsolationLevel(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            return new TransactionScope
            (
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = isolationLevel,
                    Timeout = timeout ?? new TimeSpan(0, 1, 30)
                }
            );
        }

        public void ResetContextState() => _context.ChangeTracker.Entries()
            .Where(e => e.Entity != null).ToList()
            .ForEach(e => e.State = EntityState.Detached);

        public async Task ExecuteSQLCommand(string sql, CancellationToken ct)
        {
            await _context.Database.ExecuteSqlRawAsync(sql, ct);
        }
    }
}
