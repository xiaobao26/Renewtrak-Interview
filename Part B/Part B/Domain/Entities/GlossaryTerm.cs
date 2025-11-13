namespace Part_B.Domain.Entities;

public class GlossaryTerm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Term { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
}