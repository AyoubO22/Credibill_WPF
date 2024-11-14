using Credibill_WPF.Data;
using CrediBill_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrediBill_WPF.ViewModels
{
    public class InvoiceViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly AppDbContext _context;  // Database context
        private decimal _amount;
        private DateTime _issueDate;
        private DateTime _deleted;
        private int _customerId;
        public ObservableCollection<Invoice> Invoices { get; set; }

        // Properties for data binding
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public DateTime IssueDate
        {
            get => _issueDate;
            set
            {
                _issueDate = value;
                OnPropertyChanged(nameof(IssueDate));
            }
        }

        public DateTime Deleted
        {
            get => _deleted;
            set
            {
                _deleted = value;
                OnPropertyChanged(nameof(Deleted));
            }
        }

        public int CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }

        // Commands
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        // Event for property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor
        public InvoiceViewModel()
        {
            _context = new AppDbContext(); // Initialize the database context
            Invoices = new ObservableCollection<Invoice>(); // Initialize empty collection

            // Load invoices on initialization
            LoadInvoicesAsync();

            // Initialize commands
            SaveCommand = new CommandHandler(async param => await SaveInvoice(), CanSaveInvoice);
            DeleteCommand = new CommandHandler(async param => await DeleteInvoice(param), CanDeleteInvoice);
            UpdateCommand = new CommandHandler(async param => await UpdateInvoice(param), CanUpdateInvoice);
        }

        // Create: Add a new invoice
        private async Task SaveInvoice()
        {
            try
            {
                // Validate input
                if (!ValidateInvoiceInput())
                {
                    return;
                }

                var invoice = new Invoice
                {
                    Amount = Amount,
                    IssueDate = IssueDate,
                    Deleted = Deleted,
                    CustomerId = CustomerId
                };

                _context.Invoices.Add(invoice); // Add invoice to DB
                await _context.SaveChangesAsync(); // Save changes asynchronously

                // Add to collection for UI update
                Invoices.Add(invoice);
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., database connection issues)
                HandleError(ex);
            }
        }

        // Read: Load invoices from the database
        private async Task LoadInvoicesAsync()
        {
            try
            {
                Invoices.Clear(); // Clear existing invoices
                var invoicesFromDb = await _context.Invoices.ToListAsync();
                foreach (var invoice in invoicesFromDb)
                {
                    Invoices.Add(invoice); // Add to ObservableCollection for UI binding
                }
            }
            catch (Exception ex)
            {
                // Handle errors during loading
                HandleError(ex);
            }
        }

        // Update: Update an existing invoice
        private async Task UpdateInvoice(object parameter)
        {
            try
            {
                // Validate input
                if (!ValidateInvoiceInput())
                {
                    return;
                }

                var invoiceToUpdate = _context.Invoices.FirstOrDefault(i => i.Id == (int)parameter);
                if (invoiceToUpdate != null)
                {
                    invoiceToUpdate.Amount = Amount;
                    invoiceToUpdate.IssueDate = IssueDate;
                    invoiceToUpdate.Deleted = Deleted;
                    invoiceToUpdate.CustomerId = CustomerId;

                    await _context.SaveChangesAsync(); // Save changes asynchronously
                }
            }
            catch (Exception ex)
            {
                // Handle errors during update
                HandleError(ex);
            }
        }

        // Delete: Delete an invoice from the database
        private async Task DeleteInvoice(object parameter)
        {
            try
            {
                var invoiceToDelete = _context.Invoices.FirstOrDefault(i => i.Id == (int)parameter);
                if (invoiceToDelete != null)
                {
                    _context.Invoices.Remove(invoiceToDelete); // Remove from DB
                    await _context.SaveChangesAsync(); // Save changes asynchronously

                    // Remove from ObservableCollection to update UI
                    Invoices.Remove(invoiceToDelete);
                }
            }
            catch (Exception ex)
            {
                // Handle errors during deletion
                HandleError(ex);
            }
        }

        // Validate invoice input
        private bool ValidateInvoiceInput()
        {
            if (Amount <= 0 || CustomerId <= 0)
            {
                // Display validation errors (e.g., message box, logging)
                return false;
            }
            return true;
        }

        // Handle errors
        private void HandleError(Exception ex)
        {
            // Log error or show error message
            // Example: log the error, or display a message box to the user
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Check if Save is possible
        private bool CanSaveInvoice(object parameter)
        {
            return Amount > 0 && CustomerId > 0;
        }

        // Check if Delete is possible
        private bool CanDeleteInvoice(object parameter)
        {
            return parameter != null;
        }

        // Check if Update is possible
        private bool CanUpdateInvoice(object parameter)
        {
            return parameter != null && Amount > 0 && CustomerId > 0;
        }

        // Property change notification
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Dispose the context properly
        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class CommandHandler : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object> _canExecute;

        public CommandHandler(Func<object, Task> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            if (_execute != null)
            {
                await _execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
