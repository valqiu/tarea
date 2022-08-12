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
    public class NotasController : Controller
    {
        private readonly SailorMoonContext _context;

        public NotasController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var sailorMoonContext = _context.Notas.Include(n => n.IdCursoNavigation).Include(n => n.IdEstudianteFkNavigation);
            return View(await sailorMoonContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas
                .Include(n => n.IdCursoNavigation)
                .Include(n => n.IdEstudianteFkNavigation)
                .FirstOrDefaultAsync(m => m.IdNotas == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso");
            ViewData["IdEstudianteFk"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNotas,Nota1,Nota2,Nota3,Nota4,Promedio,IdCurso,IdEstudianteFk")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso", notas.IdCurso);
            ViewData["IdEstudianteFk"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", notas.IdEstudianteFk);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso", notas.IdCurso);
            ViewData["IdEstudianteFk"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", notas.IdEstudianteFk);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNotas,Nota1,Nota2,Nota3,Nota4,Promedio,IdCurso,IdEstudianteFk")] Notas notas)
        {
            if (id != notas.IdNotas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasExists(notas.IdNotas))
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
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso", notas.IdCurso);
            ViewData["IdEstudianteFk"] = new SelectList(_context.Estudiante, "IdEstudiante", "Nombre", notas.IdEstudianteFk);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas
                .Include(n => n.IdCursoNavigation)
                .Include(n => n.IdEstudianteFkNavigation)
                .FirstOrDefaultAsync(m => m.IdNotas == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notas == null)
            {
                return Problem("Entity set 'SailorMoonContext.Notas'  is null.");
            }
            var notas = await _context.Notas.FindAsync(id);
            if (notas != null)
            {
                _context.Notas.Remove(notas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasExists(int id)
        {
          return (_context.Notas?.Any(e => e.IdNotas == id)).GetValueOrDefault();
        }
    }
}
