using System.Security.Cryptography;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<User> _userRepository;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var role = (await _userRepository.GetByIdAsync(request.UpdateUserDto.Id, cancellationToken))
                       ?? throw new NotFoundException(nameof(User), request.UpdateUserDto.Id);

            byte[] passwordHash, passwordSalt;
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.UpdateUserDto.Password));
            }

            _userRepository.Update(request.UpdateUserDto.Map(passwordHash, passwordSalt, role.Id));
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
