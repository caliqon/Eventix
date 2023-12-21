using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Eventix.DependencyInjection.Contracts;

public interface IAppHostBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
    IWebHostEnvironment Environment { get; }
    ILogger Logger { get; }
}