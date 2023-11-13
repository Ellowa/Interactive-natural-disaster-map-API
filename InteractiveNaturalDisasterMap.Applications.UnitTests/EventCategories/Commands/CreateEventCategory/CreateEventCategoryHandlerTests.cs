using System.Linq.Expressions;
using FluentAssertions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Moq;

namespace InteractiveNaturalDisasterMap.Applications.UnitTests.EventCategories.Commands
{
    public class CreateEventCategoryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        private Mock<IGenericBaseEntityRepository<EventCategory>> _eventCategoryRepositoryMock =
            new Mock<IGenericBaseEntityRepository<EventCategory>>();

        private Mock<IGenericBaseEntityRepository<MagnitudeUnit>> _magnitudeUnitRepositoryMock =
            new Mock<IGenericBaseEntityRepository<MagnitudeUnit>>();

        [SetUp]
        protected void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _eventCategoryRepositoryMock = new Mock<IGenericBaseEntityRepository<EventCategory>>();
            _magnitudeUnitRepositoryMock = new Mock<IGenericBaseEntityRepository<MagnitudeUnit>>();
        }

        [Test]
        public async Task CreateEventCategoryHandlerTests_WhenUndefinedMagnitudeUnitIsExists_ShouldCreateEventCategoryWithUndefinedMagnitude()
        {
            // Arrange
            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };

            var undefinedMagnitudeUnit = new MagnitudeUnit
            {
                MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit
            };

            _eventCategoryRepositoryMock.Setup(x => x.AddAsync(It.IsAny<EventCategory>(), It.IsAny<CancellationToken>()));
            _magnitudeUnitRepositoryMock.Setup(x =>
                    x.GetAllAsync(It.IsAny<CancellationToken>(), mu => mu.MagnitudeUnitName == EntityNamesByDefault.DefaultMagnitudeUnit))
                .ReturnsAsync(new List<MagnitudeUnit> { undefinedMagnitudeUnit });

            _unitOfWorkMock.Setup(x => x.MagnitudeUnitRepository).Returns(_magnitudeUnitRepositoryMock.Object);
            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new CreateEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            _magnitudeUnitRepositoryMock.Verify(
                mur => mur.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<MagnitudeUnit, bool>>>()), Times.Once,
                "magnitudeUnitRepository GetAllAsync() is failed");
            _eventCategoryRepositoryMock.Verify(ecr => ecr.AddAsync(It.Is<EventCategory>(ec => ec.Id == result), It.IsAny<CancellationToken>()),
                Times.Once, "eventCategoryRepository AddAsync() is failed");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once, "unitOfWork SaveAsync() is failed");
            result.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public async Task CreateEventCategoryHandlerTests_WhenUndefinedMagnitudeUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };

            _eventCategoryRepositoryMock.Setup(x => x.AddAsync(It.IsAny<EventCategory>(), It.IsAny<CancellationToken>()));
            _magnitudeUnitRepositoryMock.Setup(x =>
                    x.GetAllAsync(It.IsAny<CancellationToken>(), mu => mu.MagnitudeUnitName == EntityNamesByDefault.DefaultMagnitudeUnit))
                .ReturnsAsync(new List<MagnitudeUnit>());

            _unitOfWorkMock.Setup(x => x.MagnitudeUnitRepository).Returns(_magnitudeUnitRepositoryMock.Object);
            _unitOfWorkMock.Setup(x => x.EventCategoryRepository).Returns(_eventCategoryRepositoryMock.Object);

            var handler = new CreateEventCategoryHandler(_unitOfWorkMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Entity - MagnitudeUnit(With name {EntityNamesByDefault.DefaultMagnitudeUnit}) not found");
            _magnitudeUnitRepositoryMock.Verify(
                mur => mur.GetAllAsync(It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<MagnitudeUnit, bool>>>()), Times.Once,
                "magnitudeUnitRepository GetAllAsync() is failed");
            _eventCategoryRepositoryMock.Verify(
                ecr => ecr.AddAsync(It.Is<EventCategory>(ec => ec.CategoryName == request.CreateEventCategoryDto.CategoryName),
                    It.IsAny<CancellationToken>()), Times.Never, "eventCategoryRepository AddAsync() is calls");
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never, "unitOfWork SaveAsync() is calls");
        }
    }
}
