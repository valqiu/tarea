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
    public class GradoesController : Controller
    {
        private readonly SailorMoonContext _context;

        public GradoesController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: Gradoes
        public async Task<IActionResult> Index()
        {
              return _context.Grado != null ? 
                          View(await _context.Grado.ToListAsync()) :
                          Problem("Entity set 'SailorMoonContext.Grado'  is null.");
        }

        // GET: Gradoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grado == null)
            {
                return NotFound();
            }

            var grado = await _context.Grado
                .FirstOrDefaultAsync(m => m.IdGrado == id);
            if (grado == null)
            {
                return NotFound();
            }

            return View(grado);
        }

        // GET: Gradoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gradoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGrado,Grado1,Seccion,Tutor")] Grado grado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grado);
        }

        // GET: Gradoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grado == null)
            {
                return NotFound();
            }

            var grado = await _context.Grado.FindAsync(id);
            if (grado == null)
            {
                return NotFound();
            }
            return View(grado);
        }

        // POST: Gradoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGrado,Grado1,Seccion,Tutor")] Grado grado)
        {
            if (id != grado.IdGrado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradoExists(grado.IdGrado))
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
            return View(grado);
        }

        // GET: Gradoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grado == null)
            {
                return NotFound();
            }

            var grado = await _context.Grado
                .FirstOrDefaultAsync(m => m.IdGrado == id);
            if (grado == null)
            {
                return NotFound();
            }

            return View(grado);
        }

        // POST: Gradoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grado == null)
            {
                return Problem("Entity set 'SailorMoonContext.Grado'  is null.");
            }
            var grado = await _context.Grado.FindAsync(id);
            if (grado != null)
            {
                _context.Grado.Remove(grado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradoExists(int id)
        {
          return (_context.Grado?.Any(e => e.IdGrado == id)).GetValueOrDefault();
        }
    }
}
