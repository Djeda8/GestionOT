using DOU.GestionOT.BL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DOU.GestionOT.WEBI.Models
{
    public class OtEstadoViewModel
    {
        public List<OtDto>? Ots { get; set; }
        public SelectList? Estados { get; set; }
        public string? OtEstado { get; set; }
        public string? SearchString { get; set; }
    }
}
