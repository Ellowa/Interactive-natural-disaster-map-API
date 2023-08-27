using InteractiveNaturalDisasterMap.Domain.Entities;
using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface INaturalDisasterEventRepository : IGenericBaseEntityRepository<NaturalDisasterEvent>
    {
        Task<IReadOnlyList<NaturalDisasterEvent>> GetAllByCategoryAsync(int categoryId, CancellationToken cancellationToken, params Expression<Func<NaturalDisasterEvent, object>>[] includes);

        Task<IReadOnlyList<NaturalDisasterEvent>> GetAllByMagnitudeUnitAsync(int magnitudeUnitId, CancellationToken cancellationToken, params Expression<Func<NaturalDisasterEvent, object>>[] includes);

        Task<IReadOnlyList<NaturalDisasterEvent>> GetAllByHazardUnitAsync(int hazardUnitId, CancellationToken cancellationToken, params Expression<Func<NaturalDisasterEvent, object>>[] includes);
    }
}
