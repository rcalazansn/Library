﻿using Library.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Library.Infrastructure.Interceptor
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync
        (
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default
        )
        {
            foreach (var entry in eventData.Context?.ChangeTracker.Entries() ?? Enumerable.Empty<EntityEntry>())
            {
                if (entry is { State: EntityState.Deleted, Entity: ISoftDelete entity })
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;

                    // State
                    entry.State = EntityState.Modified;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
