using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SetlistMaker.Services.Domain.Models;
using SetlistMaker.Services.Domain.Sqlite.Config;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Sqlite.Contexts
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        public static DbConnectionStringBuilder DefaultConnectionStringBuilder =>
            new SqliteConnectionStringBuilder(@"Data Source=database.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = DefaultConnectionStringBuilder.ToString();

            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityConfigTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityConfig).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x)
                .ToList();

            var entityConfigInstances = entityConfigTypes.Select(entityConfig => (IEntityConfig)Activator.CreateInstance(entityConfig));

            foreach (IEntityConfig entityConfig in entityConfigInstances)
            {
                entityConfig.ApplyConfiguration(modelBuilder);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
