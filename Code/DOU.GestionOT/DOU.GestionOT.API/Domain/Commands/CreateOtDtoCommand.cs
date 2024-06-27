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

        public CreateOtDtoCommand(OtDto otDto)
        {
            if (string.IsNullOrEmpty(otDto.Estado)) throw new ArgumentNullException(nameof(otDto));
            if (string.IsNullOrEmpty(otDto.CodigoTipo)) throw new ArgumentNullException(nameof(otDto));
            if (string.IsNullOrEmpty(otDto.Serie)) throw new ArgumentNullException(nameof(otDto));
            if (string.IsNullOrEmpty(otDto.Tipo)) throw new ArgumentNullException(nameof(otDto));
            if (string.IsNullOrEmpty(otDto.Cliente)) throw new ArgumentNullException(nameof(otDto));

            Estado = otDto.Estado;
            CodigoTipo = otDto.CodigoTipo;
            TipoParte = otDto.TipoParte;
            Ejercicio = otDto.Ejercicio;
            Serie = otDto.Serie;
            Numero = otDto.Numero;
            Tipo = otDto.Tipo;
            Cliente = otDto.Cliente;
            Direccion = otDto.Direccion;
            Fecha = otDto.Fecha;
        }
    }
}
