using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<User> _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public CreateUserHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<string> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.CreateUserDto.Password));
            }
            var userRole = (await _unitOfWork.UserRoleRepository.GetAllAsync(cancellationToken, ur => ur.RoleName == "user"))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(UserRole), "With roleName user");


            var user = request.CreateUserDto.Map(passwordHash, passwordSalt, userRole.Id);
            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            string token = _jwtProvider.Generate(user);
            return token;
        }
    }
}
