using CrediBill_WPF.ViewModels;
using System.Windows.Controls;
using Credibill_WPF.Data;

namespace CrediBill_WPF.Views
{
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            this.DataContext = new CustomerViewModel(new AppDbContext());
        }
    }
}