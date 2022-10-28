using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Restaurant.Common.Exceptions.Responses;
using Restaurant.Common.IoC.Attributes;

namespace Restaurant.Common.Startup;

public static class ServiceCollectionExtensionMethods
{
    public static void AddControllersWithModelValidation(this IServiceCollection services,
        AuthorizeFilter? defaultAuthorizeFilter)
    {
        services.AddControllers(config =>
            {
                if (defaultAuthorizeFilter != null)
                {
                    config.Filters.Add(defaultAuthorizeFilter);
                }
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .Select(e => e.Errors
                            .Select(er => er.ErrorMessage))
                        .SelectMany(s => s)
                        .ToList();
                    throw new BadRequestException(errors.First(), errors);
                };
            });
    }

    public static void AddInjectableAsScoped(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var injectables = assemblies.SelectMany(a => a.GetExportedTypes()
            .Where(c =>
                c.IsClass &&
                !c.IsAbstract &&
                c.IsPublic &&
                c.IsDefined(typeof(InjectableAsScoped), true)
            )
        );
        foreach (var injectable in injectables)
        {
            services.AddScoped(injectable);
        }
    }

    public static void AddInjectableAsTransient(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var injectables = assemblies.SelectMany(a => a.GetExportedTypes()
            .Where(c =>
                c.IsClass &&
                !c.IsAbstract &&
                c.IsPublic &&
                c.IsDefined(typeof(InjectableAsTransient), true)
            )
        );
        foreach (var injectable in injectables)
        {
            services.AddTransient(injectable);
        }
    }

    public static void AddInjectableAsSingleton(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var injectables = assemblies.SelectMany(a => a.GetExportedTypes()
            .Where(c =>
                c.IsClass &&
                !c.IsAbstract &&
                c.IsPublic &&
                c.IsDefined(typeof(InjectableAsSingleton), true)
            )
        );
        foreach (var injectable in injectables)
        {
            services.AddSingleton(injectable);
        }
    }

    public static void AddSwaggerGenWithBearerAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", //Name the security scheme
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults
                                .AuthenticationScheme, //The name of the previously defined security scheme.
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var config = new MapperConfiguration(cfg =>
            cfg.AddMaps(assemblies)
        );
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }

    public static void AddCorsPolicy(this IServiceCollection services, string policyName)
    {
        services.AddCors(options => options.AddPolicy(name: policyName,
            policy  =>
            {
                policy.AllowCredentials();
                policy.WithHeaders("*");
                policy.WithMethods("*");
                policy.WithOrigins("https://localhost:8080");
            })
        );
    }
}
