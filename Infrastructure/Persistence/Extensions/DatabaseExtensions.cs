
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Extensions;
public static class DatabaseExtensions
{
    public static IServiceCollection AddFileRateContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<FileRateContext>(options =>
        {
            options.UseSqlServer(connectionString,
                assembly => assembly.MigrationsAssembly(typeof(FileRateContext).Assembly.FullName));
        });
        return services;
    }
}
