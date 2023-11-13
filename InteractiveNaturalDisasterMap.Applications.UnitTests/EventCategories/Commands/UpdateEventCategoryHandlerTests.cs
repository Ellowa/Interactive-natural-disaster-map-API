using FluentAssertions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.UpdateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Moq;

namespace InteractiveNaturalDisasterMap.Applications.UnitTests.EventCategories.Commands
{
    public class UpdateEventCategoryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        private Mock<IGenericBaseEntityRepository<EventCategory>> _eventCategoryRepositoryMock =
            new Mock<IGenericBaseEntityRepository<EventCategory>>();

        [SetUp]
        protected void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _eventCategoryRepositoryMock = new Mock<IGenericBaseEntityRepository<EventCategory>>();
        }

        [Test]
        public async Task UpdateEventCategoryHandlerTests_WhenEventCategoryIsExists_ShouldUpdateEventCategory()
        {
            // Arrange
            var request = new UpdateEventCategoryRequest()
            {
                UpdateEventCategoryDto = new UpdateEventCategoryDto { Id = 1, CategoryName = "Test" },
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.UpdateEventCategoryDto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new EventCategory());
            _eventCategoryRepositoryMock.Setup(x => x.Update(It.Is<EventCategory>(ec => ec.CategoryName == request.UpdateEventCategoryDto.CategoryName)));

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new UpdateEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _eventCategoryRepositoryMock.Verify(ecr => ecr.GetByIdAsync(request.UpdateEventCategoryDto.Id, It.IsAny<CancellationToken>()),
                Times.Once, "eventCategoryRepository GetByIdAsync() is failed");
            _eventCategoryRepositoryMock.Verify(ecr => ecr.Update(It.Is<EventCategory>(ec => ec.CategoryName == request.UpdateEventCategoryDto.CategoryName)),
                Times.Once, "eventCategoryRepository Update() is failed");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once, "unitOfWork SaveAsync() is failed");
        }

        [Test]
        public async Task UpdateEventCategoryHandlerTests_WhenEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateEventCategoryRequest()
            {
                UpdateEventCategoryDto = new UpdateEventCategoryDto { Id = 1, CategoryName = "Test" },
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.UpdateEventCategoryDto.Id, It.IsAny<CancellationToken>()));

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new UpdateEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Entity - EventCategory({request.UpdateEventCategoryDto.Id}) not found");

            _eventCategoryRepositoryMock.Verify(ecr => ecr.Update(It.IsAny<EventCategory>()), Times.Never, "eventCategoryRepository Update() is calls");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never, "unitOfWork SaveAsync() is calls");
        }
    }
}
