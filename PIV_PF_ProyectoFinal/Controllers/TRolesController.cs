using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIV_PF_PROYECTOFINAL.Models;

namespace PIV_PF_PROYECTOFINAL.Controllers
{
    public class TRolesController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TRolesController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // GET: TRoles
        public async Task<IActionResult> Index()
        {
              return _context.TRole != null ? 
                          View(await _context.TRole.ToListAsync()) :
                          Problem("Entity set 'FARMACIA_PROGRA4Context.TRole'  is null.");
        }

        // GET: TRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TRole == null)
            {
                return NotFound();
            }

            var tRole = await _context.TRole
                .FirstOrDefaultAsync(m => m.IdRole == id);
            if (tRole == null)
            {
                return NotFound();
            }

            return View(tRole);
        }

        // GET: TRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRole,Nombre")] TRole tRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tRole);
        }

        // GET: TRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TRole == null)
            {
                return NotFound();
            }

            var tRole = await _context.TRole.FindAsync(id);
            if (tRole == null)
            {
                return NotFound();
            }
            return View(tRole);
        }

        // POST: TRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRole,Nombre")] TRole tRole)
        {
            if (id != tRole.IdRole)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TRoleExists(tRole.IdRole))
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
            return View(tRole);
        }

        // GET: TRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TRole == null)
            {
                return NotFound();
            }

            var tRole = await _context.TRole
                .FirstOrDefaultAsync(m => m.IdRole == id);
            if (tRole == null)
            {
                return NotFound();
            }

            return View(tRole);
        }

        // POST: TRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TRole == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TRole'  is null.");
            }
            var tRole = await _context.TRole.FindAsync(id);
            if (tRole != null)
            {
                _context.TRole.Remove(tRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TRoleExists(int id)
        {
          return (_context.TRole?.Any(e => e.IdRole == id)).GetValueOrDefault();
        }
    }
}
