using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByLoginUser
{
    public class GetByLoginUserHandler : IRequestHandler<GetByLoginUserRequest, UserDto>
    {
        private readonly IGenericBaseEntityRepository<User> _userRepository;

        public GetByLoginUserHandler(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<UserDto> Handle(GetByLoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetAllAsync(cancellationToken, u => u.Role))
                       .FirstOrDefault(u => u.Login == request.GetByLoginUserDto.Login)
                       ?? throw new NotFoundException(nameof(User), request.GetByLoginUserDto.Login);

            return new UserDto(user);
        }
    }
}
