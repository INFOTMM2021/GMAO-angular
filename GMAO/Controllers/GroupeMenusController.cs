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
    public class GroupeMenusController : Controller
    {
        private readonly GMAOContext _context;

        public GroupeMenusController(GMAOContext context)
        {
            _context = context;
        }

        // GET: GroupeMenus
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupeMenu.ToListAsync());
        }

        // GET: GroupeMenus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupeMenu = await _context.GroupeMenu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupeMenu == null)
            {
                return NotFound();
            }

            return View(groupeMenu);
        }

        // GET: GroupeMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupeMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupeId,MenuId,authorisation")] GroupeMenu groupeMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupeMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupeMenu);
        }

        // GET: GroupeMenus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupeMenu = await _context.GroupeMenu.FindAsync(id);
            if (groupeMenu == null)
            {
                return NotFound();
            }
            return View(groupeMenu);
        }

        // POST: GroupeMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,GroupeId,MenuId,authorisation")] GroupeMenu groupeMenu)
        {
            if (id != groupeMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupeMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupeMenuExists(groupeMenu.Id))
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
            return View(groupeMenu);
        }

        // GET: GroupeMenus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupeMenu = await _context.GroupeMenu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupeMenu == null)
            {
                return NotFound();
            }

            return View(groupeMenu);
        }

        // POST: GroupeMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var groupeMenu = await _context.GroupeMenu.FindAsync(id);
            _context.GroupeMenu.Remove(groupeMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupeMenuExists(string id)
        {
            return _context.GroupeMenu.Any(e => e.Id == id);
        }
    }
}
