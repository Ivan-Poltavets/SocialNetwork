using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Mappings;
using SocialNetwork.Infrastructure.Repository;
using System.Reflection;

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
        services.AddScoped<IFileRepository, FileRepository>();
    }

    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddTransient<ICustomMapper, CustomMapper>();
    }
}
