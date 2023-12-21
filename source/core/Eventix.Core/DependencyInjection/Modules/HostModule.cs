using Eventix.DependencyInjection.Contracts;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Eventix.DependencyInjection.Modules;

public abstract class HostModule(string name) : IHostModule
{
    public string Name => name;
    protected IAppHostBuilder Builder { get; private set; } = null!;
    protected IServiceCollection Services => Builder.Services;
    protected IConfiguration Configuration => Builder.Configuration;
    protected IWebHostEnvironment Environment => Builder.Environment;
    protected ILogger Logger => Builder.Logger;
    
    public Result Register(IAppHostBuilder builder)
    {
        Builder = builder;
        
        return Register();
    }
    
    protected abstract Result Register();
    
    protected IConfigurationSection GetModuleSection(string sectionName)
    {
        return Configuration.GetSection($"{Name}:{sectionName}");
    }
    
    protected Result BindConfiguration<T>(string sectionName, out T configuration)
    {
        configuration = default!;
        
        var section = GetModuleSection(sectionName);
        
        if (section.Exists())
        {
            configuration = section.Get<T>();
        }
        else
        {
            Logger.LogWarning($"No configuration found for {Name}:{sectionName}");
        }
        
        return Result.Ok();
    }
    
    protected Result ConfigureOptions<T>(string sectionName, Action<BinderOptions>? configure = null, Action<T>? postConfigure = null)
        where T : class
    {
        var section = GetModuleSection(sectionName);
        
        if (section.Exists())
        {
            Services.Configure<T>(section, configure);
            
            if (postConfigure is not null) 
                Services.PostConfigure(postConfigure);
            
            return Result.Ok();
        }

        Logger.LogWarning($"No configuration found for {Name}:{sectionName}");

        return Result.Ok();
    }
}