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
    public class MO_PREVENTIVEController : Controller
    {
        private readonly GMAOContext _context;

        public MO_PREVENTIVEController(GMAOContext context)
        {
            _context = context;
        }

        // GET: MO_PREVENTIVE
        public async Task<IActionResult> Index()
        {
            return View(await _context.MO_PREVENTIVE.ToListAsync());
        }

        // GET: MO_PREVENTIVE/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mO_PREVENTIVE = await _context.MO_PREVENTIVE
                .FirstOrDefaultAsync(m => m.CodeMO == id);
            if (mO_PREVENTIVE == null)
            {
                return NotFound();
            }

            return View(mO_PREVENTIVE);
        }

        // GET: MO_PREVENTIVE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MO_PREVENTIVE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodePrev,CodeMO,Categorie,Matricule,Intervenant,TauxHoraire")] MO_PREVENTIVE mO_PREVENTIVE)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mO_PREVENTIVE);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mO_PREVENTIVE);
        }

        // GET: MO_PREVENTIVE/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mO_PREVENTIVE = await _context.MO_PREVENTIVE.FindAsync(id);
            if (mO_PREVENTIVE == null)
            {
                return NotFound();
            }
            return View(mO_PREVENTIVE);
        }

        // POST: MO_PREVENTIVE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodePrev,CodeMO,Categorie,Matricule,Intervenant,TauxHoraire")] MO_PREVENTIVE mO_PREVENTIVE)
        {
            if (id != mO_PREVENTIVE.CodeMO)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mO_PREVENTIVE);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MO_PREVENTIVEExists(mO_PREVENTIVE.CodeMO))
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
            return View(mO_PREVENTIVE);
        }

        // GET: MO_PREVENTIVE/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mO_PREVENTIVE = await _context.MO_PREVENTIVE
                .FirstOrDefaultAsync(m => m.CodeMO == id);
            if (mO_PREVENTIVE == null)
            {
                return NotFound();
            }

            return View(mO_PREVENTIVE);
        }

        // POST: MO_PREVENTIVE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mO_PREVENTIVE = await _context.MO_PREVENTIVE.FindAsync(id);
            _context.MO_PREVENTIVE.Remove(mO_PREVENTIVE);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MO_PREVENTIVEExists(int id)
        {
            return _context.MO_PREVENTIVE.Any(e => e.CodeMO == id);
        }
    }
}
