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
    public class NewPaymentMethodViewModel : JedenViewModel<PaymentMethod>
    {
        #region Konstruktor
        public NewPaymentMethodViewModel()
            : base()
        {
            base.DisplayName = "PaymentMethod";
            item = new PaymentMethod();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string PaymentMethodName
        {
            get => item.PaymentMethodName;
            set { item.PaymentMethodName = value; OnPropertyChanged(() => PaymentMethodName); }
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
            if (propertyName == nameof(PaymentMethodName))
            {
                if (string.IsNullOrWhiteSpace(PaymentMethodName)) return "Payment method name cannot be empty";
            }
            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.PaymentMethod.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
