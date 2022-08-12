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
    public class PersonalsController : Controller
    {
        private readonly SailorMoonContext _context;

        public PersonalsController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: Personals
        public async Task<IActionResult> Index()
        {
            var sailorMoonContext = _context.Personal.Include(p => p.IdCursoNavigation).Include(p => p.IdTipoUsuarioNavigation);
            return View(await sailorMoonContext.ToListAsync());
        }

        // GET: Personals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personal == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal
                .Include(p => p.IdCursoNavigation)
                .Include(p => p.IdTipoUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdPersonal == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // GET: Personals/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso");
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "Tipo");
            return View();
        }

        // POST: Personals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersonal,Nombre,Apellidos,Usuario,Clave,IdTipoUsuario,IdCurso")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso", personal.IdCurso);
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "Tipo", personal.IdTipoUsuario);
            return View(personal);
        }

        // GET: Personals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personal == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso", personal.IdCurso);
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "Tipo", personal.IdTipoUsuario);
            return View(personal);
        }

        // POST: Personals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersonal,Nombre,Apellidos,Usuario,Clave,IdTipoUsuario,IdCurso")] Personal personal)
        {
            if (id != personal.IdPersonal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalExists(personal.IdPersonal))
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
            ViewData["IdCurso"] = new SelectList(_context.Curso, "IdCurso", "NombreCurso", personal.IdCurso);
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "Tipo", personal.IdTipoUsuario);
            return View(personal);
        }

        // GET: Personals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personal == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal
                .Include(p => p.IdCursoNavigation)
                .Include(p => p.IdTipoUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdPersonal == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // POST: Personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personal == null)
            {
                return Problem("Entity set 'SailorMoonContext.Personal'  is null.");
            }
            var personal = await _context.Personal.FindAsync(id);
            if (personal != null)
            {
                _context.Personal.Remove(personal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalExists(int id)
        {
          return (_context.Personal?.Any(e => e.IdPersonal == id)).GetValueOrDefault();
        }
    }
}
