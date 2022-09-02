using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class Salary
    {
        public Salary()
        {
            PayrollReports = new HashSet<PayrollReport>();
        }

        public decimal SalaryId { get; set; }
        public decimal? JobId { get; set; }
        public decimal? Amount { get; set; }

        public virtual Job Job { get; set; }
        public virtual ICollection<PayrollReport> PayrollReports { get; set; }
    }
}
