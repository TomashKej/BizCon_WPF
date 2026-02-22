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
    public class NewLocationViewModel : JedenViewModel<Location>
    {
        // The following ObservableCollections will hold the active Sites and Aisles for selection in the UI
        public ObservableCollection<Site> Sites { get; set; }
        public ObservableCollection<Aisle> Aisles { get; set; }
        #region Konstruktor
        public NewLocationViewModel()
            : base()
        {
            base.DisplayName = "Location";
            item = new Location();

            // The following code loads active Sites and Aisles into their respective ObservableCollections
            Sites = new ObservableCollection<Site>(
                bizConDbEntities.Site.Where(s => s.IsActive == true).ToList()
            );

            Aisles = new ObservableCollection<Aisle>(
                bizConDbEntities.Aisle.Where(a => a.IsActive == true).ToList()
            );
        }
        #endregion
        #region Wlasciwosci
        //We add properties for each field we will be adding

        public string LocationName
        {
            get => item.LocationName;
            set { item.LocationName = value; OnPropertyChanged(() => LocationName); }
        }
        private int _selectedSiteId;
        public int SelectedSiteId
        {
            get => _selectedSiteId;
            set
            {
                _selectedSiteId = value;
                item.SiteId = value; // FK to Site
                OnPropertyChanged(() => SelectedSiteId);
            }
        }
        private int? _selectedAisleId;
        public int? SelectedAisleId
        {
            get => _selectedAisleId;
            set
            {
                _selectedAisleId = value;
                item.AisleId = value; // FK to Aisle
                OnPropertyChanged(() => SelectedAisleId);
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
            if (propertyName == nameof(LocationName))
            {
                if (string.IsNullOrWhiteSpace(LocationName)) return "Location field cannot be empty";
            }
            if (propertyName == nameof(SelectedSiteId))
            {
                if (SelectedSiteId <= 0) return "Site field cannot be empty and must be greater than 0";
            }
            if (propertyName == nameof(SelectedAisleId))
            {
                if (!SelectedAisleId.HasValue) return "Aisle field cannot be empty";
            }
            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Location.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
