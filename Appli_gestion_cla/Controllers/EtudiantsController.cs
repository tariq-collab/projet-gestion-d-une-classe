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
            var etudiant = await _context.Etudiants.Include(e => e.Classe).ToListAsync();
            return View(etudiant);
        }

        // GET: Etudiants/Create
        public IActionResult Create()
        {
            ViewBag.ClasseId = new SelectList(_context.Classes, "Id", "Nom_sale");
            return View();
        }

        // POST: Etudiants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                _context.Etudiants.Add(etudiant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClasseId = new SelectList(_context.Classes, "Id", "Nom_sale", etudiant.ClasseId);
            return View(etudiant);
        }

        // GET: Etudiants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null) return NotFound();

            ViewBag.ClasseId = new SelectList(_context.Classes, "Id", "Nom_sale", etudiant.ClasseId);
            return View(etudiant);
        }

        // POST: Etudiants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Etudiant etudiant)
        {
            if (id != etudiant.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(etudiant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClasseId = new SelectList(_context.Classes, "Id", "Nom_sale", etudiant.ClasseId);
            return View(etudiant);
        }

        // GET: Etudiants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(m => m.Id == id);

            if (etudiant == null) return NotFound();

            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant != null)
            {
                _context.Etudiants.Remove(etudiant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }


}
