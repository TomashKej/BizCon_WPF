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
    public class NowyTowarViewModel : JedenViewModel<Product> //T jest towarem
    {
        #region Konstruktor
        public NowyTowarViewModel()
            :base()
        {
            base.DisplayName = "Product";
            item = new Product();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola towaru, ktore bedziemy dodawac, dodajemy properties

        public string ProductName
        {
            get => item.ProductName;
            set { item.ProductName = value; OnPropertyChanged(() => ProductName); }
        }

        public string Batch
        {
            get => item.Batch;
            set { item.Batch = value; OnPropertyChanged(() => Batch); }
        }

        public string Sku
        {
            get => item.Sku;
            set { item.Sku = value; OnPropertyChanged(() => Sku); }
        }

        public decimal? UnitPrice
        {
            get => item.UnitPrice;
            set { item.UnitPrice = value; OnPropertyChanged(() => UnitPrice); }
        }

        public int? Quantity
        {
            get => item.Quantity;
            set { item.Quantity = value; OnPropertyChanged(() => Quantity); }
        }

        public int? CategoryId
        {
            get => item.CategoryId;
            set { item.CategoryId = value; OnPropertyChanged(() => CategoryId); }
        }

        public int? ClientId
        {
            get => item.ClientId;
            set { item.ClientId = value; OnPropertyChanged(() => ClientId); }
        }

        public int? SiteId
        {
            get => item.SiteId;
            set { item.SiteId = value; OnPropertyChanged(() => SiteId); }
        }

        public int? AisleId
        {
            get => item.AisleId;
            set { item.AisleId = value; OnPropertyChanged(() => AisleId); }
        }

        public int? LocationId
        {
            get => item.LocationId;
            set { item.LocationId = value; OnPropertyChanged(() => LocationId); }
        }
        #endregion

        #region Komendy

        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(ProductName))
            {
                if (string.IsNullOrWhiteSpace(ProductName)) return "Product name field cannot be empty";
            }

            else if (propertyName == nameof(Batch))
            {
                if (string.IsNullOrWhiteSpace(Batch)) return "Batch field cannot be empty";
            }

            else if(propertyName == nameof(Sku))
            {
                if (string.IsNullOrWhiteSpace(Sku)) return "Sku field cannot be empty";
            }

            else if(propertyName == nameof(UnitPrice))
            {
                if ((UnitPrice <= 0) || (!UnitPrice.HasValue)) return "Unit price field must be a number which is greater than 0";
            }

            else if(propertyName == nameof(Quantity))
            {
                if ((Quantity < 0) || (!Quantity.HasValue)) return "Quantity field must be a number greater than or equal to 0";
            }

            else if(propertyName == nameof(CategoryId))
            {
                if (!CategoryId.HasValue) return "CategoryId field must cannot be empty";
            }

            else if(propertyName == nameof(ClientId))
            {
                if (!ClientId.HasValue) return "ClientId field must cannot be empty";
            }

            else if(propertyName == nameof(SiteId))
            {
                if (!SiteId.HasValue) return "SiteId field must cannot be empty";
            }

            else if(propertyName == nameof(AisleId))
            {
                if (!AisleId.HasValue) return "AisleId field must cannot be empty";
            }

            else if(propertyName == nameof(LocationId))
            {
                if (!LocationId.HasValue) return "LocationId field must cannot be empty";
            }

            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Product.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
