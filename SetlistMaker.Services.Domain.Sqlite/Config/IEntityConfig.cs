using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Sqlite.Config
{
    public interface IEntityConfig
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
