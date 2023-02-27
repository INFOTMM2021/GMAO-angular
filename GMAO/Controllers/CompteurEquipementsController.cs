using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMAO.Data;
using GMAO.Models;

namespace GMAO.Controllers
{
    public class CompteurEquipementsController : Controller
    {
        private readonly GMAOContext _context;

        public CompteurEquipementsController(GMAOContext context)
        {
            _context = context;
        }

        // GET: CompteurEquipements
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompteurEquipement.ToListAsync());
        }

        // GET: CompteurEquipements/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compteurEquipement = await _context.CompteurEquipement
                .FirstOrDefaultAsync(m => m.CodeCompteur == id);
            if (compteurEquipement == null)
            {
                return NotFound();
            }

            return View(compteurEquipement);
        }

        // GET: CompteurEquipements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompteurEquipements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeCompteur,Description,TypeCompteur")] CompteurEquipement compteurEquipement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compteurEquipement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compteurEquipement);
        }

        // GET: CompteurEquipements/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compteurEquipement = await _context.CompteurEquipement.FindAsync(id);
            if (compteurEquipement == null)
            {
                return NotFound();
            }
            return View(compteurEquipement);
        }

        // POST: CompteurEquipements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeCompteur,Description,TypeCompteur")] CompteurEquipement compteurEquipement)
        {
            if (id != compteurEquipement.CodeCompteur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compteurEquipement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompteurEquipementExists(compteurEquipement.CodeCompteur))
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
            return View(compteurEquipement);
        }

        // GET: CompteurEquipements/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compteurEquipement = await _context.CompteurEquipement
                .FirstOrDefaultAsync(m => m.CodeCompteur == id);
            if (compteurEquipement == null)
            {
                return NotFound();
            }

            return View(compteurEquipement);
        }

        // POST: CompteurEquipements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var compteurEquipement = await _context.CompteurEquipement.FindAsync(id);
            _context.CompteurEquipement.Remove(compteurEquipement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompteurEquipementExists(string id)
        {
            return _context.CompteurEquipement.Any(e => e.CodeCompteur == id);
        }
    }
}
