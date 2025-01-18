using System.Reflection;
using System.Text.RegularExpressions;
using DAT.Common.Models.Configs;
using DAT.Core;
using DAT.Core.Implementations;
using DAT.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DAT.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddScopedInterfaceInstances<TInterface>(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(TInterface)))
                .ToArray();

            foreach (var classType in classTypes)
            {
                services.AddScoped(classType);
            }

            return services;
        }

        public static IServiceCollection AddScopedInterfaces(this IServiceCollection services, string interfaceSuffix, Assembly assembly)
        {
            var classTypes = assembly.GetTypes()
                .Where(t => t.IsClass && t.Name.EndsWith(interfaceSuffix))
                .ToArray();

            foreach (var classType in classTypes)
            {
                var interfaceType = classType.GetInterface("I" + classType.Name);
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, classType);
                }
            }

            return services;
        }

        public static IServiceCollection AddScopedServices(this IServiceCollection services, Assembly assembly)
        {
            return services
                .AddScopedInterfaces("Service", assembly);
        }

        public static IServiceCollection AddScopedRepositories(this IServiceCollection services, Assembly assembly)
        {
            return services
                .AddScopedInterfaces("Repository", assembly);
        }

        public static IServiceCollection AddScopedHelpers(this IServiceCollection services, Assembly assembly)
        {
            return services
                .AddScopedInterfaces("Helper", assembly);
        }

        public static IServiceCollection AddInfrastructures(this IServiceCollection services)
        {
            return services
                .AddScopedServices(ServiceHelperAssembly.Assembly)
                .AddScopedHelpers(ServiceHelperAssembly.Assembly);
        }

        public static IServiceCollection AddScopedUnitOfWorkCore<TContext>(this IServiceCollection services, string connectionString)
            where TContext : DbContext
        {
            return services
                .AddDbContext<TContext>(options => { options.UseSqlServer(connectionString); })
                .AddScoped<DbContext, TContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddCustomLogger(
            IServiceCollection services, AppConfig appConfig)
        {
            services.AddScoped(provider => provider.GetService<ILoggerFactory>().CreateLogger(appConfig.ServiceName));
            var serviceName = new Regex(@"\W+").Replace(appConfig.ServiceName, "");
            var logFormat = "shop-food" + serviceName + " {Level:u5} {Timestamp:yyyy-MM-dd HH:mm:ss} - {Message:j}{EscapedException}{NewLine}{NewLine}";
            var filePathFormat = $"{appConfig.Logging.OutputPath}{serviceName}" + "-{Date}.log";
            var retainedFileCountLimit = 10;

            var loggerConfig = new LoggerConfiguration();
            if (ApplicationMode.IsDevelopment)
            {
                loggerConfig = loggerConfig
                    .MinimumLevel.Is(LogEventLevel.Debug)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information);
            }
            else
            {
                loggerConfig = loggerConfig
                    .MinimumLevel.Is(LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
            }

            Log.Logger = loggerConfig
                .Enrich.With(new ExceptionEnricher())
                .WriteTo.RollingFile(filePathFormat
                    , outputTemplate: logFormat
                    , retainedFileCountLimit: retainedFileCountLimit
                    , shared: true
                )
                .CreateLogger();

            services.AddLogging();

            return services;
        }

        public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app, ILoggerFactory loggerFactory, bool logRequestInput = true)
        {
            loggerFactory.AddSerilog();

            if (logRequestInput)
            {
                app.UseMiddleware<RequestInputLoggingMiddleware>();
            }

            if (ApplicationMode.IsDevelopment)
            {
            }

            return app;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, AppConfig appConfig)
        {
            return services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(appConfig.ServiceVersion, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = appConfig.ServiceName,
                        Version = appConfig.ServiceVersion,
                    });

                    //options.IncludeXmlComments(Path.Combine(
                    //    PlatformServices.Default.Application.ApplicationBasePath,
                    //    "MT.Services.xml"));

                    var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", new string[] { }},
                    };

                    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    });
                });
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, AppConfig appConfig)
        {
            app.UseSwagger(option => { option.PreSerializeFilters.Add((swaggerDoc, httpReq) => { }); });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{appConfig.ServiceVersion}/swagger.json",
                    $"DaoAnhTu {appConfig.ServiceName} API V{appConfig.ServiceVersion}");
            });

            return app;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, AppConfig appConfig)
        {
            return services;
            //return services
            //    .AddAuthentication(AppConfig.IdentityServer.DefaultScheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = AppConfig.IdentityServer.Authority;
            //        options.RequireHttpsMetadata = false;

            //        options.ApiName = AppConfig.IdentityServer.ApiName;
            //        options.ApiSecret = AppConfig.IdentityServer.ApiSecret;

            //        options.EnableCaching = true;
            //        options.CacheDuration = TimeSpan.FromMinutes(10);
            //    });
        }

        public static string OverWriteConnectString(IOptions<AppConfig> options)
        {
            return string.Format("Server={0};Database={1};User Id={2};password={3};Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;", options.Value.ConnectionStringInfo?.IPAddress, options.Value.ConnectionStringInfo?.DBName, options.Value.ConnectionStringInfo?.UserId, options.Value.ConnectionStringInfo?.Password);
        }

        public static string OverWriteConnectString(IConfigurationSection section)
        {
            var dataConfig = section.GetSection("ConnectionStringInfo");
            return string.Format("Server={0};Database={1};User Id={2};password={3};Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;", dataConfig["IPAddress"], dataConfig["DBName"], dataConfig["UserId"], dataConfig["Password"]);
        }

        public static IServiceCollection AddSwaggerGenCustom(this IServiceCollection services)
        {
            services.AddSwaggerGen((options) =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;  // For development purposes, set it to false
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                    };
                });
            return services;
        }

        public static void AddLog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(new ConfigurationBuilder()
                            .AddJsonFile("logsettings.json")
                            .Build())
                        .CreateLogger();
        }

        public static IServiceCollection AddCors(this IServiceCollection services, string myAllowSpecificOrigins)
        {
            return services
                .AddCors(options =>
                {
                    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .WithOrigins("http://localhost:4200")
                          .WithOrigins("http://localhost:4300")
                          .WithOrigins("http://192.168.131.182")
                          .WithOrigins("http://192.168.131.182:80")
                          .WithOrigins("http://192.168.131.182:8083")
                          .WithOrigins("https://taphoagiadinh.com/")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
                });
        }
    }

    public class ExceptionEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception == null)
                return;

            var logEventProperty = propertyFactory.CreateProperty("EscapedException", logEvent.Exception.ToString().Replace("\r", "\\r").Replace("\n", "\\n"));
            logEvent.AddPropertyIfAbsent(logEventProperty);
        }
    }
}