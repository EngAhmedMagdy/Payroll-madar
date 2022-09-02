using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly PayrollContext _context;

        public EmployeesController(PayrollContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var payrollContext = _context.Employees.Include(e => e.Dep).Include(e => e.Job);
            return View(await payrollContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Dep)
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create(decimal? id)
        {
            if (id != null)
            {
                ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName", _context.Departments.FirstOrDefault(x => x.DepId == id));
                ViewData["JobId"] = new SelectList(_context.Jobs.Where(x => x.JobDep == id), "JobId", "JobName", "-- Choose Job --");

            }
            else
            {
                ViewData["DepId"] = new SelectList(_context.Departments,"DepId", "DepName", "-- Choose depart --");
                ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", "-- Choose Job --");
            }
            
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,BirthDate,EmpAddress,EmpPhone,EmpEmail,EmpGrade,DepId,JobId,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName", employee.DepId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", employee.JobId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName", employee.DepId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", employee.JobId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("EmpId,EmpName,BirthDate,EmpAddress,EmpPhone,EmpEmail,EmpGrade,DepId,JobId,HireDate")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
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
            ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepId", employee.DepId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employee.JobId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Dep)
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(decimal id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
