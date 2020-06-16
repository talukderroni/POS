using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Common;
using System.Linq;
using System.Web;
using POSApplication.Models;

namespace POSApplication.Models
{
    public class POSDBContext : DbContext
    {
        public POSDBContext() : base("POSDB")
        {

        }

        public POSDBContext (DbConnection con, bool contextOwnsConnection) : base(con, contextOwnsConnection)
        {

        }

        //=====================================================================================
       //                             Security                                           |
        //=====================================================================================

        public DbSet<Secu_User> Secu_User { get; set; }
        public DbSet<Secu_Role> Secu_Role { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public object Entities { get; internal set; }





        



        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {   

                         
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<AccountSalesInvoiceMas>()
            //   .Property(e => e.Coversion_Rate)
            //   .HasPrecision(12, 5);

            //modelBuilder.Entity<AccountSalesInvoiceMas>()
            //    .Property(e => e.Amount)
            //    .HasPrecision(15, 2);

            //modelBuilder.Entity<AccountSalesInvoiceMas>()
            //    .Property(e => e.AmountBDT)
            //    .HasPrecision(15, 2);

            //modelBuilder.Entity<AccountSalesInvoiceMas>()
            //    .Property(e => e.Vat)
            //    .HasPrecision(15, 2);



        //    modelBuilder.Entity<AccountSalesInvoiceDet>()
        //        .Property(e => e.Rate)
        //        .HasPrecision(15, 2);

        //    modelBuilder.Entity<AccountSalesInvoiceDet>()
        //        .Property(e => e.Amount)
        //        .HasPrecision(15, 2);

        //    modelBuilder.Entity<AccountSalesInvoiceDet>()
        //        .Property(e => e.AmountBDT)
        //        .HasPrecision(15, 2);



        //    modelBuilder.Entity<AccountPaymentReceive>()
        //        .Property(e => e.Balance)
        //        .HasPrecision(12, 2);

        //    modelBuilder.Entity<AccountPaymentReceive>()
        //        .Property(e => e.Coversion_Rate)
        //        .HasPrecision(12, 5);

        //    modelBuilder.Entity<AccountPaymentReceive>()
        //        .Property(e => e.Amount)
        //        .HasPrecision(15, 2);

        //    modelBuilder.Entity<AccountPaymentReceive>()
        //        .Property(e => e.AmountBDT)
        //        .HasPrecision(15, 2);



        //modelBuilder.Entity<AccountLedger>()
        // .Property(e => e.DrAmnt)
        // .HasPrecision(12, 2);

        //modelBuilder.Entity<AccountLedger>()
        //    .Property(e => e.CrAmnt)
        //    .HasPrecision(12, 2);


        //    modelBuilder.Entity<Act_SalesOrderDet>()
        //       .Property(e => e.SalesOrderRate)
        //       .HasPrecision(18, 5);

        //    modelBuilder.Entity<Act_SalesOrderDet>()
        //        .Property(e => e.Amount)
        //        .HasPrecision(18, 5);

        //    modelBuilder.Entity<Act_SalesOrderDet>()
        //        .Property(e => e.Vat)
        //        .HasPrecision(18, 5);

        //    modelBuilder.Entity<Act_SalesOrderDet>()
        //        .Property(e => e.Total_Amount)
        //        .HasPrecision(18, 5);



        //    modelBuilder.Entity<Act_PurchaseInvoiceMas>()
        //       .Property(e => e.Coversion_Rate)
        //       .HasPrecision(12, 5);

        //    modelBuilder.Entity<Act_PurchaseInvoiceMas>()
        //        .Property(e => e.Amount)
        //        .HasPrecision(15, 2);

        //    modelBuilder.Entity<Act_PurchaseInvoiceMas>()
        //        .Property(e => e.AmountBDT)
        //        .HasPrecision(15, 2);



        //    modelBuilder.Entity<Act_PurchaseInvoiceDet>()
        //       .Property(e => e.Amount)
        //       .HasPrecision(15, 2);

        //    modelBuilder.Entity<Act_PurchaseInvoiceDet>()
        //        .Property(e => e.AmountBDT)
        //        .HasPrecision(15, 2);



           // modelBuilder.Entity<Act_PurchaseInvoiceMas>().Map(p => p.Requires("LCPONO").HasValue(123)).Ignore(ce => ce.LCPONO);


        }

        //public System.Data.Entity.DbSet<Supply_Chain_Management.Models.AccountGlStockcs> AccountGlStockcs { get; set; }
    }
}