namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
            Categories = new HashSet<Category>();
            Readers = new HashSet<Reader>();
            Customers = new HashSet<Customer>();
        }

        [StringLength(10)]
        public string BookID { get; set; }

        [Required]
        [StringLength(200)]
        public string BookName { get; set; }

        public int Price { get; set; }

        public int DiscountPercent { get; set; }

        public int Quantity { get; set; }

        public int TotalSell { get; set; }

        [StringLength(50)]
        public string Avatar { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateByDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Url { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublicByDate { get; set; }

        [StringLength(50)]
        public string BookCover { get; set; }

        public int? Pages { get; set; }

        [Column(TypeName = "text")]
        public string BookDescription { get; set; }

        [Required]
        [StringLength(10)]
        public string AuthorID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProducerID { get; set; }

        public virtual Author Author { get; set; }

        public virtual Producer Producer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reader> Readers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        public object Users { get; internal set; }

        internal static void ToList()
        {
            throw new NotImplementedException();
        }
    }
}
