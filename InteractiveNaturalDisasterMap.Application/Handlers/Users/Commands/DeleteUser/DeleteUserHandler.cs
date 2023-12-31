﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<User> _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public DeleteUserHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetByIdAsync(request.DeleteUserDto.Id, cancellationToken))
                       ?? throw new NotFoundException(nameof(User), request.DeleteUserDto.Id);

            await _authorizationService.AuthorizeAsync(request.UserId, user.Id, cancellationToken, user, user.Id);

            //Delete all unconfirmed user events
            var unconfirmedEvents =
                await _unitOfWork.UnconfirmedEventRepository.GetAllAsync(cancellationToken, ue => ue.UserId == user.Id);
            foreach (var unconfirmedEvent in unconfirmedEvents)
            {
                await _unitOfWork.NaturalDisasterEventRepository.DeleteByIdAsync(unconfirmedEvent.EventId,
                    cancellationToken);
            }

            await _userRepository.DeleteByIdAsync(request.DeleteUserDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
