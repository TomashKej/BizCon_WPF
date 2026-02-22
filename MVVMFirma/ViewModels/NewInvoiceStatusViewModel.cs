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
    public class NewInvoiceStatusViewModel : JedenViewModel<InvoiceStatus>
    {
        #region Konstruktor
        public NewInvoiceStatusViewModel()
            : base()
        {
            base.DisplayName = "InvoiceStatus";
            item = new InvoiceStatus();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string InvoiceStatusName
        {
            get => item.InvoiceStatusName;
            set { item.InvoiceStatusName = value; OnPropertyChanged(() => InvoiceStatusName); }
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
            if (propertyName == nameof(InvoiceStatusName))
            {
                if (string.IsNullOrWhiteSpace(InvoiceStatusName)) return "Status name field cannot be empty";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.InvoiceStatus.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
