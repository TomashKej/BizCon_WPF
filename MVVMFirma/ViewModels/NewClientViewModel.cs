using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MVVMFirma.ViewModels
{
    internal class NewClientViewModel : JedenViewModel<Client>
    {
        // The following collections will hold active Addresses and ClientTypes for selection
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<ClientType> ClientTypes { get; set; }

        // --- Constructor ---
        #region Constructor
        public NewClientViewModel()
            : base()
        {
            base.DisplayName = "Client";
            item = new Client();

            // Load active Addresses into ObservableCollection
            Addresses = new ObservableCollection<Address>(
                bizConDbEntities.Address.Where(a => a.IsActive == true).ToList()
            );
            ClientTypes = new ObservableCollection<ClientType>(
                bizConDbEntities.ClientType.Where(a => a.IsActive == true).ToList()
            ); 
        }
        #endregion

        // --- Properties ---
        #region Properties
        public string ClientName
        {
            get => item.ClientName;
            set { item.ClientName = value; OnPropertyChanged(() => ClientName); }
        }
        public string PhoneNumber
        {
            get => item.PhoneNumber;
            set { item.PhoneNumber = value; OnPropertyChanged(() => PhoneNumber); }
        }
        public string EmailAddress
        {
            get => item.EmailAddress;
            set { item.EmailAddress = value; OnPropertyChanged(() => EmailAddress); }
        }
        private int? _selectedAddressId;
        public int? SelectedAddressId
        {
            get => _selectedAddressId;
            set 
            { 
                item.AddressId = value; 
                _selectedAddressId = value;
                OnPropertyChanged(() => SelectedAddressId); 
            }
        }
        private int? _selectedClientTypeId;
        public int? SelectedClientTypeId
        {
            get => _selectedClientTypeId;
            set 
            { 
                item.ClientTypeId = value; 
                _selectedClientTypeId = value;
                OnPropertyChanged(() => SelectedClientTypeId); 
            }
        }
        public string Notes
        {
            get => item.Notes;
            set { item.Notes = value; OnPropertyChanged(() => Notes); }
        }
        #endregion
        #region Commands
        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(ClientName))
            {
                if (string.IsNullOrWhiteSpace(ClientName))
                    return "Client name cannot be empty.";
            }
            else if (propertyName == nameof(PhoneNumber))
            {
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    return "Phone no. cannot be empty.";
            }
            else if (propertyName == nameof(EmailAddress))
            {
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    return "Email cannot be empty.";
            }
            else if (propertyName == nameof(SelectedAddressId))
            {
                if (!SelectedAddressId.HasValue)
                    return "Address cannot be empty.";
            }
            else if (propertyName == nameof(SelectedClientTypeId))
            {
                if (!SelectedClientTypeId.HasValue)
                    return "Client type field cannot be empty.";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = "SYSTEM_TEST"; // in the future, this will be the logged-in user

            bizConDbEntities.Client.Add(item);
            bizConDbEntities.SaveChanges();
        }
        #endregion 
    }
}
