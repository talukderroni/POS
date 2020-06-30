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

        //----------------Parameter DBSet----------------//

        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<CompanyInformation> CompanyInformations { get; set; }

        public virtual DbSet<GeneralAccount> GeneralAccounts { get; set; }

        //----------------End Parameter DBSet------------//
        //--------- Start Information Db Set --- 30/06/30--Talukder---//


        public virtual DbSet<PurchaseInvoiceDet> PurchaseInvoiceDets { get; set; }
        public virtual DbSet<PurchaseInvoiceMa> PurchaseInvoiceMas { get; set; }






        //===============----End==================//


        //=====================================================================================
        //                             Security                                           |


        public DbSet<Secu_User> Secu_User { get; set; }
        public DbSet<Secu_Role> Secu_Role { get; set; }
        public DbSet<UserImage> UserImage { get; set; }

        public object Entities { get; internal set; }

        //==============================End security =======================================================
       






        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {   

                         
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);



            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Description)
                .IsUnicode(false);



            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Hint)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.OpeningBalance)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Customer>()
                .Property(e => e.TotalPurchase)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Customer>()
                .Property(e => e.TotalPaid)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Customer>()
                .Property(e => e.DueBalance)
                .HasPrecision(12, 2);



            modelBuilder.Entity<Product>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.PurchasePrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.SalesPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Discount)
                .IsUnicode(false);



            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.ExpenseType1)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.PayOver)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralAccount>()
               .Property(e => e.PayOver)
               .IsUnicode(false);

            modelBuilder.Entity<GeneralAccount>()
                .Property(e => e.CashPayment)
                .HasPrecision(12, 2);

            modelBuilder.Entity<GeneralAccount>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseInvoiceDet>()
                .Property(e => e.PurchasePrize)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PurchaseInvoiceMa>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseInvoiceMa>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseInvoiceMa>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseInvoiceMa>()
                .HasMany(e => e.PurchaseInvoiceDets)
                .WithOptional(e => e.PurchaseInvoiceMa)
                .HasForeignKey(e => e.PurchaseInvoiceMasId)
                .WillCascadeOnDelete();


            




        }

       
    }
}