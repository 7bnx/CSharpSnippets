using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CSharpSnippets.EFCore.EFInterceptors;
internal class UpdateRemovableInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync
  (
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    if (eventData is not null)
    {
      DateTime now = DateTime.UtcNow;
      var removable = eventData.Context?.ChangeTracker.Entries<IRemovable>().ToList()!;
      foreach (var r in removable)
      {
        if (r.State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
          r.Property(x => x.CreatedAt).CurrentValue = now;
          r.Property(x => x.WillRemoveAt).CurrentValue = now + TimeSpan.FromDays(100);
        }
        else if (r.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
          r.Property(x => x.WillRemoveAt).CurrentValue = now + TimeSpan.FromDays(100);
        }
      }
    }
    return base.SavingChangesAsync(eventData!, result, cancellationToken);
  }
}