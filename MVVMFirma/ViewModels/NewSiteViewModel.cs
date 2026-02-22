using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewSiteViewModel : JedenViewModel<Site>
    {
        // the list of addresses to choose from
        public ObservableCollection<Address> Addresses { get; set; }

        #region Konstruktor
        public NewSiteViewModel()
            : base()
        {
            base.DisplayName = "Site";
            item = new Site();

            Addresses = new ObservableCollection<Address>(
                bizConDbEntities.Address.Where(a => a.IsActive == true).ToList()
                );
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string SiteName
        {
            get => item.SiteName;
            set { item.SiteName = value; OnPropertyChanged(() => SiteName); }
        }
        private int? _selectedAddressId;
        public int? SelectedAddressId
        {
            get => _selectedAddressId;
            set 
            {
                _selectedAddressId = value;
                item.AddressId = value;
                OnPropertyChanged(() => SelectedAddressId); 
            }
        }
        public string Notes
        {
            get => item.Notes;
            set { item.Notes = value; OnPropertyChanged(() => Notes); }
        }



        #endregion
        #region Komendy

        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(SiteName))
            {
                if (string.IsNullOrWhiteSpace(SiteName)) return "Site name field cannot be empty";
            }
            if (propertyName == nameof(SelectedAddressId))
            {
                if (!SelectedAddressId.HasValue) return "Address field cannot be empty";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Site.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
