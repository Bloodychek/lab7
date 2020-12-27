using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public partial class IncomeAndExpensesOfGsm
    {
        [Key]
        public int IncomeAndExpenseOfGsmid { get; set; }
        public int? NumberOfCapacity { get; set; }
        public int? ContainerId { get; set; }
        public int? IncomeOrExpensePerliter { get; set; }
        public DateTime DateAndTimeOfTheOperationIncomeOrExpense { get; set; }
        public int? StaffId { get; set; }
        public string ResponsibleForTheOperation { get; set; }

        public virtual Container Container { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
