using AutoMapper;
using DOU.GestionOT.API.Domain.Commands;
using DOU.GestionOT.API.Test.Fixtures;
using DOU.GestionOT.DAL;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Test.Domain.Commands
{
    [Collection("Handlers")]
    public class DeleteOtDtoHandlerTests 
    {
        private static GestionOTContext db;

        public DeleteOtDtoHandlerTests(HandlersFixture handlersFixture)
        {
            db = handlersFixture.DB;
        }

        [Fact]
        public async Task DeleteOtDtoHandler_ShouldDelete_Ot()
        {
            // Arrange
            var id = 1;

            var command = new DeleteOtDtoCommand(id);
            var handler = new DeleteOtDtoHandler(db);

            // Act
            await handler.Handle(command, CancellationToken.None);
            var otDB = await db.Ot.SingleOrDefaultAsync(x => x.Id == id);

            // Assert
            otDB.Should().BeNull();
            db.Ot.Should().HaveCount(3);
        }
        
        [Fact]
        public async Task DeleteOtDtoHandler_ThrowInvalidOperationException_IfOtNotExist()
        {
            // Arrange
            var id = 7;

            var command = new DeleteOtDtoCommand(id);
            var handler = new DeleteOtDtoHandler(db);

            // Act
            var act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Ot with id: '{command.Id}' does not exist");
        }

        [Fact]
        public void DeleteOtDtoHandler_WhenDbIsNull_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => new DeleteOtDtoHandler(null!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("context");
        }
    }
}
