using lab7.Models;
using System.Data.Entity;

namespace lab7.Data
{
    public partial class Petrol_StationContext : DbContext
    {
        public Petrol_StationContext() : base("name=gsmConnectionString")
        {
        }

        public virtual DbSet<Container> Containers { get; set; }
        public virtual DbSet<Costs> Costs { get; set; }
        public virtual DbSet<Gsm> Gsm { get; set; }
        public virtual DbSet<IncomeAndExpensesOfGsm> IncomeAndExpensesOfGsms { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
    }
}
