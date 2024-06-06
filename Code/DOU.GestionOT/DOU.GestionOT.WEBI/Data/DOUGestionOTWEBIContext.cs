using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DOU.GestionOT.WEBI.Models;

namespace DOU.GestionOT.WEBI.Data
{
    public class DOUGestionOTWEBIContext : DbContext
    {
        public DOUGestionOTWEBIContext (DbContextOptions<DOUGestionOTWEBIContext> options)
            : base(options)
        {
        }

        public DbSet<DOU.GestionOT.WEBI.Models.Ot> Ot { get; set; } = default!;
    }
}
