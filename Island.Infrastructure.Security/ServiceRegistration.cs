using Island.Core.Application.Interfaces.Authentication;
using Island.Infrastructure.Security.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Infrastructure.Security
{
    public static class ServiceRegistration
    {
        public static void AddSecutiryLayer(this IServiceCollection services, IConfiguration config)
        {
            #region authentication
            services.AddTransient<ITokenService>(provider => new TokenService(config));
            #endregion
        }
    }
}
