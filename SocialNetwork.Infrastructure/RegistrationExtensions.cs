using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Repository;

namespace SocialNetwork.Infrastructure;

public static class RegistrationExtensions
{
    public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionStrings:LocalSqlServer"]);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
