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
    public class NewCategoryViewModel : JedenViewModel<Category>
    {
        #region Konstruktor
        public NewCategoryViewModel()
            : base()
        {
            base.DisplayName = "Category";
            item = new Category();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string CategoryName
        {
            get => item.CategoryName;
            set { item.CategoryName = value; OnPropertyChanged(() => CategoryName); }
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
            if (propertyName == nameof(CategoryName))
            {
                if (string.IsNullOrEmpty(CategoryName)) return "Category name field cannot be empty";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Category.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
