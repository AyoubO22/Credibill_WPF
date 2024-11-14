using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows;
using System.Windows.Controls;

using System.Windows;

using System.Windows;

using System;

using System.Windows;
using Credibill_WPF.Views;
// Assurez-vous que le namespace de votre CustomerView est inclus

using System.Windows;
using CrediBill_WPF.ViewModels; // Assurez-vous que le bon namespace est utilisé

namespace CrediBill_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Associer le MainViewModel à la fenêtre
            this.DataContext = new MainViewModel(); // Initialisation du MainViewModel
        }
    }
}
