using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SailorMoon.Context;
using SailorMoon.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SailorMoon.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly SailorMoonContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EstudiantesController(SailorMoonContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHostEnvironment = webHost;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var sailorMoonContext = _context.Estudiante.Include(e => e.IdGradoFkNavigation);
            return View(await sailorMoonContext.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estudiante == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .Include(e => e.IdGradoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["IdGradoFk"] = new SelectList(_context.Grado, "IdGrado", "Grado1");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                string uFileName = UploadedFile(estudiante);
                estudiante.Carnet = uFileName;
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGradoFk"] = new SelectList(_context.Grado, "IdGrado", "Grado1", estudiante.IdGradoFk);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estudiante == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["IdGradoFk"] = new SelectList(_context.Grado, "IdGrado", "Grado1", estudiante.IdGradoFk);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstudiante,Nombre,Apellidos,Dni,Carnet,CodigoEst,IdGradoFk")] Estudiante estudiante)
        {
            if (id != estudiante.IdEstudiante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.IdEstudiante))
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
            ViewData["IdGradoFk"] = new SelectList(_context.Grado, "IdGrado", "Grado1", estudiante.IdGradoFk);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estudiante == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .Include(e => e.IdGradoFkNavigation)
                .FirstOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estudiante == null)
            {
                return Problem("Entity set 'SailorMoonContext.Estudiante'  is null.");
            }
            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiante.Remove(estudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
          return (_context.Estudiante?.Any(e => e.IdEstudiante == id)).GetValueOrDefault();
        }

        private string UploadedFile(Estudiante estudiante)
        {
            string uFileName = null;

            if (estudiante.ImagenFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uFileName = Guid.NewGuid().ToString() + "_" + estudiante.ImagenFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uFileName);
                using (var myFileStream = new FileStream(filePath, FileMode.Create))
                {
                    estudiante.ImagenFile.CopyTo(myFileStream);
                }
            }
            return uFileName;
        }

    }
}
