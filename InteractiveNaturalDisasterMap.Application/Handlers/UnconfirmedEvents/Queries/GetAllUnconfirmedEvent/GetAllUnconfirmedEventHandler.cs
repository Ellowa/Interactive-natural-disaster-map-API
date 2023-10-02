using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetAllUnconfirmedEvent
{
    public class GetAllUnconfirmedEventHandler : IRequestHandler<GetAllUnconfirmedEventRequest, IList<UnconfirmedEventDto>>
    {
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task<IList<UnconfirmedEventDto>> Handle(GetAllUnconfirmedEventRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<UnconfirmedEvent, bool>> isCheckFilter =
                request.GetAllUnconfirmedEventDto.AddIsChecked == null ||
                request.GetAllUnconfirmedEventDto.AddIsChecked == false
                    ? ue => !ue.IsChecked
                    : ue => true;

            Expression<Func<UnconfirmedEvent, bool>> userLoginFilter = ue => true;
            if (!string.IsNullOrEmpty(request.GetAllUnconfirmedEventDto.UserLogin))
            {
                var user = (await _unitOfWork.UserRepository.GetAllAsync(cancellationToken,
                        mu => mu.Login == request.GetAllUnconfirmedEventDto.UserLogin))
                    .FirstOrDefault();
                if (user != null)
                    userLoginFilter = ue => ue.UserId == user.Id;
            }

            Expression<Func<UnconfirmedEvent, bool>> addedAtFilter =
                request.GetAllUnconfirmedEventDto.AddedAt == null
                    ? ue => true
                    : ue => ue.Event.StartDate >= request.GetAllUnconfirmedEventDto.AddedAt.Value.ToUniversalTime();

            Expression<Func<UnconfirmedEvent, bool>> filter = this.CombineWithAnd(isCheckFilter, userLoginFilter);
            filter = this.CombineWithAnd(filter, addedAtFilter);

            IEnumerable<UnconfirmedEvent> unconfirmedEvents = await _unconfirmedEventRepository.GetAllAsync(cancellationToken, filter,
                ue => ue.User.Role, 
                ue => ue.Event.Category,
                ue => ue.Event.Source,
                ue => ue.Event.MagnitudeUnit,
                ue => ue.Event.EventHazardUnit);

            unconfirmedEvents = request.GetAllUnconfirmedEventDto.SortOrder?.ToLower() == "asc" 
                ? unconfirmedEvents.OrderBy(GetSortProperty(request).Compile()) 
                : unconfirmedEvents.OrderByDescending(GetSortProperty(request).Compile());

            IList<UnconfirmedEventDto> unconfirmedEventDtos = new List<UnconfirmedEventDto>(); 
            foreach (var unconfirmedEvent in unconfirmedEvents)
            {
                unconfirmedEventDtos.Add(new UnconfirmedEventDto(unconfirmedEvent));
            }

            return unconfirmedEventDtos;
        }

        private static Expression<Func<UnconfirmedEvent, object>> GetSortProperty(GetAllUnconfirmedEventRequest request)
        {
            return request.GetAllUnconfirmedEventDto.SortColumn?.ToLower() switch
            {
                "hazard" => ue => ue.Event.EventHazardUnit.HazardName,
                "category" => ue => ue.Event.EventCategoryId,
                "user" => ue => ue.User.Login,
                _ => ue => ue.Event.StartDate
            };
        }

        private Expression<Func<T, bool>> CombineWithAnd<T>(Expression<Func<T, bool>> firstExpression, Expression<Func<T, bool>> secondExpression)
        {
            // Create a parameter to use for both of the expression bodies.
            var parameter = Expression.Parameter(typeof(T), "x");
            // Invoke each expression with the new parameter, and combine the expression bodies with AND.
            var resultBody = Expression.AndAlso(Expression.Invoke(firstExpression, parameter), Expression.Invoke(secondExpression, parameter));
            // Combine the parameter with the resulting expression body to create a new lambda expression.
            return Expression.Lambda<Func<T, bool>>(resultBody, parameter);
        }
    }
}
