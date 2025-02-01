using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tenancy
{
    public static class Startup
    {
        public static IServiceCollection AddMultiTenancyServices(this IServiceCollection services, IConfiguration config)
        {
            return services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }).AddMultiTenant<ABCSchoolTenantInfo>().WithHeaderStrategy("tenant").WithClaimStrategy("tenant")
            .WithEFCoreStore<TenantDbContext, ABCSchoolTenantInfo>().Services;
        }
    }
}
