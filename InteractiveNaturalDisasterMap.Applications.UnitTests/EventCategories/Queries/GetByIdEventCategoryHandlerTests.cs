using FluentAssertions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetByIdEventCategory;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Applications.UnitTests.EventCategories.Queries
{
    public class GetByIdEventCategoryHandlerTests
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
        public async Task GetByIdEventCategoryHandlerTests_WhenEventCategoryIsExists_ShouldReturnEventCategory()
        {
            // Arrange
            var request = new GetByIdEventCategoryRequest()
            {
                GetByIdEventCategoryDto = new GetByIdEventCategoryDto { Id = 1 }
            };

            var expectedEventCategory = new EventCategory 
            {
                Id =1,
                CategoryName= "Test",
                MagnitudeUnits = new List<MagnitudeUnit>()
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.GetByIdEventCategoryDto.Id, It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<EventCategory, object>>>()))
                .ReturnsAsync(expectedEventCategory);

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new GetByIdEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            _eventCategoryRepositoryMock.Verify(ecr => ecr.GetByIdAsync(request.GetByIdEventCategoryDto.Id, It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<EventCategory, object>>>()),
                Times.Once, "eventCategoryRepository GetByIdAsync() is failed");
            result.CategoryName.Should().Be(expectedEventCategory.CategoryName);
        }

        [Test]
        public async Task GetByIdEventCategoryHandlerTests_WhenEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdEventCategoryRequest()
            {
                GetByIdEventCategoryDto = new GetByIdEventCategoryDto { Id = 1 }
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetByIdAsync(request.GetByIdEventCategoryDto.Id, It.IsAny<CancellationToken>()));

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new GetByIdEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Entity - EventCategory({request.GetByIdEventCategoryDto.Id}) not found");
        }
    }
}
