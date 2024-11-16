using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Credibill_WPF.Data;
using CrediBill_WPF.Data;
using Credibill_WPF.Models;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        private Customer _selectedCustomer;

        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public CustomerViewModel(AppDbContext context)
        {
            _context = context;
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            customer.Deleted = DateTime.Now;
            _context.Customers.Update(customer);
            _context.SaveChanges();
            Customers.Remove(customer);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}