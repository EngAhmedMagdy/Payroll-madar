using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class LkupAbsent
    {
        public LkupAbsent()
        {
            Attendances = new HashSet<Attendance>();
            Bonus = new HashSet<Bonu>();
        }

        public decimal AbsentId { get; set; }
        public string DaysNumber { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Bonu> Bonus { get; set; }
    }
}
