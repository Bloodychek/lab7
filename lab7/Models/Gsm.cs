using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public partial class Gsm
    {
        public Gsm()
        {
            Containers = new HashSet<Container>();
            Costs = new HashSet<Costs>();
        }
        [Key]
        public int GSmid { get; set; }
        public string TypeOfGsm { get; set; }
        public string TTkofType { get; set; }

        public virtual ICollection<Container> Containers { get; set; }
        public virtual ICollection<Costs> Costs { get; set; }
    }
}
