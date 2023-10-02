using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetAllUser
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, IList<UserDto>>
    {
        private readonly IGenericBaseEntityRepository<User> _userRepository;

        public GetAllUserHandler(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<IList<UserDto>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var users = (await _userRepository.GetAllAsync(cancellationToken, null, u=> u.Role)).OrderBy(u => u.Id);
            IList<UserDto> userDtos = new List<UserDto>(); 
            foreach (var user in users)
            {
                userDtos.Add(new UserDto(user));
            }

            return userDtos;
        }
    }
}
