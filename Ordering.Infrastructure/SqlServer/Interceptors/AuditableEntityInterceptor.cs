namespace Ordering.Infrastructure.SqlServer.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {
        const string user = "system";
        var now = DateTime.UtcNow;
        
        if (context == null)
            return;

        var entries = context.ChangeTracker.Entries<IEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.CreatedBy = user;
                entry.Entity.UpdatedAt = now;
                entry.Entity.UpdatedBy = user;
            }

            if (entry.State != EntityState.Modified && !entry.HasChangedOwnedEntities()) continue;
            
            entry.Entity.UpdatedAt = now;
            entry.Entity.UpdatedBy = user;
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
        =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                r.TargetEntry.State is EntityState.Modified or EntityState.Added);
}