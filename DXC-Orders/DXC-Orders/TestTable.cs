namespace DXCOrders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestTable")]
    public partial class TestTable
    {
        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Desc { get; set; }
    }
}
