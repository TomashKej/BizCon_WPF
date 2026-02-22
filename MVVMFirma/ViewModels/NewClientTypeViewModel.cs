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
    public class NewClientTypeViewModel : JedenViewModel<ClientType>
    {
        #region Konstruktor
        public NewClientTypeViewModel()
            : base()
        {
            base.DisplayName = "Client type";
            item = new ClientType();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string ClientType1
        {
            get => item.ClientType1;
            set { item.ClientType1 = value; OnPropertyChanged(() => ClientType1); }
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
            if (propertyName == nameof(ClientType1))
            {
                if (string.IsNullOrEmpty(ClientType1)) return "Client type field cannot be empty";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.ClientType.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
