using DreamStore.Core.AutoMappers.Categories;
using DreamStore.Core.AutoMappers.Products;
using DreamStore.Core.AutoMappers.Users;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Services;
using Microsoft.Extensions.DependencyInjection;


namespace DreamStore.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IProductService, ProductService>();
          
            services.AddMemoryCache();
            return services;

        }

        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperUserProfile));
            services.AddAutoMapper(typeof(AutoMapperCategoryProfile));
            services.AddAutoMapper(typeof(AutoMapperProductProfile));

            return services;
        }
     
    }
}
