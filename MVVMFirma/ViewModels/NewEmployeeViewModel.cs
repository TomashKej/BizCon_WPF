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
    public class NewEmployeeViewModel : JedenViewModel<Employee> //T jest towarem
    {
        // The following ObservableCollections can be populated in the constructor or through a Load method and used to bind to ComboBoxes in the view.
        public ObservableCollection<Position> Positions { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Grade> Grades { get; set; }
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<Manager> Managers { get; set; }
        public ObservableCollection<Site> Sites { get; set; }

        #region Konstruktor
        public NewEmployeeViewModel()
            : base()
        {
            base.DisplayName = "Employee";
            item = new Employee();

            // Load active Positions
            Positions = new ObservableCollection<Position>(
                bizConDbEntities.Position.Where(p => p.IsActive == true).ToList()
            );
            // Load active Departments
            Departments = new ObservableCollection<Department>(
                bizConDbEntities.Department.Where(d => d.IsActive == true).ToList()
            );
            // Load active Grades
            Grades = new ObservableCollection<Grade>(
                bizConDbEntities.Grade.Where(g => g.IsActive == true).ToList()
            );
            // Load active Addresses
            Addresses = new ObservableCollection<Address>(
                bizConDbEntities.Address.Where(a => a.IsActive == true).ToList()
            );
            // Load active Managers
            Managers = new ObservableCollection<Manager>(
                bizConDbEntities.Manager.Where(m => m.IsActive == true).ToList()
            );

            // Load active Sites
            Sites = new ObservableCollection<Site>(
                bizConDbEntities.Site.Where(s => s.IsActive == true).ToList()
            );
            DateOfBirth = DateTime.Now;
            HireDate = DateTime.Now;

        }
        #endregion
        #region Wlasciwosci
        //dla kazdego pola towaru, ktore bedziemy dodawac, dodajemy properties

        public string EmployeeNumber
        {
            get => item.EmployeeNumber;
            set { item.EmployeeNumber = value; OnPropertyChanged(() => EmployeeNumber); }
        }
        public string FirstName
        {
            get => item.FirstName;
            set { item.FirstName = value; OnPropertyChanged(() => FirstName); }
        }
        public string MiddleName
        {
            get => item.MiddleName;
            set { item.MiddleName = value; OnPropertyChanged(() => MiddleName); }
        }
        public string LastName
        {
            get => item.LastName;
            set { item.LastName = value; OnPropertyChanged(() => LastName); }
        }
        public string Sex
        {
            get => item.Sex;
            set { item.Sex = value; OnPropertyChanged(() => Sex); }
        }
        public DateTime DateOfBirth
        {
            get => item.DateOfBirth;
            set { item.DateOfBirth = value; OnPropertyChanged(() => DateOfBirth); }
        }
        private int _selectedPositionId;
        public int SelectedPositionId
        {
            get => _selectedPositionId;
            set
            {
                _selectedPositionId = value;
                item.PositionId = value;                        // FK to Position
                OnPropertyChanged(() => SelectedPositionId);
            }
        }
        private int _selectedDepartmentId;
        public int SelectedDepartmentId
        {
            get => _selectedDepartmentId;
            set
            {
                _selectedDepartmentId = value;
                item.DepartmentId = value;                        // FK to Department
                OnPropertyChanged(() => SelectedDepartmentId);
            }
        }
     
        private int _selectedGradeId;
        public int SelectedGradeId
        {
            get => _selectedGradeId;
            set
            {
                _selectedGradeId = value;
                item.GradeId = value;                               // FK to Grade
                OnPropertyChanged(() => SelectedGradeId);
            }
        }
        public DateTime HireDate
        {
            get => item.HireDate;
            set { item.HireDate = value; OnPropertyChanged(() => HireDate); }
        }
        public DateTime? TerminationDate
        {
            get => item.TerminationDate;
            set { item.TerminationDate = value; OnPropertyChanged(() => TerminationDate); }
        }
        private int _selectedAddressId;
        public int SelectedAddressId
        {
            get => _selectedAddressId;
            set
            {
                _selectedAddressId = value;
                item.AddressId = value;                        // FK to Address
                OnPropertyChanged(() => SelectedAddressId);
            }
        }

        public string PhoneNumber
        {
            get => item.PhoneNumber;
            set { item.PhoneNumber = value; OnPropertyChanged(() => PhoneNumber); }
        }
        public string EmailAddress
        {
            get => item.EmailAddress;
            set { item.EmailAddress = value; OnPropertyChanged(() => EmailAddress); }
        }
        private int? _selectedManagerId;
        public int? SelectedManagerId
        {
            get => _selectedManagerId;
            set
            {
                _selectedManagerId = value;
                item.ManagerId = value;                              // FK to Manager (Employee)
                OnPropertyChanged(() => SelectedManagerId);
            }
        }
        private int _selectedSiteId;
        public int SelectedSiteId
        {
            get => _selectedSiteId;
            set
            {
                _selectedSiteId = value;
                item.SiteId = value;                                // FK to Site
                OnPropertyChanged(() => SelectedSiteId);
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
            if (propertyName == nameof(EmployeeNumber))
            {
                if (string.IsNullOrWhiteSpace(EmployeeNumber))
                    return "Employee number cannot be empty.";
            }

            if (propertyName == nameof(FirstName))
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                    return "First name cannot be empty.";
            }

            if (propertyName == nameof(LastName))
            {
                if (string.IsNullOrWhiteSpace(LastName))
                    return "Last name cannot be empty.";
            }

            if (propertyName == nameof(Sex))
            {
                if (string.IsNullOrWhiteSpace(Sex))
                    return "Sex must be selected.";
            }

            if (propertyName == nameof(DateOfBirth))
            {
                if (DateOfBirth == default(DateTime))
                    return "Date of birth must be provided.";
            }

            if (propertyName == nameof(SelectedPositionId))
            {
                if (SelectedPositionId <= 0)
                    return "Position must be selected.";
            }

            if (propertyName == nameof(SelectedDepartmentId))
            {
                if (SelectedDepartmentId <= 0)
                    return "Department must be selected.";
            }

            if (propertyName == nameof(SelectedGradeId))
            {
                if (SelectedGradeId <= 0)
                    return "Grade must be selected.";
            }

            if (propertyName == nameof(HireDate))
            {
                if (HireDate == default(DateTime))
                    return "Hire date must be provided.";
            }

            if (propertyName == nameof(SelectedAddressId))
            {
                if (SelectedAddressId <= 0)
                    return "Address must be selected.";
            }

            if (propertyName == nameof(PhoneNumber))
            {
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    return "Phone number cannot be empty.";
            }

            if (propertyName == nameof(EmailAddress))
            {
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    return "Email address cannot be empty.";
            }

            if (propertyName == nameof(SelectedSiteId))
            {
                if (SelectedSiteId <= 0)
                    return "Site must be selected.";
            }

            return String.Empty;
        }


        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST"; //w przyszlosci bedzie to zalogowany uzytkownik
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Employee.Add(item);//to jest dodanie towaru do kolekcji towarow
            bizConDbEntities.SaveChanges();  //to jest zapisanie danych do bazy danych
        }

        #endregion

    }
}
