using Credibill_WPF.Data;
using CrediBill_WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrediBill_WPF.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly AppDbContext _context;
        private decimal _amount;
        private DateTime _paymentDate;
        private int _invoiceId;

        // List of payments
        public ObservableCollection<Payment> Payments { get; set; }

        // Properties with notifications
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public DateTime PaymentDate
        {
            get => _paymentDate;
            set
            {
                _paymentDate = value;
                OnPropertyChanged(nameof(PaymentDate));
            }
        }

        public int InvoiceId
        {
            get => _invoiceId;
            set
            {
                _invoiceId = value;
                OnPropertyChanged(nameof(InvoiceId));
            }
        }

        // Commands for CRUD operations
        public ICommand SaveCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PaymentViewModel()
        {
            // Initialize the DbContext and Payments collection
            _context = new AppDbContext();
            Payments = new ObservableCollection<Payment>(_context.Payments.Where(p => p.Deleted == DateTime.MaxValue).ToList());

            // Commands
            SaveCommand = new CommandHandler(async param => await SavePayment(), CanSavePayment);
            UpdateCommand = new CommandHandler(async param => await UpdatePayment(), CanUpdatePayment);
            DeleteCommand = new CommandHandler(async param => await DeletePayment(param), CanDeletePayment);
        }

        // Method to save a new payment
        private async Task SavePayment()
        {
            try
            {
                if (!ValidatePaymentInput()) return;

                var payment = new Payment
                {
                    Amount = Amount,
                    PaymentDate = PaymentDate,
                    InvoiceId = InvoiceId
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                Payments.Add(payment); // Add the payment to the observable collection
            }
            catch (Exception ex)
            {
                HandleError(ex); // Handle errors
            }
        }

        // Method to update an existing payment
        private async Task UpdatePayment()
        {
            try
            {
                var paymentToUpdate = _context.Payments.FirstOrDefault(p => p.Id == InvoiceId); // Using InvoiceId for identification

                if (paymentToUpdate != null && ValidatePaymentInput())
                {
                    paymentToUpdate.Amount = Amount;
                    paymentToUpdate.PaymentDate = PaymentDate;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    MessageBox.Show("Payment not found or invalid data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        // Method to delete a payment
        private async Task DeletePayment(object parameter)
        {
            try
            {
                if (parameter is Payment payment)
                {
                    // Mark the payment as deleted instead of removing it directly
                    payment.Deleted = DateTime.Now;
                    await _context.SaveChangesAsync();
                    Payments.Remove(payment); // Remove the payment from the collection
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        // Validation method for payments
        private bool ValidatePaymentInput() => Amount > 0 && InvoiceId > 0;

        // CanExecute checks for commands
        private bool CanSavePayment(object parameter) => Amount > 0 && InvoiceId > 0;
        private bool CanUpdatePayment(object parameter) => Amount > 0 && InvoiceId > 0;
        private bool CanDeletePayment(object parameter) => parameter is Payment;

        // Handle errors (showing a message box and logging)
        private void HandleError(Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            LogError(ex); // Log the error for further diagnostics
        }

        // Error logging method
        private void LogError(Exception ex)
        {
            string logFilePath = "error_log.txt";
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {ex.Message}");
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine();
            }
        }

        // PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Dispose method to clean up the DbContext
        public void Dispose() => _context.Dispose();
    }
}
