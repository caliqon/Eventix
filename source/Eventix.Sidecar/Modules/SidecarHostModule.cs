using Eventix.DependencyInjection.Contracts;
using Eventix.DependencyInjection.Modules;
using FluentResults;

namespace Eventix.Sidecar;

public class SidecarHostModule() : HostModule(ModuleName)
{
    private const string ModuleName = "Sidecar";

    public override Result Register(IAppHostBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        return Result.Ok();
    }
}