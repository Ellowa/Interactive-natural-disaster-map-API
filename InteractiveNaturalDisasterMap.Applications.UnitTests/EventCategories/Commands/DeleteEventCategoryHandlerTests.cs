using System.Linq.Expressions;
using FluentAssertions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.DeleteEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Moq;

namespace InteractiveNaturalDisasterMap.Applications.UnitTests.EventCategories.Commands
{
    public class DeleteEventCategoryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        private Mock<IGenericBaseEntityRepository<EventCategory>> _eventCategoryRepositoryMock =
            new Mock<IGenericBaseEntityRepository<EventCategory>>();

        private Mock<INaturalDisasterEventRepository> _naturalDisasterEventRepositoryMock =
            new Mock<INaturalDisasterEventRepository>();

        [SetUp]
        protected void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _eventCategoryRepositoryMock = new Mock<IGenericBaseEntityRepository<EventCategory>>();
            _naturalDisasterEventRepositoryMock = new Mock<INaturalDisasterEventRepository>();
            
            _naturalDisasterEventRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<NaturalDisasterEvent, bool>>>()))
                .ReturnsAsync(new List<NaturalDisasterEvent>());
            _unitOfWorkMock.Setup(x => x.NaturalDisasterEventRepository).Returns(_naturalDisasterEventRepositoryMock.Object);
        }

        [Test]
        public async Task DeleteEventCategoryHandlerTests_WhenEventCategoryIsExistsAndDefaultEventCategoryIsExists_ShouldDeleteEventCategoryAndRebaseEventsToDefaultEventCategory()
        {
            // Arrange
            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto { Id = 1 },
            };

            var otherCategory = new EventCategory
            {
                CategoryName = EntityNamesByDefault.DefaultEventCategory
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.DeleteEventCategoryDto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new EventCategory());
            _eventCategoryRepositoryMock.Setup(x =>
                    x.GetAllAsync(It.IsAny<CancellationToken>(), ec => ec.CategoryName == EntityNamesByDefault.DefaultEventCategory))
                .ReturnsAsync(new List<EventCategory> { otherCategory });
            _eventCategoryRepositoryMock.Setup(x => x.DeleteByIdAsync(1, It.IsAny<CancellationToken>()));

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new DeleteEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _eventCategoryRepositoryMock.Verify(ecr => ecr.GetByIdAsync(request.DeleteEventCategoryDto.Id, It.IsAny<CancellationToken>()),
                Times.Once, "eventCategoryRepository GetByIdAsync() is failed");
            _eventCategoryRepositoryMock.Verify(
                ecr => ecr.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<EventCategory, bool>>>()), Times.Once,
                "eventCategoryRepository GetAllAsync() is failed");
            _eventCategoryRepositoryMock.Verify(ecr => ecr.DeleteByIdAsync(request.DeleteEventCategoryDto.Id, It.IsAny<CancellationToken>()),
                Times.Once, "eventCategoryRepository DeleteByIdAsync() is failed");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once, "unitOfWork SaveAsync() is failed");
        }

        [Test]
        public async Task DeleteEventCategoryHandlerTests_WhenEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto { Id = 1 },
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.DeleteEventCategoryDto.Id, It.IsAny<CancellationToken>()));

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new DeleteEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Entity - EventCategory({request.DeleteEventCategoryDto.Id}) not found");

            _eventCategoryRepositoryMock.Verify(
                ecr => ecr.DeleteByIdAsync(request.DeleteEventCategoryDto.Id,
                    It.IsAny<CancellationToken>()), Times.Never, "eventCategoryRepository DeleteByIdAsync() is calls");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never, "unitOfWork SaveAsync() is calls");
        }

        [Test]
        public async Task DeleteEventCategoryHandlerTests_WhenDefaultEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto { Id = 1 },
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.DeleteEventCategoryDto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new EventCategory());
            _eventCategoryRepositoryMock.Setup(x =>
                    x.GetAllAsync(It.IsAny<CancellationToken>(), ec => ec.CategoryName == EntityNamesByDefault.DefaultEventCategory))
                .ReturnsAsync(new List<EventCategory>());

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new DeleteEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Entity - EventCategory(With name {EntityNamesByDefault.DefaultEventCategory}) not found");

            _eventCategoryRepositoryMock.Verify(
                ecr => ecr.DeleteByIdAsync(request.DeleteEventCategoryDto.Id,
                    It.IsAny<CancellationToken>()), Times.Never, "eventCategoryRepository DeleteByIdAsync() is calls");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never, "unitOfWork SaveAsync() is calls");
        }
    }
}
