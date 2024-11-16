using System.Windows.Controls;
using Credibill_WPF.Data;
using CrediBill_WPF.ViewModels;

namespace Credibill_WPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class InvoiceView : Page
    {
        private InvoiceViewModel _model;

        public InvoiceView(AppDbContext context)
        {
            InitializeComponent();
            _model = new InvoiceViewModel(context);
            DataContext = _model;
        }
    }
}
