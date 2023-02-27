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
    public class PreventiveEquipementsController : Controller
    {
        private readonly GMAOContext _context;

        public PreventiveEquipementsController(GMAOContext context)
        {
            _context = context;
        }

        // GET: PreventiveEquipements
        public async Task<IActionResult> Index()
        {
            return View(await _context.PreventiveEquipement.ToListAsync());
        }

        // GET: PreventiveEquipements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventiveEquipement = await _context.PreventiveEquipement
                .FirstOrDefaultAsync(m => m.CodePrev == id);
            if (preventiveEquipement == null)
            {
                return NotFound();
            }

            return View(preventiveEquipement);
        }

        // GET: PreventiveEquipements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PreventiveEquipements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodePrev,CodeEquipement,CompteurEquipement,Condition,DateDernierExecution,ValeurDernierExecution,DateProchaineexecution,ValeurProchianeExecution,DateCreation,Statut,DateActivite,DateInactivite")] PreventiveEquipement preventiveEquipement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preventiveEquipement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preventiveEquipement);
        }

        // GET: PreventiveEquipements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventiveEquipement = await _context.PreventiveEquipement.FindAsync(id);
            if (preventiveEquipement == null)
            {
                return NotFound();
            }
            return View(preventiveEquipement);
        }

        // POST: PreventiveEquipements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodePrev,CodeEquipement,CompteurEquipement,Condition,DateDernierExecution,ValeurDernierExecution,DateProchaineexecution,ValeurProchianeExecution,DateCreation,Statut,DateActivite,DateInactivite")] PreventiveEquipement preventiveEquipement)
        {
            if (id != preventiveEquipement.CodePrev)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preventiveEquipement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreventiveEquipementExists(preventiveEquipement.CodePrev))
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
            return View(preventiveEquipement);
        }

        // GET: PreventiveEquipements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventiveEquipement = await _context.PreventiveEquipement
                .FirstOrDefaultAsync(m => m.CodePrev == id);
            if (preventiveEquipement == null)
            {
                return NotFound();
            }

            return View(preventiveEquipement);
        }

        // POST: PreventiveEquipements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preventiveEquipement = await _context.PreventiveEquipement.FindAsync(id);
            _context.PreventiveEquipement.Remove(preventiveEquipement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreventiveEquipementExists(int id)
        {
            return _context.PreventiveEquipement.Any(e => e.CodePrev == id);
        }
    }
}
