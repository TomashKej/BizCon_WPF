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
    public class NewGradeViewModel : JedenViewModel<Grade>
    {
        #region Konstruktor
        public NewGradeViewModel()
            : base()
        {
            base.DisplayName = "Grade";
            item = new Grade();
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string GradeName
        {
            get => item.GradeName;
            set { item.GradeName = value; OnPropertyChanged(() => GradeName); }
        }
        public decimal? HourlyWage
        {
            get => item.HourlyWage;
            set { item.HourlyWage = value; OnPropertyChanged(() => HourlyWage); }
        }
        public decimal? AnnualWage
        {
            get => item.AnnualWage;
            set { item.AnnualWage = value; OnPropertyChanged(() => AnnualWage); }
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
            if (propertyName == nameof(GradeName))
            {
                if (string.IsNullOrWhiteSpace(GradeName))
                    return "GradeName Line cannot be empty.";
            }
            return String.Empty;
        }
        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Grade.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
