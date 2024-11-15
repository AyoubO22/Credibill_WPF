using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows;
using System.Windows.Controls;

using System.Windows;

using System.Windows;

using System;

using System.Windows;

using System.Windows;

using System.Windows;

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
            MainFrame.Content = new Views.CustomerView();
        }

        private void NavigateToInvoiceView(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Views.InvoiceView();
        }

        private void NavigateToPaymentView(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Views.PaymentView();
        }
    }
}

