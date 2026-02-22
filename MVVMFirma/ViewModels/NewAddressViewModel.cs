using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewAddressViewModel : JedenViewModel<Address>
    {
        #region Konstruktor
        public NewAddressViewModel()
            : base()
        {
            base.DisplayName = "Address";
            item = new Address();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string AddressLine1
        {
            get => item.AddressLine1;
            set { item.AddressLine1 = value; OnPropertyChanged(() => AddressLine1); }
        }
        public string AddressLine2
        {
            get => item.AddressLine2;
            set { item.AddressLine2 = value; OnPropertyChanged(() => AddressLine2); }
        }
        public string City
        {
            get => item.City;
            set { item.City = value; OnPropertyChanged(() => City); }
        }
        public string PostCode
        {
            get => item.PostCode;
            set { item.PostCode = value; OnPropertyChanged(() => PostCode); }
        }
        public string County
        {
            get => item.County;
            set { item.County = value; OnPropertyChanged(() => County); }
        }
        public string Country
        {
            get => item.Country;
            set { item.Country = value; OnPropertyChanged(() => Country); }
        }

        #endregion
        #region Komendy
        protected override string ValidateProperty(string propertyName)
        { 
            if (propertyName == nameof(AddressLine1))
            {
                if (string.IsNullOrWhiteSpace(AddressLine1))
                    return "Address Line 1 cannot be empty.";
            }
            else if (propertyName == nameof(City))
            {
                if (string.IsNullOrWhiteSpace(City))
                    return "City cannot be empty.";
            }
            else if (propertyName == nameof(PostCode))
            {
                if (string.IsNullOrWhiteSpace(PostCode))
                    return "Post Code cannot be empty.";
            }
            else if (propertyName == nameof(County))
            {
                if (string.IsNullOrWhiteSpace(County))
                    return "County cannot be empty.";
            }
            else if (propertyName == nameof(Country))
            {
                if (string.IsNullOrWhiteSpace(Country))
                    return "Country cannot be empty.";
            }
            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Address.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
