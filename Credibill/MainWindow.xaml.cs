using System.Windows;
using Credibill_WPF.Data;
using Credibill_WPF.Views;

namespace CrediBill_WPF
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context;
        private CustomerView _customerView;
        private InvoiceView _invoiceView;
        private PaymentView _paymentView;
        public MainWindow()
        {
            _context = new AppDbContext();
            _customerView = new CustomerView(_context);
            _invoiceView = new InvoiceView(_context);
            _paymentView = new PaymentView(_context);
            InitializeComponent();
        }

        private void NavigateToCustomerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _customerView;
        }

        private void NavigateToInvoiceView(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _invoiceView;
        }

        private void NavigateToPaymentView(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _paymentView;
        }
    }
}

