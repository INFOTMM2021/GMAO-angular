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
    public class NaturePiecesController : Controller
    {
        private readonly GMAOContext _context;

        public NaturePiecesController(GMAOContext context)
        {
            _context = context;
        }

        // GET: NaturePieces
        public async Task<IActionResult> Index()
        {
            return View(await _context.NaturePiece.ToListAsync());
        }

        // GET: NaturePieces/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naturePiece = await _context.NaturePiece
                .FirstOrDefaultAsync(m => m.NatPiece == id);
            if (naturePiece == null)
            {
                return NotFound();
            }

            return View(naturePiece);
        }

        // GET: NaturePieces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NaturePieces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NatPiece")] NaturePiece naturePiece)
        {
            if (ModelState.IsValid)
            {
                _context.Add(naturePiece);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(naturePiece);
        }

        // GET: NaturePieces/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naturePiece = await _context.NaturePiece.FindAsync(id);
            if (naturePiece == null)
            {
                return NotFound();
            }
            return View(naturePiece);
        }

        // POST: NaturePieces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NatPiece")] NaturePiece naturePiece)
        {
            if (id != naturePiece.NatPiece)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naturePiece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaturePieceExists(naturePiece.NatPiece))
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
            return View(naturePiece);
        }

        // GET: NaturePieces/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naturePiece = await _context.NaturePiece
                .FirstOrDefaultAsync(m => m.NatPiece == id);
            if (naturePiece == null)
            {
                return NotFound();
            }

            return View(naturePiece);
        }

        // POST: NaturePieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var naturePiece = await _context.NaturePiece.FindAsync(id);
            _context.NaturePiece.Remove(naturePiece);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaturePieceExists(string id)
        {
            return _context.NaturePiece.Any(e => e.NatPiece == id);
        }
    }
}
