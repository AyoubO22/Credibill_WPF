using System;
using System.Windows;
using CrediBill_WPF.Views;

namespace CrediBill_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigateToCustomerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomerView());
        }

        private void NavigateToInvoiceView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InvoiceView());
        }

        private void NavigateToPaymentView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PaymentView());
        }

    }
}