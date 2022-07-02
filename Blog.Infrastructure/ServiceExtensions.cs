using Blog.Contracts.Data;
using Blog.Infrastructure.Data;
using Blog.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlite<BlogDbContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            {
                options.MigrationsAssembly("Blog.Migrations");
            });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}

