using Microsoft.AspNetCore.Mvc.Rendering;

namespace DOU.GestionOT.WEBI.Models
{
    public class OtEstadoViewModel
    {
        public List<Ot>? Ots { get; set; }
        public SelectList? Estados { get; set; }
        public string? OtEstado { get; set; }
        public string? SearchString { get; set; }
    }
}
