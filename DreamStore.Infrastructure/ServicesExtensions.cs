using DreamStore.Core.Entites;
using DreamStore.Core.Interfaces;
using DreamStore.Infrastructure.Context;
using DreamStore.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DreamStore.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddAppDbContext(this IServiceCollection services, string connString)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connString);

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
       
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        }

    }
}
