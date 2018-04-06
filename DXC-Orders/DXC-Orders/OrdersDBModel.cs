namespace DXCOrders
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class OrdersDBModel : DbContext
	{
		public OrdersDBModel()
			: base("name=OrdersDBModelConnection")
		{
		}

		public virtual DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
