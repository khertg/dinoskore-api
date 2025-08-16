namespace DinoSkore.Api.Entities;

public class Competition : AuditableEntity
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
