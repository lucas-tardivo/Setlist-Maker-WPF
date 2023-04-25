using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SetlistMaker.Services.Configuration;
using SetlistMaker.Services.Domain.Sqlite.Contexts;
using SetlistMaker.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Setlist_Maker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlite("Data Source = database.db");
            });

            services.AddSingleton<BandWindow>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoadingWindow>();
            services.ConfigureService();
            services.ConfigureRepository();
            ConfigureAutoMapper(services);

            serviceProvider = services.BuildServiceProvider();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var loadingWindow = serviceProvider.GetService<LoadingWindow>();
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mappingConfig.CompileMappings();

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
