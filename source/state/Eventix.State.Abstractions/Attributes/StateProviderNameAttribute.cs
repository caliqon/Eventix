namespace Eventix.State.Attributes;

public class StateProviderNameAttribute(string name) : Attribute
{
    public string Name => name;
}