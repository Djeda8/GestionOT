using DOU.GestionOT.BL.Entities;
using DOU.GestionOT.DAL;
using DOU.GestionOT.WEBI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOU.GestionOT.BL.Services;

namespace DOU.GestionOT.WEBI.Controllers
{
    public class OtsController : Controller
    {
        private readonly GestionOTContext _context;
        private readonly IOtBLService _otService;

        public OtsController(GestionOTContext context, IOtBLService otService)
        {
            _context = context;
            _otService = otService;
        }

        // GET: Ots
        public async Task<IActionResult> Index(string otEstado, string searchString)
        {
            if (_context.Ot == null)
            {
                return Problem("Entity set 'DOUGestionOTWEBIContext.Ot'  is null.");
            }


            var ots = await _otService.GetOtsAsyncs();

            // Use LINQ to get list of genres.
            IQueryable<string> estadosQuery = from m in _context.Ot
                                              orderby m.Estado
                                              select m.Estado;

            if (!String.IsNullOrEmpty(searchString))
            {
                ots = ots.Where(s => s.OtKey!.Contains(searchString)).ToList();
            }

            if (!string.IsNullOrEmpty(otEstado))
            {
                ots = ots.Where(x => string.Equals(x.Estado, otEstado)).ToList();
            }

            var movieGenreVM = new OtEstadoViewModel
            {
                Estados = new SelectList(await estadosQuery.Distinct().ToListAsync()),
                Ots = ots.ToList(),
            };

            return View(movieGenreVM);
        }

        // GET: Ots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ot = await _context.Ot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ot == null)
            {
                return NotFound();
            }

            return View(ot);
        }

        // GET: Ots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,CodigoTipo,TipoParte,Ejercicio,Serie,Numero,Tipo,Cliente,Direccion,Fecha")] Ot ot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ot);
        }

        // GET: Ots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ot = await _context.Ot.FindAsync(id);
            if (ot == null)
            {
                return NotFound();
            }
            return View(ot);
        }

        // POST: Ots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado,CodigoTipo,TipoParte,Ejercicio,Serie,Numero,Tipo,Cliente,Direccion,Fecha")] Ot ot)
        {
            if (id != ot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtExists(ot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ot);
        }

        // GET: Ots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ot = await _context.Ot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ot == null)
            {
                return NotFound();
            }

            return View(ot);
        }

        // POST: Ots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ot = await _context.Ot.FindAsync(id);
            if (ot != null)
            {
                _context.Ot.Remove(ot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtExists(int id)
        {
            return _context.Ot.Any(e => e.Id == id);
        }
    }
}
