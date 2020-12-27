using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public partial class Container
    {
        public Container()
        {
            IncomeAndExpensesOfGsm = new HashSet<IncomeAndExpensesOfGsm>();
        }
        [Key]
        public int ContainerId { get; set; }
        public int? Number { get; set; }
        public double? TankCapacity { get; set; }
        public int? TypeOfGsmid { get; set; }

        public virtual Gsm TypeOfGsm { get; set; }
        public virtual ICollection<IncomeAndExpensesOfGsm> IncomeAndExpensesOfGsm { get; set; }
    }
}
