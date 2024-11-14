using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CrediBill_WPF.Models;

namespace Credibill_WPF.Models
{

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        // One-to-many relationship with Invoice
        public ICollection<Invoice> Invoices { get; set; }
    }

}

