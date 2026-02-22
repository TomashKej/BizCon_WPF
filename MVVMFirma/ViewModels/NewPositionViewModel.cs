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
    public class NewPositionViewModel : JedenViewModel<Position>
    {

        // ObservableCllection is a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Grade> Grades { get; set; }
        #region Konstruktor
        public NewPositionViewModel()
            : base()
        {
            base.DisplayName = "Position";
            item = new Position();

            Departments = new ObservableCollection<Department>(
                bizConDbEntities.Department.Where(d => d.IsActive == true).ToList()
            );
            Grades = new ObservableCollection<Grade>(
                bizConDbEntities.Grade.Where(g => g.IsActive == true).ToList()
            );
        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties

        public string PositionName
        {
            get => item.PositionName;
            set { item.PositionName = value; OnPropertyChanged(() => PositionName); }
        }
        private int? _selectedDepartmentId;
        public int? SelectedDepartmentId
        {
            get => _selectedDepartmentId;
            set 
            {
                _selectedDepartmentId = value; 
                item.DepartmentId = value;                      // Fk do Department
                OnPropertyChanged(() => SelectedDepartmentId); 
            }
        }
        private int? _selectedGradeId;
        public int? SelectedGradeId
        {
            get => _selectedGradeId;
            set 
            {
                _selectedGradeId = value; 
                item.GradeId = value;                      // Fk do Grade
                OnPropertyChanged(() => SelectedGradeId); 
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
            if (propertyName == nameof(PositionName))
            {
                if (string.IsNullOrWhiteSpace(PositionName)) return "Position name field cannot be empty";
            }
            if (propertyName == nameof(SelectedDepartmentId))
            {
                if (!SelectedDepartmentId.HasValue) return "Department field cannot be empty";
            }
            if (propertyName == nameof(SelectedGradeId))
            {
                if (!SelectedGradeId.HasValue) return "Grade field cannot be empty";
            }
            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;

            bizConDbEntities.Position.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
