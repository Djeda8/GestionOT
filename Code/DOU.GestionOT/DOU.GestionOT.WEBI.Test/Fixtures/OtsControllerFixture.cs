using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.BL.Services;
using DOU.GestionOT.DAL;
using DOU.GestionOT.WEBI.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DOU.GestionOT.WEBI.Test.Fixtures
{
    public sealed class OtsControllerFixture : IDisposable
    {
        private static GestionOTContext? db;

        public OtsController OtsController { get; }

        public static OtDto[] Files => otDto;

        private readonly static OtDto[] otDto = {
                    new()
                    {
                        Id = 1,
                        Numero = 32,
                        Serie = "P",
                        Ejercicio = 2023,
                        Tipo = "PARTE SERVICIOS",
                        CodigoTipo = "5",
                        Cliente = "CP PLAZA KOLITZA, 1",
                        Direccion = "PLAZA KOLITXA, 1",
                        Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
                        Estado = "INICIADA"
                    },
                    new()
                    {
                        Id = 2,
                        Numero = 34,
                        Serie = "P",
                        Ejercicio = 2023,
                        Tipo = "PARTE OBRA",
                        CodigoTipo = "5",
                        Cliente = "CP SENDEJA, 3 - BILBAO",
                        Direccion = "C/ SENDEJA, 3",
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00, DateTimeKind.Utc),
                        Estado = "PENDIENTE"
                    },
                    new()
                    {
                        Id = 3,
                        Numero = 35,
                        Serie = "P",
                        Ejercicio = 2023,
                        Tipo = "PARTE CCTV",
                        CodigoTipo = "5",
                        Cliente = "CP URETAMENDI 49 A 71",
                        Direccion = "C/ URETAMENDI, 49",
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00, DateTimeKind.Utc),
                        Estado = "PENDIENTE"
                    } };


        public OtsControllerFixture()
        {
            SeedDatabase();

            var otBLService = new Mock<IOtBLService>();
            otBLService.Setup(x => x.GetOtsAsync()).ReturnsAsync(otDto);

            otBLService.Setup(x => x.DeleteOtAsync(It.IsAny<OtDto>())).Returns( Task.CompletedTask);

            otBLService.Setup(x => x.GetOtAsync(9999)).ReturnsAsync( (OtDto?) null);
            otBLService.Setup(x => x.GetOtAsync(1)).ReturnsAsync(new OtDto()
            {
                Id = 1,
                Numero = 32,
                Serie = "P",
                Ejercicio = 2023,
                Tipo = "PARTE SERVICIOS",
                CodigoTipo = "5",
                Cliente = "CP PLAZA KOLITZA, 1",
                Direccion = "PLAZA KOLITXA, 1",
                Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
                Estado = "INICIADA"
            });

            OtsController = new OtsController(db!, otBLService.Object);
        }

        private static void SeedDatabase()
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
                        Fecha = new DateTime(2016, 6, 30, 15, 00, 00, DateTimeKind.Utc),
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
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00, DateTimeKind.Utc),
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
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00, DateTimeKind.Utc),
                        Estado = "PENDIENTE"
                    });

            db.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
