using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Report
{
    public class AttendancesReport
    {
        public string EMP_ID { get; set; }
        public DateTime? LEAVE_DATE { get; set; }
        public DateTime? ATTEND_DATE { get; set; }
        public DateTime? ABSENT_DATE { get; set; }
        public string ABSENT_REASON { get; set; }
        public string ABSENT_CASE_ID { get; set; }
    }
}
