using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

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
            Expression<Func<User, bool>> filter = u => u.Login == request.GetByLoginUserDto.Login;
            var user = (await _userRepository.GetAllAsync(cancellationToken, filter, u => u.Role))
                       .FirstOrDefault() ?? throw new NotFoundException(nameof(User), request.GetByLoginUserDto.Login);

            return new UserDto(user);
        }
    }
}
