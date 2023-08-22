﻿using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class EventsCollectionRepository : BaseRepository<EventsCollection>, IEventsCollectionRepository
    {
        public EventsCollectionRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }

        public async Task<EventsCollection?> GetByEventIdAsync(int eventId,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<EventsCollection?> GetByUserIdAsync(int userId,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            query = query.Include(x => x.EventsCollectionInfo);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventsCollectionInfo.UserId == userId);
        }

        public async Task<EventsCollection?> GetByCollectionName(string collectionName,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            query = query.Include(x => x.EventsCollectionInfo);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventsCollectionInfo.CollectionName == collectionName);
        }
    }
}