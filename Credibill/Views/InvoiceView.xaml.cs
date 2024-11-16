using CrediBill_WPF.ViewModels;
using System.Windows.Controls;
using Credibill_WPF.Data;

namespace CrediBill_WPF.Views
{
    public partial class InvoiceView : UserControl
    {
        public InvoiceView()
        {
            InitializeComponent();
            this.DataContext = new InvoiceViewModel(new AppDbContext());
        }
    }
}