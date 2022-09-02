using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class PayrollReport
    {
        public decimal PayrollId { get; set; }
        public decimal? EmpId { get; set; }
        public decimal? JobId { get; set; }
        public decimal? SalaryId { get; set; }
        public decimal? AttendanceId { get; set; }
        public DateTime? PayrollDate { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual Attendance Attendance { get; set; }
        public virtual Employee Emp { get; set; }
        public virtual Job Job { get; set; }
        public virtual Salary Salary { get; set; }
    }
}
