using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Appli_gestion_cla.Data;
using Appli_gestion_cla.Models;
using Microsoft.AspNetCore.Authorization;

namespace Appli_gestion_cla.Controllers
{
    
    public class EtudiantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EtudiantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Etudiants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Etudiants.Include(e => e.Classe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Etudiants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiants = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etudiants == null)
            {
                return NotFound();
            }

            return View(etudiants);
        }

        // GET: Etudiants/Create
        public IActionResult Create()
        {
            ViewData["ClasseId"] = new SelectList(_context.Classe, "Id", "Id");
            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,classe,age,ClasseId")] Etudiants etudiants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etudiants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasseId"] = new SelectList(_context.Classe, "Id", "Id", etudiants.ClasseId);
            return View(etudiants);
        }

        // GET: Etudiants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiants = await _context.Etudiants.FindAsync(id);
            if (etudiants == null)
            {
                return NotFound();
            }
            ViewData["ClasseId"] = new SelectList(_context.Classe, "Id", "Id", etudiants.ClasseId);
            return View(etudiants);
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,classe,age,ClasseId")] Etudiants etudiants)
        {
            if (id != etudiants.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etudiants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtudiantsExists(etudiants.Id))
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
            ViewData["ClasseId"] = new SelectList(_context.Classe, "Id", "Id", etudiants.ClasseId);
            return View(etudiants);
        }

        // GET: Etudiants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiants = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etudiants == null)
            {
                return NotFound();
            }

            return View(etudiants);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etudiants = await _context.Etudiants.FindAsync(id);
            if (etudiants != null)
            {
                _context.Etudiants.Remove(etudiants);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtudiantsExists(int id)
        {
            return _context.Etudiants.Any(e => e.Id == id);
        }
    }
}
