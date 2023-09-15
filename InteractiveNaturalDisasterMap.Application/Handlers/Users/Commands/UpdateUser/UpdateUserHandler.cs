using System.Security.Cryptography;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Interfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<User> _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetByIdAsync(request.UpdateUserDto.Id, cancellationToken))
                       ?? throw new NotFoundException(nameof(User), request.UpdateUserDto.Id);

            await _authorizationService.AuthorizeAsync(request.UserId, user.Id, cancellationToken, user);

            byte[] passwordHash, passwordSalt;
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.UpdateUserDto.Password));
            }

            user.FirstName = request.UpdateUserDto.FirstName;
            user.SecondName = request.UpdateUserDto.SecondName;
            user.LastName = request.UpdateUserDto.LastName;
            user.Email = request.UpdateUserDto.Email;
            user.Telegram = request.UpdateUserDto.Telegram;
            user.Login = request.UpdateUserDto.Login;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Update(user);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
