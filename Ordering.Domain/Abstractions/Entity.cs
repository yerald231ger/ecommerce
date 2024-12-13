namespace Ordering.Domain.Abstractions;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public required TKey Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}