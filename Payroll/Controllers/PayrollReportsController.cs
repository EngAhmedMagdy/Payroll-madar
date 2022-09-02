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
    public class PayrollReportsController : Controller
    {
        private readonly PayrollContext _context;

        public PayrollReportsController(PayrollContext context)
        {
            _context = context;
        }

        // GET: PayrollReports
        public async Task<IActionResult> Index()
        {
            var payrollContext = _context.PayrollReports
                .Include(p => p.Attendance)
                .ThenInclude(x=>x.AbsentCase)
                .Include(p => p.Emp).Include(p => p.Job).Include(p => p.Salary);
            return View(await payrollContext.ToListAsync());
        }

        // GET: PayrollReports/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollReport = await _context.PayrollReports
                .Include(p => p.Attendance)
                .Include(p => p.Emp)
                .Include(p => p.Job)
                .Include(p => p.Salary)
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payrollReport == null)
            {
                return NotFound();
            }

            return View(payrollReport);
        }

        // GET: PayrollReports/Create
        public IActionResult Create(decimal? id )
        {
            if(id == null)
            {
                var emps = _context.Employees.ToList();
                emps.Add(new Employee { EmpId = -1, EmpName = "--Select--" });
                ViewData["AttendanceId"] = new SelectList(_context.Attendances.Include(x => x.AbsentCase), "AttendanceId", "AbsentCase.DaysNumber");
                ViewData["EmpId"] = new SelectList(emps, "EmpId", "EmpName",-1);
                ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName");
                ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "Amount");
            }
            else
            {
                var loadEmployee = _context.Employees.Where(p =>p.EmpId == id)
                    .Include(x=>x.Attendances).ThenInclude(x=>x.AbsentCase)
                    .Include(x => x.Job).ThenInclude(x => x.Salaries)
                    .FirstOrDefault();
                ViewData["AttendanceId"] = new SelectList(loadEmployee.Attendances, "AttendanceId", "AbsentCase.DaysNumber");
                ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", loadEmployee.EmpId);
                ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", loadEmployee.Job.JobId);
                ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "Amount", loadEmployee.Job.Salaries.FirstOrDefault().SalaryId);
               
            }
            
            return View();
        }

        // POST: PayrollReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayrollId,EmpId,JobId,SalaryId,AttendanceId,PayrollDate,TotalAmount")] PayrollReport payrollReport)
        {
            if (ModelState.IsValid)
            {
                payrollReport.TotalAmount = CalculateTotalAmount(payrollReport);
                _context.Add(payrollReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttendanceId"] = new SelectList(_context.Attendances.Include(x => x.AbsentCase), "AttendanceId", "AbsentCase.DaysNumber", payrollReport.AttendanceId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", payrollReport.EmpId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", payrollReport.JobId);
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "Amount", payrollReport.SalaryId);
            return View(payrollReport);
        }

        // GET: PayrollReports/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollReport = await _context.PayrollReports.FindAsync(id);
            if (payrollReport == null)
            {
                return NotFound();
            }
            ViewData["AttendanceId"] = new SelectList(_context.Attendances, "AttendanceId", "AttendanceId", payrollReport.AttendanceId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", payrollReport.EmpId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", payrollReport.JobId);
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "Amount", payrollReport.SalaryId);
            return View(payrollReport);
        }

        // POST: PayrollReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PayrollId,EmpId,JobId,SalaryId,AttendanceId,PayrollDate,TotalAmount")] PayrollReport payrollReport)
        {
            if (id != payrollReport.PayrollId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payrollReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollReportExists(payrollReport.PayrollId))
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
            ViewData["AttendanceId"] = new SelectList(_context.Attendances, "AttendanceId", "AttendanceId", payrollReport.AttendanceId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpName", payrollReport.EmpId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobName", payrollReport.JobId);
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "Amount", payrollReport.SalaryId);
            return View(payrollReport);
        }

        // GET: PayrollReports/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollReport = await _context.PayrollReports
                .Include(p => p.Attendance)
                .Include(p => p.Emp)
                .Include(p => p.Job)
                .Include(p => p.Salary)
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payrollReport == null)
            {
                return NotFound();
            }

            return View(payrollReport);
        }

        // POST: PayrollReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var payrollReport = await _context.PayrollReports.FindAsync(id);
            _context.PayrollReports.Remove(payrollReport);
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

        private bool PayrollReportExists(decimal id)
        {
            return _context.PayrollReports.Any(e => e.PayrollId == id);
        }
        private decimal CalculateTotalAmount(PayrollReport payrollReport)
        {
            decimal totalAmount = 0;
            //init with gross salary
            var gross = _context.Salaries.FirstOrDefault(x=>x.SalaryId == payrollReport.SalaryId)?.Amount ?? 0;
            //emp attendances
            var attendance = _context.Attendances.FirstOrDefault(x=>x.AttendanceId == payrollReport.AttendanceId);
            //department details with bonus/discount
            var Dep_job = _context.Jobs.Where(x => x.JobId == payrollReport.JobId)
                                    .FirstOrDefault();
            //bonus 
            
            var bonusPercentage = _context.Bonus.FirstOrDefault(x =>x.BonusDep == Dep_job.JobDep && x.AbsentCaseId == attendance.AbsentCaseId)?
                                    .Bouns??0;
            //annual bonus
            var annualBonusPercentage = _context.Bonus.FirstOrDefault(x => x.BonusDep == Dep_job.JobDep && x.AbsentCaseId == attendance.AbsentCaseId)?
                                    .Annual ?? 0;
            var hireTime = _context.Employees.FirstOrDefault(x => x.EmpId == payrollReport.EmpId).HireDate ?? DateTime.Now;
            TimeSpan span = DateTime.Now - hireTime;
            DateTime zeroTime = new DateTime(1, 1, 1);
            var numberOfYers = (zeroTime + span).Year - 1;
            //discount
            var discountPercentage = _context.Bonus.FirstOrDefault(x => x.BonusDep == Dep_job.JobDep && x.AbsentCaseId == attendance.AbsentCaseId)?
                                    .Discount??0;
            //add the gross
            totalAmount += gross;
            //add the bonus
            totalAmount += ((bonusPercentage / 100) * gross);
            //add the annual bonus
            totalAmount += ((annualBonusPercentage / 100) * gross) * numberOfYers;
            //subtruct the discount
            totalAmount -= (discountPercentage / 100) * gross;
            return totalAmount;
        }
    }
}
