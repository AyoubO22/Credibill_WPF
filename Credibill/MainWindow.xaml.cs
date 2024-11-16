﻿using System;
using System.Windows;
using System.Windows.Controls;

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