using System;
using System.Linq;
using System.Windows;
using Credibill_WPF.Data;
using CrediBill_WPF.Data;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.Views
{
    public partial class EditInvoiceWindow : Window
    {
        private readonly AppDbContext _dbContext;
        private readonly Invoice _invoice;

        public EditInvoiceWindow(Invoice invoice)
        {
            InitializeComponent();
            _dbContext = new AppDbContext(); // Ideally use Dependency Injection
            _invoice = invoice;
            LoadInvoiceDetails();
        }

        /// <summary>
        /// Loads the details of the invoice into the fields for editing.
        /// </summary>
        private void LoadInvoiceDetails()
        {
            AmountTextBox.Text = _invoice.Amount.ToString();
            IssueDateTextBox.Text = _invoice.IssueDate.ToString("yyyy-MM-dd");
            CustomerComboBox.SelectedItem = _dbContext.Customers.FirstOrDefault(c => c.Id == _invoice.CustomerId);
        }

        /// <summary>
        /// Handles the logic for saving the changes made to the invoice when the "Save" button is clicked.
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
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

            // Update the invoice properties
            _invoice.Amount = amount;
            _invoice.IssueDate = issueDate;

            // Save changes to the database
            _dbContext.SaveChanges();

            MessageBox.Show("Invoice updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close the window
            Close();
        }

        /// <summary>
        /// Handles the logic for canceling the edit when the "Cancel" button is clicked.
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving changes
            Close();
        }
    }
}
