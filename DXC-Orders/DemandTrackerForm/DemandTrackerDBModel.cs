namespace DemandTrackerForm
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class DemandTrackerDBModel : DbContext
	{
		public DemandTrackerDBModel()
			: base("name=DemandTrackerDBModelConnection")
		{
		}

		public virtual DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
