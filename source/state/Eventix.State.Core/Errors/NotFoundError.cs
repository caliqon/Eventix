using FluentResults;

namespace Eventix.State.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
    }
}