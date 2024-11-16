using Credibill_WPF.Data;
using CrediBill_WPF.ViewModels;
using System.Windows.Controls;

namespace Credibill_WPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class PaymentView : Page
    {
        private PaymentViewModel _model;

        public PaymentView(AppDbContext context)
        {
            InitializeComponent();

            _model = new PaymentViewModel(context);
            DataContext = _model;
        }
    }
}
