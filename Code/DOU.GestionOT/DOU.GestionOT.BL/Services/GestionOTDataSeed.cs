using DOU.GestionOT.BL.Entities;

namespace DOU.GestionOT.BL.Services
{
    public class GestionOTDataSeed : IGestionOTDataSeed
    {
        public Ot[] GetInitialOts()
        {
            var ots = new[]
           {
                CreateOt(42, "P", "PARTE SERVICIOS", "5", "CP PLAZA KOLITZA, 1","PLAZA KOLITXA, 1", new DateTime(2016, 6, 30, 15, 00, 00), "INICIADA"),
                CreateOt(44, "P", "PARTE OBRA", "5", "CP SENDEJA, 3 - BILBAO","C/ SENDEJA, 3", new DateTime(2016, 6, 30, 18, 30, 00), "PENDIENTE"),
                CreateOt(45, "P", "PARTE CCTV", "5", "CP URETAMENDI 49 A 71","C/ URETAMENDI, 49", new DateTime(2016, 6, 30, 21, 00, 00), "PENDIENTE"),

                
            };
            return ots;

            static Ot CreateOt(int numero, string serie, string tipo, string codigoTipo, string cliente, string direccion, DateTime fecha, string estado)
            {
                var ot = new Ot()
                {
                    Numero = numero,
                    Serie = serie,
                    Tipo = tipo,
                    CodigoTipo = codigoTipo,
                    Cliente = cliente,
                    Direccion = direccion,
                    Fecha = fecha,
                    Estado = estado
                };
                return ot;
            }
        }
    }
}