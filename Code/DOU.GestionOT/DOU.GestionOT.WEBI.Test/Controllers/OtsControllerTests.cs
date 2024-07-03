using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.WEBI.Controllers;
using DOU.GestionOT.WEBI.Models;
using DOU.GestionOT.WEBI.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;

namespace DOU.GestionOT.WEBI.Test.Controllers
{
    public class OtsControllerTests : IClassFixture<OtsControllerFixture>
    {
        private readonly OtsController _otsController;

        public OtsControllerTests(OtsControllerFixture fixture)
        {
            _otsController = fixture.OtsController;
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfOts()
        {
            // Arrange

            // Act
            var result = await _otsController.Index("", "");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OtEstadoViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Ots!.Count);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithMovieGenreAndSearchString()
        {
            // Arrange

            // Act
            var result = await _otsController.Index("PENDIENTE", "4");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OtEstadoViewModel>(viewResult.ViewData.Model);
            Assert.Equal(1, model?.Ots?.Count);
        }

        [Fact]
        public async Task Details_ReturnNotFound_withIdNull()
        {
            // Arrange

            // Act
            var result = await _otsController.Details(null);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnNotFound_withIdNotExits()
        {
            // Arrange

            // Act
            var result = await _otsController.Details(9999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnModel_withIdCorrect()
        {
            // Arrange

            // Act
            var result = await _otsController.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OtDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void CreateGet_ReturnsView()
        {
            // Arrange

            // Act
            var result = _otsController.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreatePost_ReturnsModel_WhenModelStateIsInvalid()
        {
            // Arrange
            _otsController.ModelState.AddModelError("Serie", "Required");
            var newMovie = new OtDto();

            // Act
            var result = await _otsController.Create(newMovie);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OtDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsMovie_WhenModelStateIsValid()
        {
            // Arrange
            _otsController.ModelState.Remove("Serie");

            var newOtDto = new OtDto()
            {
                Numero = 36,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
                Estado = "INICIADA"
            };

            // Act
            var result = await _otsController.Create(newOtDto);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnNotFound_withIdNotExits()
        {
            // Arrange

            // Act
            var result = await _otsController.Edit(9999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnNotFound_withIdNull()
        {
            // Arrange

            // Act
            var result = await _otsController.Edit(null);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditGet_ReturnsView()
        {
            // Arrange

            // Act
            var result = _otsController.Edit(1);

            // Assert
            var actionResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsARedirectAndAddsMovie_WhenModelStateIsValid()
        {
            // Arrange

            var newOtDto = new OtDto()
            {
                Id = 1,
                Numero = 36,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
                Estado = "INICIADA"
            };

            // Act
            var result = await _otsController.Edit(newOtDto.Id, newOtDto);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task EditPost_ReturnsNotFound_WhenModelAndIdIsDifferent()
        {
            // Arrange

            var newOtDto = new OtDto()
            {
                Id = 1,
                Numero = 36,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
                Estado = "INICIADA"
            };

            // Act
            var result = await _otsController.Edit(2, newOtDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsModel_WhenModelStateIsInvalid()
        {
            // Arrange
            _otsController.ModelState.AddModelError("Serie", "Required");
            var newOtDto = new OtDto()
            {
                Id = 1,
                Numero = 36,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
                Estado = "INICIADA"
            };

            // Act
            var result = await _otsController.Edit(newOtDto.Id, newOtDto);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OtDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Delete_ReturnNotFound_withIdNotExits()
        {
            // Arrange

            // Act
            var result = await _otsController.Delete(9999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnNotFound_withIdNull()
        {
            // Arrange

            // Act
            var result = await _otsController.Delete(null);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteGet_ReturnsView()
        {
            // Arrange

            // Act
            var result = _otsController.Delete(3);

            // Assert
            var actionResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void DeleteConfirm_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = _otsController.DeleteConfirmed(7000);

            // Assert
            var actionResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void DeleteConfirm_ReturnsView()
        {
            // Arrange

            // Act
            var result = _otsController.DeleteConfirmed(1);

            // Assert
            var actionResult = Assert.IsType<Task<IActionResult>>(result);
        }
    }
}
