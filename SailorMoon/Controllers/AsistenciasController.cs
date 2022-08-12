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
    public class AsistenciasController : Controller
    {
        private readonly SailorMoonContext _context;

        public AsistenciasController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: Asistencias
        public async Task<IActionResult> Index()
        {
            var sailorMoonContext = _context.Asistencias.Include(a => a.IdEstudianteNavigation);
            return View(await sailorMoonContext.ToListAsync());
        }

        // GET: Asistencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asistencias == null)
            {
                return NotFound();
            }

            var asistencias = await _context.Asistencias
                .Include(a => a.IdEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.IdAsistencias == id);
            if (asistencias == null)
            {
                return NotFound();
            }

            return View(asistencias);
        }

        // GET: Asistencias/Create
        public IActionResult Create()
        {
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre");
            return View();
        }

        // POST: Asistencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsistencias,InastJustificada,InastInjustificada,TardJustificada,TardInjustificada,IdEstudiante")] Asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", asistencias.IdEstudiante);
            return View(asistencias);
        }

        // GET: Asistencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asistencias == null)
            {
                return NotFound();
            }

            var asistencias = await _context.Asistencias.FindAsync(id);
            if (asistencias == null)
            {
                return NotFound();
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", asistencias.IdEstudiante);
            return View(asistencias);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsistencias,InastJustificada,InastInjustificada,TardJustificada,TardInjustificada,IdEstudiante")] Asistencias asistencias)
        {
            if (id != asistencias.IdAsistencias)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciasExists(asistencias.IdAsistencias))
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
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", asistencias.IdEstudiante);
            return View(asistencias);
        }

        // GET: Asistencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asistencias == null)
            {
                return NotFound();
            }

            var asistencias = await _context.Asistencias
                .Include(a => a.IdEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.IdAsistencias == id);
            if (asistencias == null)
            {
                return NotFound();
            }

            return View(asistencias);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asistencias == null)
            {
                return Problem("Entity set 'SailorMoonContext.Asistencias'  is null.");
            }
            var asistencias = await _context.Asistencias.FindAsync(id);
            if (asistencias != null)
            {
                _context.Asistencias.Remove(asistencias);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciasExists(int id)
        {
          return (_context.Asistencias?.Any(e => e.IdAsistencias == id)).GetValueOrDefault();
        }
    }
}
