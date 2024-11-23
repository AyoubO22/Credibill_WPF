using CrediBill_WPF.Views;
using System.Windows;

namespace CrediBill_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Actions pour le menu "File"
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Quitter l'application
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New File clicked!");
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open File clicked!");
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save File clicked!");
        }

        // Actions pour le menu "Edit"
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cut clicked!");
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copy clicked!");
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Paste clicked!");
        }

        // Action pour le menu "Help"
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About clicked! CrediBill - Invoice and Payment Management");
        }

        // Actions pour les boutons de navigation
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
