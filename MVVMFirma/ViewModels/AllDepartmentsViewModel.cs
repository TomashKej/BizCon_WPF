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
    public class AllDepartmentsViewModel : WszystkieViewModel<DepartmentForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<DepartmentForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from department in bizConDbEntities.Department
                    select new DepartmentForAllView
                    {
                        DepartmentName = department.DepartmentName,
                        Notes = department.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllDepartmentsViewModel()
            : base()
        {
            base.DisplayName = "Departments";
        }
        #endregion

        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Department" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Department") // if we sort by department
            {
                List = new ObservableCollection<DepartmentForAllView>(List.OrderBy(item => item.DepartmentName));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Department", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Department")
            {
                List = new ObservableCollection<DepartmentForAllView>(List.Where(item => item.DepartmentName != null && item.DepartmentName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes")
            {
                List = new ObservableCollection<DepartmentForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewDepartmentViewModel();
        }
        #endregion
    }
}