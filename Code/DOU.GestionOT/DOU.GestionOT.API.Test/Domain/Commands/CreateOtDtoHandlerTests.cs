using AutoMapper;
using DOU.GestionOT.API.Domain.Commands;
using DOU.GestionOT.API.Test.Fixtures;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Test.Domain.Commands
{
    [Collection("Handlers")]
    public class CreateOtDtoHandlerTests
    {
        private static GestionOTContext db;

        private readonly IMapper mapper;

        public CreateOtDtoHandlerTests(HandlersFixture handlersFixture)
        {
            db = handlersFixture.DB;

            mapper = handlersFixture.MAPPER;
        }

        [Fact]
        public async Task CreateOtDtoHandler_ShouldInsertOt()
        {
            // Arrange
            OtDto otDto = new()
            {
                Numero = 36,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                Estado = "INICIADA"
            };

            var command = new CreateOtDtoCommand(otDto);
            var handler = new CreateOtDtoHandler(db, mapper);

            // Act
            await handler.Handle(command, CancellationToken.None);
            var otDB = await db.Ot.SingleOrDefaultAsync(x => x.Numero == otDto.Numero);

            // Assert
            otDB
                .Should()
                .NotBeNull()
                .And.Match<Ot>(u => u.Numero == otDto.Numero && u.Serie == otDto.Serie && u.Ejercicio == otDto.Ejercicio);
        }

        [Fact]
        public void CreateOtDtoHandler_WhenDbIsNull_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => new CreateOtDtoHandler(null!, mapper);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("context");
        }

        [Fact]
        public void CreateOtDtoHandler_WhenMapperIsNull_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => new CreateOtDtoHandler(db, null!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("mapper");
        }
    }
}
