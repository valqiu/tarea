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
    public class InstitucionsController : Controller
    {
        private readonly SailorMoonContext _context;

        public InstitucionsController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: Institucions
        public async Task<IActionResult> Index()
        {
              return _context.Institucion != null ? 
                          View(await _context.Institucion.ToListAsync()) :
                          Problem("Entity set 'SailorMoonContext.Institucion'  is null.");
        }

        // GET: Institucions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Institucion == null)
            {
                return NotFound();
            }

            var institucion = await _context.Institucion
                .FirstOrDefaultAsync(m => m.IdInstitucion == id);
            if (institucion == null)
            {
                return NotFound();
            }

            return View(institucion);
        }

        // GET: Institucions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Institucions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInstitucion,Nombre,Nivel,CodigoModular,Logo")] Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institucion);
        }

        // GET: Institucions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Institucion == null)
            {
                return NotFound();
            }

            var institucion = await _context.Institucion.FindAsync(id);
            if (institucion == null)
            {
                return NotFound();
            }
            return View(institucion);
        }

        // POST: Institucions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInstitucion,Nombre,Nivel,CodigoModular,Logo")] Institucion institucion)
        {
            if (id != institucion.IdInstitucion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitucionExists(institucion.IdInstitucion))
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
            return View(institucion);
        }

        // GET: Institucions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Institucion == null)
            {
                return NotFound();
            }

            var institucion = await _context.Institucion
                .FirstOrDefaultAsync(m => m.IdInstitucion == id);
            if (institucion == null)
            {
                return NotFound();
            }

            return View(institucion);
        }

        // POST: Institucions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Institucion == null)
            {
                return Problem("Entity set 'SailorMoonContext.Institucion'  is null.");
            }
            var institucion = await _context.Institucion.FindAsync(id);
            if (institucion != null)
            {
                _context.Institucion.Remove(institucion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucionExists(int id)
        {
          return (_context.Institucion?.Any(e => e.IdInstitucion == id)).GetValueOrDefault();
        }
    }
}
