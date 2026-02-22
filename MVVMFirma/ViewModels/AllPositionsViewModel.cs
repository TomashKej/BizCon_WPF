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
    public class AllPositionsViewModel : WszystkieViewModel<PositionForAllVIew>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<PositionForAllVIew>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from position in bizConDbEntities.Position
                    select new PositionForAllVIew
                    {
                        PositionName = position.PositionName,
                        DepartmentName = position.Department.DepartmentName,
                        GradeName = position.Grade.GradeName,
                        Salary =
                                position.Grade != null
                                    ? (position.Grade.HourlyWage.HasValue && position.Grade.HourlyWage > 0
                                        ? position.Grade.HourlyWage.Value * 160
                                        : position.Grade.AnnualWage ?? 0)
                                    : 0,
                        Notes = position.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllPositionsViewModel()
            : base()
        {
            base.DisplayName = "Positions";
        }
        #endregion
        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Position Name", "Department", "Grade", "Salary" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Position Name")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.OrderBy(item => item.PositionName));
            }
            else if (SortPhrase == "Department")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.OrderBy(item => item.DepartmentName));
            }
            else if (SortPhrase == "Grade")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.OrderBy(item => item.GradeName));
            }
            else if (SortPhrase == "Salary")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.OrderBy(item => item.Salary));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Position Name", "Department", "Grade", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Position Name")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.Where(item => item.PositionName != null && item.PositionName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Department")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.Where(item => item.DepartmentName != null && item.DepartmentName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Grade")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.Where(item => item.GradeName != null && item.GradeName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes")
            {
                List = new ObservableCollection<PositionForAllVIew>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewPositionViewModel();
        }
        #endregion

    }
}