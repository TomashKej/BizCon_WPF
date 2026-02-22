using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Models;
using MVVMFirma.Helper;
using System.Windows.Input;
using System.Windows;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllManagersViewModel : WszystkieViewModel<ManagerForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<ManagerForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from manager in bizConDbEntities.Manager
                    select new ManagerForAllView
                    {
                        EmployeeNumber = manager.Employee1.EmployeeNumber,
                        EmployeeName = manager.Employee1.FirstName + " " + manager.Employee1.LastName,
                        DepartmentName = manager.Department.DepartmentName,
                        Email = manager.Employee1.EmailAddress,
                        PhoneNumber = manager.Employee1.PhoneNumber,
                        Salary =
                            manager.Employee1 != null &&
                            manager.Employee1.Grade != null
                                ? (manager.Employee1.Grade.HourlyWage.HasValue && manager.Employee1.Grade.HourlyWage > 0
                                    ? manager.Employee1.Grade.HourlyWage.Value * 160
                                    : manager.Employee1.Grade.AnnualWage)
                                    : 0,
                        Notes = manager.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllManagersViewModel()
            : base()
        {
            base.DisplayName = "Managers";
        }
        #endregion

        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Employee Number", "Employee Name", "Department", "Salary" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Employee Number") // if we sort by employee number
            {
                List = new ObservableCollection<ManagerForAllView>(List.OrderBy(item => item.EmployeeNumber));
            }
            else if (SortPhrase == "Employee Name") // if we sort by employee name
            {
                List = new ObservableCollection<ManagerForAllView>(List.OrderBy(item => item.EmployeeName));
            }
            else if (SortPhrase == "Department") // if we sort by department
            {
                List = new ObservableCollection<ManagerForAllView>(List.OrderBy(item => item.DepartmentName));
            }
            else if (SortPhrase == "Salary") // if we sort by salary
            {
                List = new ObservableCollection<ManagerForAllView>(List.OrderBy(item => item.Salary));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Employee Number", "Employee Name", "Department", "Email", "Phone Number", "Salary", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Employee Number") // if we search by employee number
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.EmployeeNumber != null && item.EmployeeNumber.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Employee Name") // if we search by employee name
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.EmployeeName != null && item.EmployeeName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Department") // if we search by department
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.DepartmentName != null && item.DepartmentName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Email") // if we search by email
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.Email != null && item.Email.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Phone Number") // if we search by phone number
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.PhoneNumber != null && item.PhoneNumber.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Salary") // if we search by salary
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.Salary != null && item.Salary.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes") // if we search by notes
            {
                List = new ObservableCollection<ManagerForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewManagerViewModel();
        }
        #endregion
    }
}