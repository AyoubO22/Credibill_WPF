using System.Windows.Controls;
using Credibill_WPF.Data;
using Credibill_WPF.ViewModels;

namespace Credibill_WPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {
        private CustomerViewModel _model;

        public CustomerView(AppDbContext context)
        {
            InitializeComponent();
            _model = new CustomerViewModel(context);
            DataContext = _model;
        }
    }
}
