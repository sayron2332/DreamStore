using DreamStore.Core.Validation.Attribute;
using DreamStore.Core.Validation.Category;
using DreamStore.Core.Validation.Product;
using DreamStore.Core.Validation.User;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace DreamStore.Api
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAutoFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(CreateUpdateProductValidation).Assembly);
            return services;
        }
        public static IServiceCollection LowerCaseRoutes(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            return services;
        }
        public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"]!)),
                    ValidateIssuerSigningKey = true
                };
            });
            return services;
        }
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Dream Store Api", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                           }
               });
            });

            return services;
        }

        
    }
}
