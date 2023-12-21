using Eventix.DependencyInjection.Extensions;
using Eventix.State;
using Eventix.State.Controllers;
using Eventix.State.Providers.CosmosDb;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddSingleton<ISystemClock>(new SystemClock());
builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .AddUserSecrets(typeof(Program).Assembly);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(StateController).Assembly);

builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddSerilog(Log.Logger);

builder.ConfigureModules(NullLogger.Instance, host =>
{
    host.AddModule<CosmosDbStateHostModule>()
        .AddModule<StateHostModule>();
});

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();