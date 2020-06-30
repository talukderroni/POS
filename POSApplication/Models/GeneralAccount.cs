namespace POSApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeneralAccount
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int? ExpenseTypeId { get; set; }

        [Required]
        [StringLength(30)]
        public string PayOver { get; set; }

        public decimal CashPayment { get; set; }

        [StringLength(20)]
        public string Description { get; set; }
    }
}
