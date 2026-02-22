using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Models;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.ViewModels.Abstract;
using System.Windows;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllEmployeesViewModel : WszystkieViewModel<EmployeeForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<EmployeeForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy pracownika
                    from Employee in bizConDbEntities.Employee
                    select new EmployeeForAllView
                    {
                        EmployeeNumber = Employee.EmployeeNumber,
                        FullName = Employee.FirstName + (Employee.MiddleName != null ? " " + Employee.MiddleName + " " : " ")  + Employee.LastName,
                        Sex = Employee.Sex,
                        DateOfBirth = Employee.DateOfBirth,
                        PositionName = Employee.Position.PositionName,
                        DepartmentName = Employee.Department.DepartmentName,
                        GradeName = Employee.Grade.GradeName,
                        HireDate = Employee.HireDate,
                        TerminationDate = Employee.TerminationDate != null ? Employee.TerminationDate : null ,
                        PhoneNumber = Employee.PhoneNumber,
                        EmailAddress = Employee.EmailAddress,
                        Address = Employee.Address.AddressLine1,
                        ManagerName = Employee.Manager != null ? Employee.Manager.Employee1.FirstName + " " + Employee.Manager.Employee1.LastName : "No Manager assigned",
                    }
                );
            
        }
        #endregion
        #region Konstruktor
        public AllEmployeesViewModel()
            : base()
        {
            base.DisplayName = "Employees";
        }
        #endregion

        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Employee Number", "Full Name", "Sex", 
                                      "Date of Birth", "Position", "Department", 
                                      "Grade", "Hire Date", "Termination Date", "Manager Name"};
        }
        // -- The following methods implement sorting --
        public override void Sort()
        {
            if (SortPhrase == "Employee Number") // if we sort by employee number
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.EmployeeNumber));
            }
            else if (SortPhrase == "Full Name") // if we sort by full name
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.FullName));
            }
            else if (SortPhrase == "Sex")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.Sex));
            }
            else if (SortPhrase == "Date of Birth")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.DateOfBirth));
            }
            else if (SortPhrase == "Position")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.PositionName));
            }
            else if (SortPhrase == "Department")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.DepartmentName));
            }
            else if (SortPhrase == "Grade")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.GradeName));
            }
            else if (SortPhrase == "Hire Date")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.HireDate));
            }
            else if (SortPhrase == "Termination Date")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.TerminationDate));
            }
            else if (SortPhrase == "Manager Name")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.ManagerName));
            }
        }
        // The following method decides by what we can find
        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Employee Number", "Full Name", "Sex", 
                                      "Date of Birth", "Position", "Department", 
                                      "Grade", "Hire Date", "Termination Date", 
                                      "Manager Name", "Phone Number", "Email Address", "Address" };
        }
        // -- The following methods implement finding --
        public override void Find()
        {
            if (FindPhrase == "Employee Number") // if we search by employee number
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.EmployeeNumber != null && item.EmployeeNumber.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Full Name") // if we search by full name
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.FullName != null && item.FullName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Sex")
            { 
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.Sex != null && item.Sex.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Date of Birth")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.DateOfBirth.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Position")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.PositionName != null && item.PositionName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Department")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.DepartmentName != null && item.DepartmentName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Grade")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.GradeName != null && item.GradeName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Hire Date")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.HireDate.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Termination Date")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.TerminationDate != null && item.TerminationDate.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Manager Name")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.ManagerName != null && item.ManagerName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Phone Number")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.PhoneNumber != null && item.PhoneNumber.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Email Address")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.EmailAddress != null && item.EmailAddress.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Address")
            {
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.Address != null && item.Address.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewEmployeeViewModel();
        }
        #endregion
    }
}