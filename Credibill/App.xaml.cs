using Credibill_WPF.Data;
using CrediBill_WPF.Data;
using Credibill_WPF;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CrediBill_WPF
{
    public partial class App : Application
    {
        private AppDbContext _context;
        private DbSeeder _dbSeeder;

        public App()
        {
            _context = new AppDbContext();  // Initialise le contexte de base de données
            _dbSeeder = new DbSeeder(_context);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Appel de la méthode Seed pour ajouter des données de test à la base de données
            _dbSeeder.Seed();

            // Démarrer la fenêtre principale de l'application
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}

