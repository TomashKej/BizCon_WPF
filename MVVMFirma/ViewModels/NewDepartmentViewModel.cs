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
    public class NewDepartmentViewModel : JedenViewModel<Department>
    {
        #region Konstruktor
        public NewDepartmentViewModel()
            : base()
        {
            base.DisplayName = "Department";
            item = new Department();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string DepartmentName
        {
            get => item.DepartmentName;
            set { item.DepartmentName = value; OnPropertyChanged(() => DepartmentName); }
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
            if (propertyName == nameof(DepartmentName))
            {
                if (string.IsNullOrWhiteSpace(DepartmentName)) return "Department field cannot be empty";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Department.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
