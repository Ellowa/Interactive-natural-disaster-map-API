using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Application.Interfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByIdUser
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserRequest, UserDto>
    {
        private readonly IGenericBaseEntityRepository<User> _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public GetByIdUserHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<UserDto> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.GetByIdUserDto.Id, cancellationToken, u => u.Role)
                       ?? throw new NotFoundException(nameof(User), request.GetByIdUserDto.Id);

            await _authorizationService.AuthorizeAsync(request.UserId, user.Id, cancellationToken, user);

            return new UserDto(user);
        }
    }
}
