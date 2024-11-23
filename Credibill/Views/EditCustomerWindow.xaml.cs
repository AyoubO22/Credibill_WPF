using System;
using System.Windows;
using Credibill_WPF.Models;
using CrediBill_WPF.Models;

namespace CrediBill_WPF.Views
{
    public partial class EditCustomerWindow : Window
    {
        public Customer Customer { get; set; }

        public EditCustomerWindow(Customer customer)
        {
            InitializeComponent();

            // Set the customer to edit
            Customer = customer;

            // Fill in the existing customer data into the TextBoxes
            NameTextBox.Text = customer.Name;
            EmailTextBox.Text = customer.Email;
            AddressTextBox.Text = customer.Address;
        }

        // Save the changes and close the window
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update customer with new values
            Customer.Name = NameTextBox.Text.Trim();
            Customer.Email = EmailTextBox.Text.Trim();
            Customer.Address = AddressTextBox.Text.Trim();

            // Close the window and pass the updated customer back
            this.DialogResult = true;
            Close();
        }

        // Cancel the editing and close the window
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}