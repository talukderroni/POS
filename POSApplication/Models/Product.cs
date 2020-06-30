namespace POSApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductName { get; set; }

        [ForeignKey("Supplier")]
        public int? SupplierId { get; set; }

        [ForeignKey("ProductCategory")]
        public int? ProductCategoryId { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalesPrice { get; set; }

        [StringLength(20)]
        public string Discount { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
