
using Credibill_WPF.Views;
using Credibill_WPF;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CrediBill_WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Déclaration des commandes
        private ICommand _navigateToCustomerViewCommand;
        private ICommand _navigateToInvoiceViewCommand;
        private ICommand _navigateToPaymentViewCommand;

        // Commandes de navigation
        public ICommand NavigateToCustomerViewCommand
        {
            get
            {
                if (_navigateToCustomerViewCommand == null)
                {
                    _navigateToCustomerViewCommand = new RelayCommand(NavigateToCustomerView);
                }
                return _navigateToCustomerViewCommand;
            }
        }

        public ICommand NavigateToInvoiceViewCommand
        {
            get
            {
                if (_navigateToInvoiceViewCommand == null)
                {
                    _navigateToInvoiceViewCommand = new RelayCommand(NavigateToInvoiceView);
                }
                return _navigateToInvoiceViewCommand;
            }
        }

        public ICommand NavigateToPaymentViewCommand
        {
            get
            {
                if (_navigateToPaymentViewCommand == null)
                {
                    _navigateToPaymentViewCommand = new RelayCommand(NavigateToPaymentView);
                }
                return _navigateToPaymentViewCommand;
            }
        }

        // Méthodes de navigation
        private void NavigateToCustomerView()
        {
            var customerView = new CustomerView(); // Assure-toi d'avoir créé cette vue
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Content = customerView;
        }

        private void NavigateToInvoiceView()
        {
            var invoiceView = new InvoiceView(); // Assure-toi d'avoir créé cette vue
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Content = invoiceView;
        }

        private void NavigateToPaymentView()
        {
            var paymentView = new PaymentView(); // Assure-toi d'avoir créé cette vue
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Content = paymentView;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Implémentation de la méthode INotifyPropertyChanged
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Simple implementation of RelayCommand
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter) => true; // Always allow execution for navigation

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
