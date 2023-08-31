using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class NaturalDisasterEventRepository : GenericBaseEntityRepository<NaturalDisasterEvent>, INaturalDisasterEventRepository
    {
        public NaturalDisasterEventRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }
    }
}
