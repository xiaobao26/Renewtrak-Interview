namespace Part_B.Domain.Exceptions;

public class NotFoundException : Exception
{
    public string Resource { get; }
    public string? Identifier { get; }

    public NotFoundException(string resource, string? identifier = null)
        : base(identifier is null ? $"{resource} not found." : $"{resource} '{identifier}' not found.")
    {
        Resource = resource;
        Identifier = identifier;
    }
}