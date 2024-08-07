using Island.Core.Application.Interfaces.Services;
using Island.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IContainerService, ContainerService>();
            #endregion
        }
    }
}
