using DOU.GestionOT.WEBI.Data;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.WEBI.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DOUGestionOTWEBIContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DOUGestionOTWEBIContext>>()))
            {
                // Look for any movies.
                if (context.Ot.Any())
                {
                    return;   // DB has been seeded
                }
                context.Ot.AddRange(
                    new()
                    {
                        Numero = 32,
                        Serie = "P",
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
                        Tipo = "PARTE OBRA",
                        CodigoTipo ="5",
                        Cliente = "CP SENDEJA, 3 - BILBAO",
                        Direccion = "C/ SENDEJA, 3",
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00),
                        Estado = "PENDIENTE"
                    },
                    new()
                    {
                        Numero = 35,
                        Serie = "P",
                        Tipo = "PARTE CCTV",
                        CodigoTipo = "5",
                        Cliente = "CP URETAMENDI 49 A 71",
                        Direccion = "C/ URETAMENDI, 49",
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00),
                        Estado = "PENDIENTE"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
