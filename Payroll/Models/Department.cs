using System;
using System.Collections.Generic;

#nullable disable

namespace Payroll.Models
{
    public partial class Department
    {
        public Department()
        {
            Bonus = new HashSet<Bonu>();
            Employees = new HashSet<Employee>();
            Jobs = new HashSet<Job>();
        }

        public decimal DepId { get; set; }
        public string DepName { get; set; }
        public string DepDescription { get; set; }

        public virtual ICollection<Bonu> Bonus { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
