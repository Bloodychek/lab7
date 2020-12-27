using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public partial class Costs
    {
        [Key]
        public int CostId { get; set; }
        public int? TypeOfGsmid { get; set; }
        public double PricePerLiter { get; set; }
        public DateTime DateOfCostGsm { get; set; }

        public virtual Gsm TypeOfGsm { get; set; }
    }
}
