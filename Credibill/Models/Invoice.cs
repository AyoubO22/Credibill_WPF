using Credibill_WPF.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrediBill_WPF.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        // foreign key to the customer associated with this invoice
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // One-to-many relationship with Payment
        public ICollection<Payment> Payments { get; set; }
    }

}

