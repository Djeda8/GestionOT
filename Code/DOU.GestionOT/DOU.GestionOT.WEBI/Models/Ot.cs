using System.ComponentModel.DataAnnotations;

namespace DOU.GestionOT.WEBI.Models
{
    public class Ot
    {
        private string otKey;

        public int Id { get; set; }
        public string Estado { get; set; }
        public string CodigoTipo { get; set; }
        public string? TipoParte { get; set; }
        [Range(2023, 2035)]
        public int Ejercicio { get; set; }
        [StringLength(1, MinimumLength = 1)]
        [Required]
        public string Serie { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string? Direccion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha intervención")]
        public DateTime Fecha { get; set; }
        [Display(Name = "OT")]
        public string OtKey { get => $"{Numero} {Serie} {Ejercicio}";}
    }
}
