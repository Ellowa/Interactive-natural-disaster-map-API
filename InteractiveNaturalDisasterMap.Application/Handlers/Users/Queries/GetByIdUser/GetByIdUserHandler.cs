using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByIdUser
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserRequest, UserDto>
    {
        private readonly IGenericBaseEntityRepository<User> _userRepository;

        public GetByIdUserHandler(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<UserDto> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.GetByIdUserDto.Id, cancellationToken, u => u.Role) 
                                ?? throw new NotFoundException(nameof(User), request.GetByIdUserDto.Id);

            return new UserDto(user);
        }
    }
}
