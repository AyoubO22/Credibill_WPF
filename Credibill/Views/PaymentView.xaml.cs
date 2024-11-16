using CrediBill_WPF.ViewModels;
using System.Windows.Controls;
using Credibill_WPF.Data;
using Credibill.Views;

namespace CrediBill_WPF.Views
{
    public partial class PaymentView : UserControl
    {
        public PaymentView()
        {
            InitializeComponent();
            this.DataContext = new PaymentViewModel(new AppDbContext());
        }
    }
}