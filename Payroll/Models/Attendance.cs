using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class Attendance
    {
        public Attendance()
        {
            PayrollReports = new HashSet<PayrollReport>();
        }

        public decimal AttendanceId { get; set; }
        public decimal? EmpId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? AttendDate { get; set; }
        public DateTime? AbsentDate { get; set; }
        public string AbsentReason { get; set; }
        public decimal? AbsentCaseId { get; set; }

        public virtual LkupAbsent AbsentCase { get; set; }
        public virtual Employee Emp { get; set; }
        public virtual ICollection<PayrollReport> PayrollReports { get; set; }
    }
}
