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
    public partial class CustomerView : UserControl
    {
        private readonly AppDbContext _dbContext;

        public CustomerView()
        {
            InitializeComponent();
            _dbContext = new AppDbContext(); // To be improved with dependency injection
            LoadCustomers();
        }

        /// <summary>
        /// Loads all active customers from the database and displays them in the DataGrid.
        /// </summary>
        private void LoadCustomers()
        {
            // Refresh the customers list by fetching the data from the database
            CustomersDataGrid.ItemsSource = _dbContext.Customers
                .Where(c => c.Deleted == DateTime.MaxValue) // Only show non-deleted customers
                .ToList();
        }

        /// <summary>
        /// Handles the logic to add a new customer to the database when the "Add Customer" button is clicked.
        /// </summary>
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();

            // Validate input fields
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create a new customer
            var newCustomer = new Customer
            {
                Name = name,
                Email = email,
                Address = address,
                Deleted = DateTime.MaxValue,
                
            };

            // Save the new customer to the database
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();

            MessageBox.Show("Customer added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear input fields after adding
            NameTextBox.Clear();
            EmailTextBox.Clear();
            AddressTextBox.Clear();

            // Refresh the DataGrid to display the new customer
            LoadCustomers();
        }

        /// <summary>
        /// Handles the logic to delete a selected customer when the "Delete" button is clicked.
        /// </summary>
        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer selectedCustomer)
            {
                // Mark the selected customer as deleted
                selectedCustomer.Deleted = DateTime.Now;
                _dbContext.SaveChanges();

                MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the DataGrid to reflect changes
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Handles the logic to update the selected customer when the "Update" button is clicked.
        /// </summary>
        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer selectedCustomer)
            {
                
                var editWindow = new EditCustomerWindow(selectedCustomer);

                
                var result = editWindow.ShowDialog();

               
                if (result == true)
                {
                    _dbContext.SaveChanges(); 
                    MessageBox.Show("Customer updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
