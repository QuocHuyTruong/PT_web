namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        [StringLength(10)]
        public string ReviewID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReviewByDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Required]
        [StringLength(10)]
        public string BookID { get; set; }

        [Required]
        [StringLength(10)]
        public string CustomerID { get; set; }

        public virtual Book Book { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
