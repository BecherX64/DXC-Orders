namespace DemandTrackerForm
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class DemandTrackerDBModelNew : DbContext
	{
		public DemandTrackerDBModelNew()
			: base("name=DemandTrackerDBModelNew")
		{
		}

		public virtual DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
