using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CrediBill_WPF.Models;
using CrediBill_WPF.Data;

namespace CrediBill_WPF.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly AppDbContext _context;
        private string _name;
        private string _email;
        private string _address;
        private Customer _selectedCustomer; // Propri�t� pour le client s�lectionn�

        public ObservableCollection<Customer> Customers { get; set; }

        // Propri�t�s pour lier les donn�es de la vue
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                if (_selectedCustomer != null)
                {
                    // Mettre � jour les champs lorsque le client est s�lectionn�
                    Name = _selectedCustomer.Name;
                    Email = _selectedCustomer.Email;
                    Address = _selectedCustomer.Address;
                }
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        // Commandes
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        // Ev�nement pour notifier les changements de propri�t�s
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructeur
        public CustomerViewModel()
        {
            _context = new AppDbContext(); // Initialisation du contexte de la base de donn�es
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList()); // Charger les clients depuis la base de donn�es

            // Initialisation des commandes
            SaveCommand = new RelayCommand(async param => await SaveCustomer(), CanSaveCustomer);
            DeleteCommand = new RelayCommand(async param => await DeleteCustomer(param), CanDeleteCustomer);
            UpdateCommand = new RelayCommand(async param => await UpdateCustomer(), CanUpdateCustomer);
        }

        // Cr�er : Ajouter un nouveau client
        private async Task SaveCustomer()
        {
            try
            {
                // Validation des entr�es
                if (!ValidateCustomerInput())
                {
                    return;
                }

                var customer = new Customer
                {
                    Name = Name,
                    Email = Email,
                    Address = Address
                };

                _context.Customers.Add(customer); // Ajouter le client � la base de donn�es
                await _context.SaveChangesAsync(); // Sauvegarder les changements de mani�re asynchrone

                // Ajouter � la collection pour mettre � jour l'UI
                Customers.Add(customer);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        // Lire : Charger les clients depuis la base de donn�es
        private async Task LoadCustomers()
        {
            try
            {
                Customers.Clear();
                var customersFromDb = await _context.Customers.ToListAsync();
                foreach (var customer in customersFromDb)
                {
                    Customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        // Mettre � jour : Modifier un client existant
        private async Task UpdateCustomer()
        {
            try
            {
                // Validation des entr�es
                if (!ValidateCustomerInput())
                {
                    return;
                }

                if (SelectedCustomer != null)
                {
                    var customerToUpdate = _context.Customers.FirstOrDefault(c => c.Id == SelectedCustomer.Id);
                    if (customerToUpdate != null)
                    {
                        customerToUpdate.Name = Name;
                        customerToUpdate.Email = Email;
                        customerToUpdate.Address = Address;

                        await _context.SaveChangesAsync(); // Sauvegarder les modifications
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        // Supprimer : Supprimer un client de la base de donn�es
        private async Task DeleteCustomer(object parameter)
        {
            try
            {
                var customerToDelete = _context.Customers.FirstOrDefault(c => c.Id == (int)parameter);
                if (customerToDelete != null)
                {
                    _context.Customers.Remove(customerToDelete); // Supprimer du DB
                    await _context.SaveChangesAsync(); // Sauvegarder les changements

                    // Supprimer de la ObservableCollection pour mettre � jour l'UI
                    Customers.Remove(customerToDelete);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        // Valider les entr�es du client
        private bool ValidateCustomerInput()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Address);
        }

        // G�rer les erreurs
        private void HandleError(Exception ex)
        {
            // Afficher un message d'erreur ou enregistrer l'erreur
            Console.WriteLine($"Error: {ex.Message}");
        }

        // V�rifier si l'enregistrement est possible
        private bool CanSaveCustomer(object parameter)
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Address);
        }

        // V�rifier si la suppression est possible
        private bool CanDeleteCustomer(object parameter)
        {
            return SelectedCustomer != null;
        }

        // V�rifier si la mise � jour est possible
        private bool CanUpdateCustomer(object parameter)
        {
            return SelectedCustomer != null && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Address);
        }

        // Notifier des changements de propri�t�s
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Lib�rer les ressources
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
