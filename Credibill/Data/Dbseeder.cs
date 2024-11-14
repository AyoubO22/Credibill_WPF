using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrediBill_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Credibill_WPF.Data;
using Credibill_WPF.Models;

namespace CrediBill_WPF.Data
{
    public class DbSeeder
    {
        private readonly AppDbContext _context;

        public DbSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Créer les clients de test
            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(
                    new Customer
                    {
                        Name = "John Doe",
                        Email = "john.doe@example.com",
                        Address = "1234 Elm Street"
                    },
                    new Customer
                    {
                        Name = "Jane Smith",
                        Email = "jane.smith@example.com",
                        Address = "5678 Oak Avenue"
                    }
                );
                _context.SaveChanges();
            }

            // Créer les factures de test
            if (!_context.Invoices.Any())
            {
                var customer1 = _context.Customers.First();
                var customer2 = _context.Customers.Skip(1).First();

                _context.Invoices.AddRange(
                    new Invoice
                    {
                        Amount = 200.50m,
                        IssueDate = DateTime.Now.AddDays(-30),
                        Deleted = DateTime.MaxValue,
                        CustomerId = customer1.Id
                    },
                    new Invoice
                    {
                        Amount = 350.00m,
                        IssueDate = DateTime.Now.AddDays(-15),
                        Deleted = DateTime.MaxValue,
                        CustomerId = customer2.Id
                    }
                );
                _context.SaveChanges();
            }

            // Créer les paiements de test
            if (!_context.Payments.Any())
            {
                var invoice1 = _context.Invoices.First();
                var invoice2 = _context.Invoices.Skip(1).First();

                _context.Payments.AddRange(
                    new Payment
                    {
                        Amount = 150.00m,
                        PaymentDate = DateTime.Now.AddDays(-10),
                        InvoiceId = invoice1.Id
                    },
                    new Payment
                    {
                        Amount = 200.00m,
                        PaymentDate = DateTime.Now.AddDays(-5),
                        InvoiceId = invoice2.Id
                    }
                );
                _context.SaveChanges();
            }
        }
    }
}
