using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Interfaces
{
    public interface IRepositoryBase
    {
        void ResetContextState();
        Task ExecuteSQLCommand(string sql, CancellationToken ct);
    }
}
