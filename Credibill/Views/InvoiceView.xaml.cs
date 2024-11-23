using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Credibill_WPF.Data;
using Credibill_WPF.Models;
using CrediBill_WPF.Data;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.Views
{
    public partial class InvoiceView : UserControl
    {
        private readonly AppDbContext _dbContext;

        public InvoiceView()
        {
            InitializeComponent();
            _dbContext = new AppDbContext(); // Should ideally use Dependency Injection
            LoadInvoices();
        }

        /// <summary>
        /// Loads all active invoices from the database and displays them in the DataGrid.
        /// </summary>
        private void LoadInvoices()
        {
            // Refresh the invoice list by retrieving data from the database
            InvoiceDataGrid.ItemsSource = _dbContext.Invoices
                .Where(i => i.Deleted == DateTime.MaxValue) // Only active invoices
                .ToList();
        }

        /// <summary>
        /// Handles the logic for adding a new invoice to the database when the "Add Invoice" button is clicked.
        /// </summary>
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            decimal amount;
            DateTime issueDate;

            // Validate the amount
            if (!decimal.TryParse(AmountTextBox.Text.Trim(), out amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate the issue date
            if (!DateTime.TryParse(IssueDateTextBox.Text.Trim(), out issueDate))
            {
                MessageBox.Show("Please enter a valid date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Select a customer from the ComboBox
            if (CustomerComboBox.SelectedItem is Customer selectedCustomer)
            {
                // Create a new invoice
                var newInvoice = new Invoice
                {
                    Amount = amount,
                    IssueDate = issueDate,
                    CustomerId = selectedCustomer.Id,
                    Deleted = DateTime.MaxValue // Active invoice
                };

                // Add the new invoice to the database
                _dbContext.Invoices.Add(newInvoice);
                _dbContext.SaveChanges();

                MessageBox.Show("Invoice added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset input fields
                AmountTextBox.Clear();
                IssueDateTextBox.Clear();
                CustomerComboBox.SelectedIndex = -1;

                // Refresh the DataGrid to display the new invoice
                LoadInvoices();
            }
            else
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Handles the logic for deleting a selected invoice when the "Delete" button is clicked.
        /// </summary>
        private void DeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceDataGrid.SelectedItem is Invoice selectedInvoice)
            {
                // Mark the selected invoice as deleted
                selectedInvoice.Deleted = DateTime.Now;
                _dbContext.SaveChanges();

                MessageBox.Show("Invoice deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the DataGrid to reflect the change
                LoadInvoices();
            }
            else
            {
                MessageBox.Show("Please select an invoice to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Handles the logic for updating the selected invoice when the "Update" button is clicked.
        /// </summary>
        private void UpdateInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceDataGrid.SelectedItem is Invoice selectedInvoice)
            {
                // Open the EditInvoiceWindow for editing the selected invoice
                var editWindow = new EditInvoiceWindow(selectedInvoice);
                editWindow.ShowDialog();

                // Refresh the DataGrid to display any updates
                LoadInvoices();
            }
            else
            {
                MessageBox.Show("Please select an invoice to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
