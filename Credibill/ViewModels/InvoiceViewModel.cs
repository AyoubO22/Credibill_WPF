using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Credibill_WPF.Data;
using CrediBill_WPF.Data;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.ViewModels
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        private Invoice _selectedInvoice;

        public ObservableCollection<Invoice> Invoices { get; set; }

        public Invoice SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                _selectedInvoice = value;
                OnPropertyChanged();
            }
        }

        public InvoiceViewModel(AppDbContext context)
        {
            _context = context;
            Invoices = new ObservableCollection<Invoice>(_context.Invoices.ToList());
        }

        public void AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            Invoices.Add(invoice);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
        }

        public void DeleteInvoice(Invoice invoice)
        {
            invoice.Deleted = DateTime.Now;
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
            Invoices.Remove(invoice);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}