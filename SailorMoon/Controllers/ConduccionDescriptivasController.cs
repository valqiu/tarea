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
    public class ConduccionDescriptivasController : Controller
    {
        private readonly SailorMoonContext _context;

        public ConduccionDescriptivasController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: ConduccionDescriptivas
        public async Task<IActionResult> Index()
        {
            var sailorMoonContext = _context.ConduccionDescriptiva.Include(c => c.IdEstudianteNavigation);
            return View(await sailorMoonContext.ToListAsync());
        }

        // GET: ConduccionDescriptivas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConduccionDescriptiva == null)
            {
                return NotFound();
            }

            var conduccionDescriptiva = await _context.ConduccionDescriptiva
                .Include(c => c.IdEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.IdConduccionDescriptiva == id);
            if (conduccionDescriptiva == null)
            {
                return NotFound();
            }

            return View(conduccionDescriptiva);
        }

        // GET: ConduccionDescriptivas/Create
        public IActionResult Create()
        {
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre");
            return View();
        }

        // POST: ConduccionDescriptivas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConduccionDescriptiva,Descripcion,Periodo,IdEstudiante,IdPersonal")] ConduccionDescriptiva conduccionDescriptiva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conduccionDescriptiva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", conduccionDescriptiva.IdEstudiante);
            return View(conduccionDescriptiva);
        }

        // GET: ConduccionDescriptivas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConduccionDescriptiva == null)
            {
                return NotFound();
            }

            var conduccionDescriptiva = await _context.ConduccionDescriptiva.FindAsync(id);
            if (conduccionDescriptiva == null)
            {
                return NotFound();
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", conduccionDescriptiva.IdEstudiante);
            return View(conduccionDescriptiva);
        }

        // POST: ConduccionDescriptivas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConduccionDescriptiva,Descripcion,Periodo,IdEstudiante,IdPersonal")] ConduccionDescriptiva conduccionDescriptiva)
        {
            if (id != conduccionDescriptiva.IdConduccionDescriptiva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conduccionDescriptiva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConduccionDescriptivaExists(conduccionDescriptiva.IdConduccionDescriptiva))
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
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", conduccionDescriptiva.IdEstudiante);
            return View(conduccionDescriptiva);
        }

        // GET: ConduccionDescriptivas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConduccionDescriptiva == null)
            {
                return NotFound();
            }

            var conduccionDescriptiva = await _context.ConduccionDescriptiva
                .Include(c => c.IdEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.IdConduccionDescriptiva == id);
            if (conduccionDescriptiva == null)
            {
                return NotFound();
            }

            return View(conduccionDescriptiva);
        }

        // POST: ConduccionDescriptivas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConduccionDescriptiva == null)
            {
                return Problem("Entity set 'SailorMoonContext.ConduccionDescriptiva'  is null.");
            }
            var conduccionDescriptiva = await _context.ConduccionDescriptiva.FindAsync(id);
            if (conduccionDescriptiva != null)
            {
                _context.ConduccionDescriptiva.Remove(conduccionDescriptiva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConduccionDescriptivaExists(int id)
        {
          return (_context.ConduccionDescriptiva?.Any(e => e.IdConduccionDescriptiva == id)).GetValueOrDefault();
        }
    }
}
