using FluentAssertions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetAllEventCategory;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Applications.UnitTests.EventCategories.Queries
{
    public class GetAllEventCategoryHandlerTests
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
        public async Task GetAllEventCategoryHandlerTests_WhenEventCategoriesIsExists_ShouldReturnEventCategories()
        {
            // Arrange
            var request = new GetAllEventCategoryRequest()
            {
            };

            var expectedEventCategories = new List<EventCategory>
            { 
                new EventCategory { MagnitudeUnits = new List<MagnitudeUnit>()},
                new EventCategory { MagnitudeUnits = new List<MagnitudeUnit>()},
                new EventCategory { MagnitudeUnits = new List<MagnitudeUnit>()}
            };

            _eventCategoryRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<EventCategory, bool>>>(), It.IsAny<Expression<Func<EventCategory, object>>>()))
                .ReturnsAsync(expectedEventCategories);

            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new GetAllEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            _eventCategoryRepositoryMock.Verify(ecr => ecr.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<EventCategory, bool>>>(), It.IsAny<Expression<Func<EventCategory, object>>>()),
                Times.Once, "eventCategoryRepository GetAllAsync() is failed");
            result.Count.Should().Be(expectedEventCategories.Count);
        }
    }
}
