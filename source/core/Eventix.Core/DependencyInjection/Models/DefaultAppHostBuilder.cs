using Eventix.DependencyInjection.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Eventix.DependencyInjection.Models;

public class DefaultAppHostBuilder : IAppHostBuilder
{
    public DefaultAppHostBuilder(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment, ILogger logger)
    {
        Services = services;
        Configuration = configuration;
        Environment = environment;
        Logger = logger;
    }

    public DefaultAppHostBuilder(WebApplicationBuilder builder, ILogger logger) : this(builder.Services, builder.Configuration, builder.Environment, logger)
    {
        
    }
    
    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public ILogger Logger { get; }
    public string EnvironmentName => Environment.EnvironmentName;
}