using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.BL.Services;
using DOU.GestionOT.DAL;
using DOU.GestionOT.WEBI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.WEBI.Controllers
{
    public class OtsController : Controller
    {
        private readonly GestionOTContext _context;
        private readonly IOtBLService _otBLService;

        public OtsController(GestionOTContext context, IOtBLService otBLService)
        {
            _context = context;
            _otBLService = otBLService;
        }

        // GET: Ots
        public async Task<IActionResult> Index(string otEstado, string searchString)
        {
            if (_context.Ot == null)
            {
                return Problem("Entity set 'DOUGestionOTWEBIContext.Ot'  is null.");
            }

            var ots = await _otBLService.GetOtsAsync();

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

            var estadoViewVM = new OtEstadoViewModel
            {
                Estados = new SelectList(await estadosQuery.Distinct().ToListAsync()),
                Ots = ots.ToList(),
            };

            return View(estadoViewVM);
        }

        // GET: Ots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otDto = await _otBLService.GetOtAsync(id);

            if (otDto == null)
            {
                return NotFound();
            }

            return View(otDto);
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
        public async Task<IActionResult> Create([Bind("Id,Estado,CodigoTipo,TipoParte,Ejercicio,Serie,Numero,Tipo,Cliente,Direccion,Fecha")] OtDto otDto)
        {
            if (ModelState.IsValid)
            {
                await _otBLService.PostOtAsync(otDto);

                return RedirectToAction(nameof(Index));
            }
            return View(otDto);
        }

        // GET: Ots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otDto = await _otBLService.GetOtAsync(id);

            if (otDto == null)
            {
                return NotFound();
            }
            return View(otDto);
        }

        // POST: Ots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado,CodigoTipo,TipoParte,Ejercicio,Serie,Numero,Tipo,Cliente,Direccion,Fecha")] OtDto otDto)
        {
            if (id != otDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _otBLService.PutOtAsync(otDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtExists(otDto.Id))
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
            return View(otDto);
        }

        // GET: Ots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otDto = await _otBLService.GetOtAsync(id);

            if (otDto == null)
            {
                return NotFound();
            }

            return View(otDto);
        }

        // POST: Ots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var otDto = await _otBLService.GetOtAsync(id);
            if (otDto == null)
            {
                return NotFound();
            }
            await _otBLService.DeleteOtAsync(otDto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtExists(int id)
        {
            return _context.Ot.Any(e => e.Id == id);
        }
    }
}
