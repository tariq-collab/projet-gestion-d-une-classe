using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Appli_gestion_cla.Data;
using Appli_gestion_cla.Models;

namespace Appli_gestion_cla.Controllers
{
    public class AffectationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AffectationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Affectations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Affectations.Include(a => a.Classe).Include(a => a.Enseignant).Include(a => a.Matiere);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Affectations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectation = await _context.Affectations
                .Include(a => a.Classe)
                .Include(a => a.Enseignant)
                .Include(a => a.Matiere)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (affectation == null)
            {
                return NotFound();
            }

            return View(affectation);
        }

        // GET: Affectations/Create
        public IActionResult Create()
        {
            ViewData["ClasseId"] = new SelectList(_context.Classes, "Id", "Nom_sale");
            ViewData["EnseignantId"] = new SelectList(_context.Enseignants, "Id", "Nom");
            ViewData["MatiereId"] = new SelectList(_context.Matieres, "Id", "Nom");
            return View();
        }

        // POST: Affectations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EnseignantId,MatiereId,ClasseId")] Affectation affectation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(affectation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasseId"] = new SelectList(_context.Classes, "Id", "Nom_sale", affectation.ClasseId);
            ViewData["EnseignantId"] = new SelectList(_context.Enseignants, "Id", "nom", affectation.EnseignantId);
            ViewData["MatiereId"] = new SelectList(_context.Matieres, "Id", "nom", affectation.MatiereId);
            return View(affectation);
        }

        // GET: Affectations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectation = await _context.Affectations.FindAsync(id);
            if (affectation == null)
            {
                return NotFound();
            }
            ViewData["ClasseId"] = new SelectList(_context.Classes, "Id", "nom_salle", affectation.ClasseId);
            ViewData["EnseignantId"] = new SelectList(_context.Enseignants, "Id", "Nom", affectation.EnseignantId);
            ViewData["MatiereId"] = new SelectList(_context.Matieres, "Id", "Nom", affectation.MatiereId);
            return View(affectation);
        }

        // POST: Affectations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnseignantId,MatiereId,ClasseId")] Affectation affectation)
        {
            if (id != affectation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(affectation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AffectationExists(affectation.Id))
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
            ViewData["ClasseId"] = new SelectList(_context.Classes, "Id", "Nom_salle", affectation.ClasseId);
            ViewData["EnseignantId"] = new SelectList(_context.Enseignants, "Id", "Nom", affectation.EnseignantId);
            ViewData["MatiereId"] = new SelectList(_context.Matieres, "Id", "Nom", affectation.MatiereId);
            return View(affectation);
        }

        // GET: Affectations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectation = await _context.Affectations
                .Include(a => a.Classe)
                .Include(a => a.Enseignant)
                .Include(a => a.Matiere)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (affectation == null)
            {
                return NotFound();
            }

            return View(affectation);
        }

        // POST: Affectations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var affectation = await _context.Affectations.FindAsync(id);
            if (affectation != null)
            {
                _context.Affectations.Remove(affectation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AffectationExists(int id)
        {
            return _context.Affectations.Any(e => e.Id == id);
        }
    }
}
