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
    public class TipoUsuariosController : Controller
    {
        private readonly SailorMoonContext _context;

        public TipoUsuariosController(SailorMoonContext context)
        {
            _context = context;
        }

        // GET: TipoUsuarios
        public async Task<IActionResult> Index()
        {
              return _context.TipoUsuario != null ? 
                          View(await _context.TipoUsuario.ToListAsync()) :
                          Problem("Entity set 'SailorMoonContext.TipoUsuario'  is null.");
        }

        // GET: TipoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoUsuario == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.TipoUsuario
                .FirstOrDefaultAsync(m => m.IdTipoUsuario == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoUsuario,Tipo")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoUsuario == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.TipoUsuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }
            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoUsuario,Tipo")] TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.IdTipoUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUsuarioExists(tipoUsuario.IdTipoUsuario))
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
            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoUsuario == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.TipoUsuario
                .FirstOrDefaultAsync(m => m.IdTipoUsuario == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoUsuario == null)
            {
                return Problem("Entity set 'SailorMoonContext.TipoUsuario'  is null.");
            }
            var tipoUsuario = await _context.TipoUsuario.FindAsync(id);
            if (tipoUsuario != null)
            {
                _context.TipoUsuario.Remove(tipoUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoUsuarioExists(int id)
        {
          return (_context.TipoUsuario?.Any(e => e.IdTipoUsuario == id)).GetValueOrDefault();
        }
    }
}
