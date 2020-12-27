using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public partial class Staff
    {
        public Staff()
        {
            IncomeAndExpensesOfGsm = new HashSet<IncomeAndExpensesOfGsm>();
        }
        [Key]
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public int? StaffAge { get; set; }
        public string StaffFunction { get; set; }
        public int WorkingHoursForAweek { get; set; }

        public virtual ICollection<IncomeAndExpensesOfGsm> IncomeAndExpensesOfGsm { get; set; }
    }
}
