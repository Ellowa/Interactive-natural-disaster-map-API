using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Moq;

namespace InteractiveNaturalDisasterMap.Applications.UnitTests.EventCategories.Commands.CreateEventCategory
{
    public class CreateEventCategoryHandlerTests
    {

        [Test]
        public async Task CreateEventCategoryHandlerTests_WhenEventCategoryIsValid_ShouldCreateEventCategoryWithUndefinedMagnitude()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var eventCategoryRepository = new Mock<IGenericBaseEntityRepository<EventCategory>>();
            var magnitudeUnitRepository = new Mock<IGenericBaseEntityRepository<MagnitudeUnit>>();

            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };

            var undefinedMagnitudeUnit = new MagnitudeUnit
            {
                MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit
            };

            eventCategoryRepository.Setup(x => x.AddAsync(It.IsAny<EventCategory>(), It.IsAny<CancellationToken>()));
            magnitudeUnitRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<MagnitudeUnit, bool>>>()))
                .ReturnsAsync(new List<MagnitudeUnit> { undefinedMagnitudeUnit });

            unitOfWork.Setup(x => x.MagnitudeUnitRepository).Returns(magnitudeUnitRepository.Object);
            unitOfWork.Setup(x => x.EventCategoryRepository).Returns(eventCategoryRepository.Object);

            var handler = new CreateEventCategoryHandler(unitOfWork.Object);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            eventCategoryRepository.Verify(ecr => ecr.AddAsync(It.IsAny<EventCategory>(), CancellationToken.None), Times.Once);
            magnitudeUnitRepository.Verify(mur => mur.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<MagnitudeUnit, bool>>>()), Times.Once);
            unitOfWork.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once);
        }
    }
}
