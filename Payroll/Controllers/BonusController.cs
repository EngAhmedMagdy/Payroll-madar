using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class BonusController : Controller
    {
        private readonly PayrollContext _context;

        public BonusController(PayrollContext context)
        {
            _context = context;
        }

        // GET: Bonus
        public async Task<IActionResult> Index()
        {
            var payrollContext = _context.Bonus.Include(b => b.AbsentCase).Include(b => b.BonusDepNavigation);
            return View(await payrollContext.ToListAsync());
        }

        // GET: Bonus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonu = await _context.Bonus
                .Include(b => b.AbsentCase)
                .Include(b => b.BonusDepNavigation)
                .FirstOrDefaultAsync(m => m.BonusId == id);
            if (bonu == null)
            {
                return NotFound();
            }

            return View(bonu);
        }

        // GET: Bonus/Create
        public IActionResult Create()
        {
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber");
            ViewData["BonusDep"] = new SelectList(_context.Departments, "DepId", "DepName");
            return View();
        }

        // POST: Bonus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BonusId,BonusDep,Bouns,Annual,Discount,AbsentCaseId")] Bonu bonu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bonu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber", bonu.AbsentCaseId);
            ViewData["BonusDep"] = new SelectList(_context.Departments, "DepId", "DepName", bonu.BonusDep);
            return View(bonu);
        }

        // GET: Bonus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonu = await _context.Bonus.FindAsync(id);
            if (bonu == null)
            {
                return NotFound();
            }
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber", bonu.AbsentCaseId);
            ViewData["BonusDep"] = new SelectList(_context.Departments, "DepId", "DepName", bonu.BonusDep);
            return View(bonu);
        }

        // POST: Bonus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("BonusId,BonusDep,Bouns,Annual,Discount,AbsentCaseId")] Bonu bonu)
        {
            if (id != bonu.BonusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bonu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BonuExists(bonu.BonusId))
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
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber", bonu.AbsentCaseId);
            ViewData["BonusDep"] = new SelectList(_context.Departments, "DepId", "DepName", bonu.BonusDep);
            return View(bonu);
        }

        // GET: Bonus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonu = await _context.Bonus
                .Include(b => b.AbsentCase)
                .Include(b => b.BonusDepNavigation)
                .FirstOrDefaultAsync(m => m.BonusId == id);
            if (bonu == null)
            {
                return NotFound();
            }

            return View(bonu);
        }

        // POST: Bonus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var bonu = await _context.Bonus.FindAsync(id);
            _context.Bonus.Remove(bonu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BonuExists(decimal id)
        {
            return _context.Bonus.Any(e => e.BonusId == id);
        }
    }
}
