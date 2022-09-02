using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Models;
using Payroll.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class ReportsController : Controller
    {
        private readonly PayrollContext _context;

        public ReportsController(PayrollContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public IActionResult EmployeeReport()
        {
            var employees = _context.Employees.Include(x=>x.Dep).Include(x=>x.Job);
            List<EmployeeReport> employeeReport = new();
            foreach (var em in employees)
            {
                employeeReport.Add(new EmployeeReport()
                {
                    EMP_NAME = em.EmpName,
                    EMP_ADDRESS = em.EmpAddress,
                    EMP_EMAIL = em.EmpEmail,
                    EMP_PHONE = em.EmpPhone,
                    DEP_ID = em.Dep?.DepName,
                    JOB_ID = em.Job?.JobName,
                    HIRE_DATE = em.HireDate
                });
            }
            var reportFileByteString = GenerateReportAsync("DataSet1",employeeReport,"EmployeeReport");
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName("EmployeeReport", "pdf"));
        }
        public IActionResult AttendanceReport()
        {
            var attendances = _context.Attendances.Include(a => a.Emp).Include(x=>x.AbsentCase);
            List<Payroll.Report.AttendancesReport> attendancesReport = new();
            foreach (var attend in attendances)
            {
                attendancesReport.Add(new Payroll.Report.AttendancesReport()
                {
                    EMP_ID = attend.Emp?.EmpName,
                    ABSENT_CASE_ID = attend.AbsentCase?.DaysNumber,
                    ABSENT_DATE = attend.AbsentDate,
                    ABSENT_REASON = attend.AbsentReason,
                    ATTEND_DATE = attend.AttendDate,
                    LEAVE_DATE = attend.LeaveDate
                });
            }
            var reportFileByteString = GenerateReportAsync("DataSet2",attendancesReport, "AttendancesReport");
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName("AttendancesReport", "pdf"));
        }
        public IActionResult SalaryBonusDiscountReport()
        {
            var payrolls = _context.PayrollReports
                .Include(x => x.Emp)
                .Include(x => x.Job)
                .Include(x => x.Salary)
                .Include(x=>x.Attendance).ThenInclude(x=>x.AbsentCase);
            List<Payroll.Report.SalaryBonusDiscountReport> employeeReport = new();
            if(payrolls.Any())
            {
                foreach (var payroll in payrolls)
                {
                    employeeReport.Add(new Payroll.Report.SalaryBonusDiscountReport()
                    {
                        EMP_ID = payroll.Emp?.EmpName,
                        ATTENDANCE_ID = payroll.Attendance?.AbsentCase?.DaysNumber,
                        PAYROLL_DATE = payroll.PayrollDate,
                        Job_ID = payroll.Job?.JobName,
                        SALARY_ID = payroll.Salary?.Amount ?? 0,
                        TOTAL_AMOUNT = payroll.TotalAmount ?? 0,

                    });
                }
            }
           
            var reportFileByteString = GenerateReportAsync("DataSet3", employeeReport, "SalaryBonusDiscountReport");
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName("SalaryBonusDiscountReport", "pdf"));
        }
        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLS":
                    outputFileName = reportName + ".xls";
                    break;
                case "WORD":
                    outputFileName = reportName + ".doc";
                    break;
            }

            return outputFileName;
        }
        public byte[] GenerateReportAsync(string dataSet,object dataSource,string url)
        {

            string test = $"C:\\Users\\Lenovo\\source\\repos\\Payroll\\Payroll\\Report\\{url}.rdlc";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");
            LocalReport report = new LocalReport(test);
           
            report.AddDataSource(dataSet, dataSource);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var result = report.Execute(RenderType.Pdf, 1, parameters);

            return result.MainStream;
        }


    }
}
