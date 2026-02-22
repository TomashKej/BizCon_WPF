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
    public class NewAisleViewModel : JedenViewModel<Aisle>
    {
        // The list of Sites for selection
        public ObservableCollection<Site> Sites { get; set; }

        #region Konstruktor
        public NewAisleViewModel()
            : base()
        {
            base.DisplayName = "Aisle";
            item = new Aisle();

            Sites = new ObservableCollection<Site>(
                bizConDbEntities.Site.Where(s => s.IsActive == true).ToList()
            );
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string AisleName
        {
            get => item.AisleName;
            set { item.AisleName = value; OnPropertyChanged(() => AisleName); }
        }

        private int? _selectedSiteId;
        public int? SelectedSiteId
        {
            get => _selectedSiteId;
            set 
            {
                _selectedSiteId = value; 
                item.SiteId = value;                        // Fk do Site
                OnPropertyChanged(() => SelectedSiteId); 
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
            if (propertyName == nameof(AisleName))
            {
                if (string.IsNullOrWhiteSpace(AisleName)) return "Aisle name field cannot be empty";
            }
            if (propertyName == nameof(SelectedSiteId))
            {
                if (!SelectedSiteId.HasValue) return "Site field cannot be empty";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Aisle.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
