using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credibill_WPF.Models;
using CrediBill_WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace Credibill_WPF.Data
{
    public class AppDbContext : DbContext
    {
        // Define database tables
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }  
        public DbSet<Payment> Payments { get; set; }  

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CredibillDBWPF;Trusted_Connection=True;\r\n");
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Payments)
                .HasForeignKey(p => p.InvoiceId);
        }
    }
}