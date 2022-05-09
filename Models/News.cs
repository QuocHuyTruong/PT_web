namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [StringLength(10)]
        public string NewsID { get; set; }

        [Required]
        [StringLength(200)]
        public string NewsName { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PostByDate { get; set; }
    }
}
