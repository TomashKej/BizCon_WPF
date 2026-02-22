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
    public class NewManagerViewModel : JedenViewModel<Manager>
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        #region Konstruktor
        public NewManagerViewModel()
            : base()
        {
            base.DisplayName = "Manager";
            item = new Manager();

            // Ladujemy aktywnych pracowników do ObservableCollection
            Employees = new ObservableCollection<Employee>(
                bizConDbEntities.Employee.Where(e => e.IsActive == true).ToList()
            );
            // Ladujemy aktywne działy do ObservableCollection
            Departments = new ObservableCollection<Department>(
                bizConDbEntities.Department.Where(d => d.IsActive == true).ToList()
            );
        }
        #endregion

        #region List

        #endregion

        #region Wlasciwosci
        //dla kazdego pola ktore bedziemy dodawac, dodajemy properties
        private int _selectedEmployeeId;
        public int SelectedEmployeeId
        {
            get => _selectedEmployeeId;
            set 
            { 
                _selectedEmployeeId = value; 
                item.EmployeeId = value;                        // Fk do Employee
                OnPropertyChanged(() => SelectedEmployeeId); 
            }
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

        public string Notes
        {
            get => item.Notes;
            set { item.Notes = value; OnPropertyChanged(() => Notes); }
        }

        #endregion
        #region Komendy
        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(SelectedEmployeeId))
            {
                if (SelectedEmployeeId <= 0) return "Employee ID field cannot be empty, and value must be greater than 0";
            }
            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;

            bizConDbEntities.Manager.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }
        #endregion

    }
}
