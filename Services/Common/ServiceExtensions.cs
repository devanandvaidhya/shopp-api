using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructureServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {

            commonRepository.EmpConnection = configuration.GetConnectionString("EmpConnection");

        }

    }
}
