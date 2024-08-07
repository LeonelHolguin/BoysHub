using Island.Core.Application.Interfaces.Repositories;
using Island.Infrastructure.Persistence.Contexts;
using Island.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region contexts
            var connectionString = config.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<ApplicationContext>(options =>
                                  options.UseSqlServer(connectionString,
                                    m =>
                                    {
                                        m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                                    }));
            #endregion

            #region repositories 
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IContainerRepository, ContainerRepository>();
            #endregion
        }
    }
}
