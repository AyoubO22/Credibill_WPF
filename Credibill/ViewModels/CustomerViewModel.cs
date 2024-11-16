using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Credibill_WPF.Data;
using Credibill_WPF.Models;

namespace Credibill_WPF.ViewModels
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

        public CustomerViewModel()
        {
        }

        public CustomerViewModel(AppDbContext context)
        {
            _context = context;
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());

            AddCustomerCommand = new RelayCommand<object>((o) => AddCustomer(new Customer()
            {
                Address = "Here",
                Email = "test@mail.com",
                Name = "You"
            }));
            UpdateCustomerCommand = new RelayCommand<object>((o) => UpdateCustomer(SelectedCustomer));
            DeleteCustomerCommand = new RelayCommand<object>((o) => DeleteCustomer(SelectedCustomer));
        }

        public ICommand AddCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }

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