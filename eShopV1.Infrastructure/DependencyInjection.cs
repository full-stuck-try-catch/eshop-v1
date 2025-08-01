﻿using Asp.Versioning;
using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts;
using eShopv1.Domain.Users;
using eShopV1.Application.Abstractions.Authentication;
using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Jwt;
using eShopV1.Application.Abstractions.Time;
using eShopV1.Application.Caching;
using eShopV1.Infrastructure.Authentication;
using eShopV1.Infrastructure.Authorization;
using eShopV1.Infrastructure.Caching;
using eShopV1.Infrastructure.Data;
using eShopV1.Infrastructure.Repositories;
using eShopV1.Infrastructure.Swaggers;
using eShopV1.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Text;

namespace eShopV1.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            AddCors(services);

            AddPersistence(services, configuration);

            AddCaching(services, configuration);

            AddAuthentication(services, configuration);

            AddAuthorization(services);

            AddSwagger(services, configuration);

            AddHealthChecks(services, configuration);

            AddApiVersioning(services);

            AddBackgroundJobs(services, configuration);

            return services;
        }

        private static void AddSwagger(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
          Enter 'Bearer' [space] and then your token.
          Example: 'bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",                  // <- All lowercase! ("bearer")
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new string[]{}
                    }
                });
             });
        }

        private static void AddCors(IServiceCollection services)
        {
            // Add CORS policy for client
            services.AddCors(options =>
            {
                options.AddPolicy("AllowClient",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database") ??
                                      throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
        }

        private static void AddApiVersioning(IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1);
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtSettings = configuration.GetSection("Jwt");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();

            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();
            //services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        }

        private static void AddCaching(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Cache") ??
                                      throw new ArgumentNullException(nameof(configuration));

            services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

            services.AddSingleton<ICacheService, CacheService>();
        }

        private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("Database")!)
                .AddRedis(configuration.GetConnectionString("Cache")!);
        }

        private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
        {

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        }
    }
}
