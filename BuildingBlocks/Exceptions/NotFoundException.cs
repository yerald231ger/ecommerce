namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message)
    {
    }

    protected NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}