using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class Bonu
    {
        public decimal BonusId { get; set; }
        public decimal? BonusDep { get; set; }
        public decimal? Bouns { get; set; }
        public decimal? Annual { get; set; }
        public decimal? Discount { get; set; }
        public decimal? AbsentCaseId { get; set; }

        public virtual LkupAbsent AbsentCase { get; set; }
        public virtual Department BonusDepNavigation { get; set; }
    }
}
