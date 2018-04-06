namespace DXCOrderChecker
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class OrderCheckerDBModel : DbContext
	{
		public OrderCheckerDBModel()
			: base("name=OrderCheckerDBModelConnection")
		{
		}

		public virtual DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
