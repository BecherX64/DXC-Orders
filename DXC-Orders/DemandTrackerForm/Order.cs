namespace DemandTrackerForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Creator { get; set; }

        [Required]
        [StringLength(20)]
        public string TaskName { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(20)]
        public string Assignee { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public string Note { get; set; }

        public bool? LockStatus { get; set; }
    }
}
