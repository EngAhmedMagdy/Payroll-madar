using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
            PayrollReports = new HashSet<PayrollReport>();
        }

        public decimal EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string EmpAddress { get; set; }
        public string EmpPhone { get; set; }
        public string EmpEmail { get; set; }
        public string EmpGrade { get; set; }
        public decimal? DepId { get; set; }
        public decimal? JobId { get; set; }
        public DateTime? HireDate { get; set; }

        public virtual Department Dep { get; set; }
        public virtual Job Job { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<PayrollReport> PayrollReports { get; set; }
    }
}
