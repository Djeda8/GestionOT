using DOU.GestionOT.BL.Dto;
using MediatR;

namespace DOU.GestionOT.API.Domain.Commands
{
    public class CreateOtDtoCommand : IRequest<OtDto>
    {
        public string Estado { get; set; }
        public string CodigoTipo { get; set; }
        public string? TipoParte { get; set; }
        public int Ejercicio { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string? Direccion { get; set; }
        public DateTime Fecha { get; set; }

        public CreateOtDtoCommand(string estado, string codigoTipo, string? tipoParte, int ejercicio, string serie, int numero, string tipo, string cliente, string? direccion, DateTime fecha)
        {
            Estado = estado ?? throw new ArgumentNullException(nameof(estado));
            CodigoTipo = codigoTipo ?? throw new ArgumentNullException(nameof(codigoTipo));
            TipoParte = tipoParte;
            Ejercicio = ejercicio;
            Serie = serie ?? throw new ArgumentNullException(nameof(serie));
            Numero = numero;
            Tipo = tipo ?? throw new ArgumentNullException(nameof(tipo));
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Direccion = direccion;
            Fecha = fecha;
        }
    }
}
