using Microsoft.Extensions.DependencyInjection;
using SetlistMaker.Services.Domain.Interfaces;
using SetlistMaker.Services.Domain.Sqlite.Repositories;
using SetlistMaker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IBandService, BandService>();
        }

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IBandRepository, BandRepository>();
        }
    }
}
