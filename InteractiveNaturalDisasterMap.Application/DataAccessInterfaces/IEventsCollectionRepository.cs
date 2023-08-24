﻿using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IEventsCollectionRepository : IBaseRepository<EventsCollection>
    {
        Task<IReadOnlyList<EventsCollection>> GetByEventIdAsync(int eventId,
            params Expression<Func<EventsCollection, object>>[] includes);

        Task<IReadOnlyList<EventsCollection>> GetByUserIdAsync(int userId,
            params Expression<Func<EventsCollection, object>>[] includes);

        Task<EventsCollection?> GetByCollectionName(string collectionName,
            params Expression<Func<EventsCollection, object>>[] includes);
    }
}
