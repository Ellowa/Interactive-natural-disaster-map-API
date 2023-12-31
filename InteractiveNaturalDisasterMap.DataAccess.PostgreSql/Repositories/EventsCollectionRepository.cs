﻿using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class EventsCollectionRepository : BaseRepository<EventsCollection>, IEventsCollectionRepository
    {
        public EventsCollectionRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }

        public async Task<EventsCollection?> GetByCollectionName(string collectionName,
            CancellationToken cancellationToken,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            query = query.Include(x => x.EventsCollectionInfo);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventsCollectionInfo.CollectionName == collectionName,
                cancellationToken);
        }
    }
}
