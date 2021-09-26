using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SureSuccess.Shared
{
    public static class ApplicationDbContextFactory {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>();

            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseNpgsql(
                       configuration.GetConnectionString("PostgresConnection"), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));


            //register repository
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));


            return services;
        }
    }
   
}
