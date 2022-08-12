using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SailorMoon.Context;
using SailorMoon.Models;

namespace SailorMoon.Controllers
{
    public class NotasPeriodoesController : Controller
    {
        private readonly SailorMoonContext _context;

        public NotasPeriodoesController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: NotasPeriodoes
        public async Task<IActionResult> Index()
        {
            var sailorMoonContext = _context.NotasPeriodo.Include(n => n.IdNotasNavigation);
            return View(await sailorMoonContext.ToListAsync());
        }

        // GET: NotasPeriodoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NotasPeriodo == null)
            {
                return NotFound();
            }

            var notasPeriodo = await _context.NotasPeriodo
                .Include(n => n.IdNotasNavigation)
                .FirstOrDefaultAsync(m => m.IdPeriodo == id);
            if (notasPeriodo == null)
            {
                return NotFound();
            }

            return View(notasPeriodo);
        }

        // GET: NotasPeriodoes/Create
        public IActionResult Create()
        {
            ViewData["IdNotas"] = new SelectList(_context.Notas, "IdNotas", "Promedio");
            return View();
        }

        // POST: NotasPeriodoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeriodo,NotaPeriodo1,NotaPeriodo2,NotaPeriodo3,NotaPeriodo4,PromedioAnual,IdNotas")] NotasPeriodo notasPeriodo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notasPeriodo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNotas"] = new SelectList(_context.Notas, "IdNotas", "Promedio", notasPeriodo.IdNotas);
            return View(notasPeriodo);
        }

        // GET: NotasPeriodoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NotasPeriodo == null)
            {
                return NotFound();
            }

            var notasPeriodo = await _context.NotasPeriodo.FindAsync(id);
            if (notasPeriodo == null)
            {
                return NotFound();
            }
            ViewData["IdNotas"] = new SelectList(_context.Notas, "IdNotas", "Promedio", notasPeriodo.IdNotas);
            return View(notasPeriodo);
        }

        // POST: NotasPeriodoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeriodo,NotaPeriodo1,NotaPeriodo2,NotaPeriodo3,NotaPeriodo4,PromedioAnual,IdNotas")] NotasPeriodo notasPeriodo)
        {
            if (id != notasPeriodo.IdPeriodo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notasPeriodo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasPeriodoExists(notasPeriodo.IdPeriodo))
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
            ViewData["IdNotas"] = new SelectList(_context.Notas, "IdNotas", "Promedio", notasPeriodo.IdNotas);
            return View(notasPeriodo);
        }

        // GET: NotasPeriodoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NotasPeriodo == null)
            {
                return NotFound();
            }

            var notasPeriodo = await _context.NotasPeriodo
                .Include(n => n.IdNotasNavigation)
                .FirstOrDefaultAsync(m => m.IdPeriodo == id);
            if (notasPeriodo == null)
            {
                return NotFound();
            }

            return View(notasPeriodo);
        }

        // POST: NotasPeriodoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NotasPeriodo == null)
            {
                return Problem("Entity set 'SailorMoonContext.NotasPeriodo'  is null.");
            }
            var notasPeriodo = await _context.NotasPeriodo.FindAsync(id);
            if (notasPeriodo != null)
            {
                _context.NotasPeriodo.Remove(notasPeriodo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasPeriodoExists(int id)
        {
          return (_context.NotasPeriodo?.Any(e => e.IdPeriodo == id)).GetValueOrDefault();
        }
    }
}
