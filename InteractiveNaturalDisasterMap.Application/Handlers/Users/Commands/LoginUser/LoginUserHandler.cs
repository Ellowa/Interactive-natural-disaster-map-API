using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, string>
    {
        private readonly IGenericBaseEntityRepository<User> _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> filter = u => u.Login == request.LoginUserDto.Login;
            User user = (await _userRepository.GetAllAsync(cancellationToken, filter, u => u.Role)).FirstOrDefault()
                        ?? throw new NotFoundException(nameof(User), "With login " + request.LoginUserDto.Login);

            //Todo check pass

            string token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
