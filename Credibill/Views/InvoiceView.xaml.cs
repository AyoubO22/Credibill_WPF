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
            _dbContext = new AppDbContext(); // To be improved with dependency injection
            LoadCustomers(); // Load customers for the ComboBox
            LoadInvoices();  // Load invoices into the DataGrid
        }

        /// <summary>
        /// Loads all customers into the ComboBox for customer selection.
        /// </summary>
        private void LoadCustomers()
        {
            var customers = _dbContext.Customers
                .Where(c => c.Deleted == DateTime.MaxValue) // Only show non-deleted customers
                .ToList();

            // Bind the ComboBox to the customer list
            CustomerComboBox.ItemsSource = customers;
        }

        /// <summary>
        /// Loads all active invoices from the database and displays them in the DataGrid.
        /// </summary>
        private void LoadInvoices()
        {
            // Refresh the invoices list by fetching the data from the database
            InvoiceDataGrid.ItemsSource = _dbContext.Invoices
                .Where(i => i.Deleted == DateTime.MaxValue) // Only show non-deleted invoices
                .ToList();
        }

        /// <summary>
        /// Handles the logic to add a new invoice when the "Add Invoice" button is clicked.
        /// </summary>
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            // Validate input fields
            if (CustomerComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(AmountTextBox.Text))
            {
                MessageBox.Show("Customer and Amount are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get selected customer and invoice data
            var selectedCustomer = (Customer)CustomerComboBox.SelectedItem;
            if (!decimal.TryParse(AmountTextBox.Text.Trim(), out decimal amount))
            {
                MessageBox.Show("Invalid amount entered.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime issueDate = IssueDatePicker.SelectedDate ?? DateTime.Now;

            // Create a new invoice (Entity Framework will automatically generate the ID)
            var newInvoice = new Invoice
            {
                CustomerId = selectedCustomer.Id,
                Amount = amount,
                IssueDate = issueDate,
                Deleted = DateTime.MaxValue // Set the invoice as active
            };

            // Save the new invoice to the database
            _dbContext.Invoices.Add(newInvoice);
            _dbContext.SaveChanges();

            MessageBox.Show("Invoice added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear input fields after adding
            AmountTextBox.Clear();
            IssueDatePicker.SelectedDate = null;
            CustomerComboBox.SelectedIndex = -1;

            // Refresh the DataGrid to display the new invoice
            LoadInvoices();
        }

        /// <summary>
        /// Handles the logic to delete a selected invoice when the "Delete" button is clicked.
        /// </summary>
        private void DeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceDataGrid.SelectedItem is Invoice selectedInvoice)
            {
                // Mark the selected invoice as deleted
                selectedInvoice.Deleted = DateTime.Now;
                _dbContext.SaveChanges();

                MessageBox.Show("Invoice deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the DataGrid to reflect changes
                LoadInvoices();
            }
            else
            {
                MessageBox.Show("Please select an invoice to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Handles the logic to update a selected invoice when the "Update" button is clicked.
        /// </summary>
        private void UpdateInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceDataGrid.SelectedItem is Invoice selectedInvoice)
            {
                // Open the EditInvoiceWindow to edit the selected invoice
                var editWindow = new EditInvoiceWindow(selectedInvoice);

                var result = editWindow.ShowDialog();

                // If the update was successful, save the changes to the database
                if (result == true)
                {
                    _dbContext.SaveChanges();
                    MessageBox.Show("Invoice updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh the DataGrid to display updated invoices
                    LoadInvoices();
                }
            }
            else
            {
                MessageBox.Show("Please select an invoice to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
