using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Report
{
    public class EmployeeReport
    {
        public string EMP_NAME { get; set; }
        public string EMP_ADDRESS { get; set; }
        public string EMP_PHONE { get; set; }
        public string EMP_EMAIL { get; set; }
        public string DEP_ID { get; set; }
        public string JOB_ID  { get; set; }
        public DateTime? HIRE_DATE { get; set; }

    }
}
