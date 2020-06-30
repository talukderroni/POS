namespace POSApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("PurchaseInvoiceMas")]
    public partial class PurchaseInvoiceMa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseInvoiceMa()
        {
            PurchaseInvoiceDets = new HashSet<PurchaseInvoiceDet>();
        }

        public int Id { get; set; }

        public int? SupplierId { get; set; }

        [Required]
        [StringLength(25)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(25)]
        public string Email { get; set; }

        public DateTime Date { get; set; }

        [StringLength(20)]
        public string UserFullName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseInvoiceDet> PurchaseInvoiceDets { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
