using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class Job
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
            PayrollReports = new HashSet<PayrollReport>();
            Salaries = new HashSet<Salary>();
        }

        public decimal JobId { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public decimal? JobDep { get; set; }
        public string SalaryRange { get; set; }

        public virtual Department JobDepNavigation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PayrollReport> PayrollReports { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
