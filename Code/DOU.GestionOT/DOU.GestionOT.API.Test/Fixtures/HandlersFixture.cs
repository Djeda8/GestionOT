using AutoMapper;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Test.Fixtures
{
    public class HandlersFixture : IClassFixture<HandlersFixture>
    {
        public GestionOTContext DB => db;
        public IMapper MAPPER => mapper;

        private static GestionOTContext db;

        private readonly IMapper mapper;

        public HandlersFixture()
        {
            db = new GestionOTContext(
                      new DbContextOptionsBuilder<GestionOTContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options
                  );

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoMappingProfile());
            });

            mapper = mappingConfig.CreateMapper();

            SeedDatabase();
        }

        private static void SeedDatabase()
        {
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
        }

        public void Dispose()
        {
    
        }
    }
}
