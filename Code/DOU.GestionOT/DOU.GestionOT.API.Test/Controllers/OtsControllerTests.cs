using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace DOU.GestionOT.API.Test.Controllers
{

    public class OtsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _client;

        private static GestionOTContext db;

        public OtsControllerTests(WebApplicationFactory<Program> app)
        {

            app = new ApiApplication(services =>
            {
                services.AddSingleton(_ => SeedDatabase());

            });

            _client = app.CreateClient();
        }

        [Fact]
        public async Task GetOts_Should_be_OK_And_Return_3_OtsDto()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/Ots");
            var returnMessage = await response.Content.ReadAsStringAsync();
            var itemList = JsonConvert.DeserializeObject<IList<OtDto>>(returnMessage);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            itemList.Should().BeOfType<List<OtDto>>().And.HaveCount(3);
        }

        [Fact]
        public async Task GetOt_Should_Be_First_OtDto()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/Ots/1");
            var returnMessage = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<OtDto>(returnMessage);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            item.Should().BeOfType<OtDto>();
            Assert.True(item.Numero == 32);
        }

        [Fact]
        public async Task GetOt_ThatNotExists_Should_Be_InternalServerError()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/Ots/4");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedAndId()
        {
            // Arrange
            OtDto otDto = new()
            {
                Numero = 32,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                Estado = "INICIADA"
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(otDto), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/Ots", content);

            var response = await result.Content.ReadAsStringAsync();
            var newOtDto = JsonConvert.DeserializeObject<OtDto>(response);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            newOtDto.Numero.Should().Be(otDto.Numero);
        }

        [Fact]
        public async Task Put_ShouldReturnNoContent()
        {
            // Arrange
            var response = await _client.GetAsync("/api/Ots/1");
            var returnMessage = await response.Content.ReadAsStringAsync();
            var otDto = JsonConvert.DeserializeObject<OtDto>(returnMessage);
            otDto.Numero = 6000;

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(otDto), Encoding.UTF8, "application/json");
            var result = await _client.PutAsync($"/api/Ots/{otDto.Id}", content);

            response = await _client.GetAsync($"/api/Ots/{otDto.Id}");
            returnMessage = await response.Content.ReadAsStringAsync();
            var otDtoUpdate = JsonConvert.DeserializeObject<OtDto>(returnMessage);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            otDtoUpdate.Numero.Should().Be(otDto.Numero);
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest()
        {
            // Arrange
            var response = await _client.GetAsync("/api/Ots/1");
            var returnMessage = await response.Content.ReadAsStringAsync();
            var otDto = JsonConvert.DeserializeObject<OtDto>(returnMessage);

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(otDto), Encoding.UTF8, "application/json");
            var result = await _client.PutAsync($"/api/Ots/2", content);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            // Arrange
            var response = await _client.GetAsync("/api/Ots/2");
            var returnMessage = await response.Content.ReadAsStringAsync();
            var otDto = JsonConvert.DeserializeObject<OtDto>(returnMessage);

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(otDto), Encoding.UTF8, "application/json");
            var result = await _client.DeleteAsync($"/api/Ots/{otDto.Id}");
            response = await _client.GetAsync($"/api/Ots/{otDto.Id}");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        private static GestionOTContext SeedDatabase()
        {
            db = new GestionOTContext(
                new DbContextOptionsBuilder<GestionOTContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options
            );

            db.Ot.AddRange(
                    new()
                    {
                        Numero = 32,
                        Serie = "P",
                        Ejercicio = 2023,
                        Tipo = "PARTE SERVICIOS",
                        CodigoTipo = "5",
                        Cliente = "CP PLAZA KOLITZA, 1",
                        Direccion = "PLAZA KOLITXA, 1",
                        Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                        Estado = "INICIADA"
                    },
                    new()
                    {
                        Numero = 34,
                        Serie = "P",
                        Ejercicio = 2023,
                        Tipo = "PARTE OBRA",
                        CodigoTipo = "5",
                        Cliente = "CP SENDEJA, 3 - BILBAO",
                        Direccion = "C/ SENDEJA, 3",
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00),
                        Estado = "PENDIENTE"
                    },
                    new()
                    {
                        Numero = 35,
                        Serie = "P",
                        Ejercicio = 2023,
                        Tipo = "PARTE CCTV",
                        CodigoTipo = "5",
                        Cliente = "CP URETAMENDI 49 A 71",
                        Direccion = "C/ URETAMENDI, 49",
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00),
                        Estado = "PENDIENTE"
                    });

            db.SaveChanges();

            return db;
        }
    }
}
