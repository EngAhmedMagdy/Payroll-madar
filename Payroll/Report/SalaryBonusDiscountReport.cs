using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Report
{
    public class SalaryBonusDiscountReport
    {
        public string EMP_ID { get; set; }
        public string Job_ID { get; set; }
        public decimal SALARY_ID { get; set; }
        public string ATTENDANCE_ID { get; set; }
        public DateTime? PAYROLL_DATE { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
    }
}
