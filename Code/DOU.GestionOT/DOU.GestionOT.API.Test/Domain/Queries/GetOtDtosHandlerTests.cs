using AutoMapper;
using DOU.GestionOT.API.Domain.Commands;
using DOU.GestionOT.API.Domain.Queries;
using DOU.GestionOT.API.Test.Fixtures;
using DOU.GestionOT.DAL;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Test.Domain.Queries
{
    [Collection("Handlers")]
    public class GetOtDtosHandlerTests
    {
        private static GestionOTContext db;

        private readonly IMapper mapper;

        public GetOtDtosHandlerTests(HandlersFixture handlersFixture)
        {
            db = handlersFixture.DB;
            mapper = handlersFixture.MAPPER;
        }

        [Fact]
        public async Task GetOtDtosHandler_ReturnExpectedOrders()
        {
            // Arrange
            var id = 1;

            var query = new GetOtDtosQuery();
            var handler = new GetOtDtosHandler(db, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(3);
        }


        [Fact]
        public void GetOtDtosHandler_WhenDbIsNull_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => new GetOtDtosHandler(null!, mapper);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("context");
        }

        [Fact]
        public void GetOtDtosHandler_WhenMapperIsNull_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => new GetOtDtosHandler(db, null!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("mapper");
        }
    }
}
