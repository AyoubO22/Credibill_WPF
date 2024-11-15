using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Credibill_WPF.Data;
using CrediBill_WPF.Data;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        private Payment _selectedPayment;

        public ObservableCollection<Payment> Payments { get; set; }

        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged();
            }
        }

        public PaymentViewModel(AppDbContext context)
        {
            _context = context;
            Payments = new ObservableCollection<Payment>(_context.Payments.ToList());
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            Payments.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void DeletePayment(Payment payment)
        {
            payment.Deleted = DateTime.Now;
            _context.Payments.Update(payment);
            _context.SaveChanges();
            Payments.Remove(payment);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}