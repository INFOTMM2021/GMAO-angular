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
    public class DemPreventivesController : Controller
    {
        private readonly GMAOContext _context;

        public DemPreventivesController(GMAOContext context)
        {
            _context = context;
        }

        // GET: DemPreventives
        public async Task<IActionResult> Index()
        {
            return View(await _context.DemPreventive.ToListAsync());
        }

        // GET: DemPreventives/Details/5
        public async Task<IActionResult> Details(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demPreventive = await _context.DemPreventive
                .FirstOrDefaultAsync(m => m.NumDem == id);
            if (demPreventive == null)
            {
                return NotFound();
            }

            return View(demPreventive);
        }

        // GET: DemPreventives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DemPreventives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumDem,CCRacine,CodeEquipement,Designation")] DemPreventive demPreventive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demPreventive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(demPreventive);
        }

        // GET: DemPreventives/Edit/5
        public async Task<IActionResult> Edit(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demPreventive = await _context.DemPreventive.FindAsync(id);
            if (demPreventive == null)
            {
                return NotFound();
            }
            return View(demPreventive);
        }

        // POST: DemPreventives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float id, [Bind("NumDem,CCRacine,CodeEquipement,Designation")] DemPreventive demPreventive)
        {
            if (id != demPreventive.NumDem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demPreventive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemPreventiveExists(demPreventive.NumDem))
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
            return View(demPreventive);
        }

        // GET: DemPreventives/Delete/5
        public async Task<IActionResult> Delete(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demPreventive = await _context.DemPreventive
                .FirstOrDefaultAsync(m => m.NumDem == id);
            if (demPreventive == null)
            {
                return NotFound();
            }

            return View(demPreventive);
        }

        // POST: DemPreventives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(float id)
        {
            var demPreventive = await _context.DemPreventive.FindAsync(id);
            _context.DemPreventive.Remove(demPreventive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemPreventiveExists(float id)
        {
            return _context.DemPreventive.Any(e => e.NumDem == id);
        }
    }
}
