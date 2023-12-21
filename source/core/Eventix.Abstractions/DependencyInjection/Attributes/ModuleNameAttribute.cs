namespace Eventix.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ModuleNameAttribute(string name) : Attribute
{
    public string Name => name;
}