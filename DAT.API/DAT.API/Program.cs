using DAT.API.Services;
using DAT.Common.Models.Configs;
using DAT.Database;
using DAT.Infrastructure;
using DAT.Realtime;
using Newtonsoft.Json;

try
{
    var allowCors = "_allowCors";
    var builder = WebApplication.CreateBuilder(args);
    var env = builder.Environment.EnvironmentName;
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .AddJsonFile($"logsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGenCustom();
    builder.Services.AddDbContext<EntityDBContext>();
    builder.Services.AddInfrastructures();
    builder.Services.AddCors(allowCors, configuration.GetSection("AppConfig"));
    builder.Services.AddScopedServices(ServiceAssembly.Assembly);
    builder.Services.AddScopedUnitOfWorkCore<EntityDBContext>(ServiceExtensions.OverWriteConnectString(configuration.GetSection("AppConfig")));
    builder.Services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
    builder.Services.AddLog();
    builder.Logging.AddConsole();
    builder.Services.AddSignalR();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.Urls.Add("http://localhost:1112");
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors(allowCors);
    app.UseDefaultFiles();

    app.UseStaticFiles();

    app
        .MapHub<NotificationHub>("/realtime-api")
        .RequireCors(allowCors);

    //app.UseMiddleware<TokenDecodedMiddleware>();
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(JsonConvert.SerializeObject(ex));
    Console.WriteLine("tutv19");
    throw;
}