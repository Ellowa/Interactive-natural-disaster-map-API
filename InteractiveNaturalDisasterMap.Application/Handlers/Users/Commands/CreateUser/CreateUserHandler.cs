using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<User> _userRepository;

        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<int> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.CreateUserDto.Password));
            }
            var userRole = (await _unitOfWork.UserRoleRepository.GetAllAsync(cancellationToken, ur => ur.RoleName == "user"))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(UserRole), "With roleName user");


            var entity = request.CreateUserDto.Map(passwordHash, passwordSalt, userRole.Id);
            await _userRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
