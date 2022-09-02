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
    public class AttendancesController : Controller
    {
        private readonly PayrollContext _context;

        public AttendancesController(PayrollContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var payrollContext = _context.Attendances.Include(a => a.AbsentCase).Include(a => a.Emp);
            return View(await payrollContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.AbsentCase)
                .Include(a => a.Emp)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber");
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,EmpId,LeaveDate,AttendDate,AbsentDate,FAbsent,AbsentReason,AbsentCaseId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber", attendance.AbsentCaseId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", attendance.EmpId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber", attendance.AbsentCaseId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", attendance.EmpId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AttendanceId,EmpId,LeaveDate,AttendDate,AbsentDate,FAbsent,AbsentReason,AbsentCaseId")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            ViewData["AbsentCaseId"] = new SelectList(_context.LkupAbsents, "AbsentId", "DaysNumber", attendance.AbsentCaseId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", attendance.EmpId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.AbsentCase)
                .Include(a => a.Emp)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(decimal id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
