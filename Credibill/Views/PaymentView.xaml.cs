using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Credibill_WPF.Data;
using CrediBill_WPF.Data;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.Views
{
    public partial class PaymentView : UserControl
    {
        private readonly AppDbContext _dbContext;

        public PaymentView()
        {
            InitializeComponent();
            _dbContext = new AppDbContext(); // Database context
            LoadPayments();
            LoadInvoices();  // Charger les factures dans la ComboBox
        }

        // Charger les paiements existants dans le DataGrid
        private void LoadPayments()
        {
            PaymentDataGrid.ItemsSource = _dbContext.Payments
                .Where(p => p.Deleted == DateTime.MaxValue) // Only show non-deleted payments
                .ToList();
        }

        // Charger les factures dans la ComboBox
        private void LoadInvoices()
        {
            InvoiceComboBox.ItemsSource = _dbContext.Invoices
                .Where(i => i.Deleted == DateTime.MaxValue) // Seules les factures actives
                .ToList();
           // InvoiceComboBox.DisplayMemberPath = "Id";   Afficher l'ID dans la ComboBox
            InvoiceComboBox.SelectedValuePath = "Id";  // Sélectionner par l'ID de la facture
        }

        // Ajouter un paiement
        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(AmountTextBox.Text.Trim(), out decimal amount) && PaymentDatePicker.SelectedDate.HasValue)
            {
                // Récupérer l'ID de la facture sélectionnée dans la ComboBox
                int invoiceId = (int)InvoiceComboBox.SelectedValue;

                // Vérifier si la facture existe dans la base de données
                var invoice = _dbContext.Invoices.FirstOrDefault(i => i.Id == invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show("Invoice not found. Please provide a valid Invoice ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Vérifier si le montant payé est inférieur ou égal au montant restant de la facture
                if (invoice.Amount <= 0)
                {
                    MessageBox.Show("The invoice has already been fully paid.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Créer un nouveau paiement
                var newPayment = new Payment
                {
                    Amount = amount,
                    PaymentDate = PaymentDatePicker.SelectedDate.Value,
                    InvoiceId = invoiceId
                };

                // Ajouter le paiement à la base de données
                _dbContext.Payments.Add(newPayment);

                // Mettre à jour le montant restant de la facture
                invoice.Amount -= amount;

                // Sauvegarder les modifications dans la base de données
                _dbContext.SaveChanges();

                MessageBox.Show("Payment added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Effacer les champs après l'ajout du paiement
                AmountTextBox.Clear();
                PaymentDatePicker.SelectedDate = null;

                // Rafraîchir la liste des paiements et des factures
                LoadPayments();
                LoadInvoices();
            }
            else
            {
                MessageBox.Show("Please provide valid input for Amount and Payment Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // Mettre à jour un paiement
        private void UpdatePayment_Click(object sender, RoutedEventArgs e)
        {
            if (PaymentDataGrid.SelectedItem is Payment selectedPayment)
            {
                selectedPayment.Amount = decimal.Parse(AmountTextBox.Text.Trim());
                selectedPayment.PaymentDate = PaymentDatePicker.SelectedDate.Value;

                _dbContext.SaveChanges();

                MessageBox.Show("Payment updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Rafraîchir le DataGrid
                LoadPayments();
            }
            else
            {
                MessageBox.Show("Please select a payment to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Supprimer un paiement
        private void DeletePayment_Click(object sender, RoutedEventArgs e)
        {
            if (PaymentDataGrid.SelectedItem is Payment selectedPayment)
            {
                // Marquer le paiement comme supprimé (suppression logique)
                selectedPayment.Deleted = DateTime.Now;
                _dbContext.SaveChanges();

                MessageBox.Show("Payment deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Rafraîchir le DataGrid
                LoadPayments();
            }
            else
            {
                MessageBox.Show("Please select a payment to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
