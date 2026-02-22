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
    public class AllGradesViewModel : WszystkieViewModel<GradeForAllViews>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<GradeForAllViews>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from grade in bizConDbEntities.Grade
                    select new GradeForAllViews
                    {
                        GradeName = grade.GradeName,
                        HourlyWage = grade.HourlyWage != null ? Math.Round(grade.HourlyWage.Value, 2) : (decimal?)null,
                        AnnualWage = grade.AnnualWage != null ? Math.Round(grade.AnnualWage.Value, 2) : (decimal?)null,
                        Notes = grade.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllGradesViewModel()
            : base()
        {
            base.DisplayName = "Grades";
        }
        #endregion

        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Grade Name", "Hourly Wage", "Annual Wage" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Grade Name") // if we sort by grade name
            {
                List = new ObservableCollection<GradeForAllViews>(List.OrderBy(item => item.GradeName));
            }
            else if (SortPhrase == "Hourly Wage") // if we sort by hourly wage
            {
                List = new ObservableCollection<GradeForAllViews>(List.OrderBy(item => item.HourlyWage));
            }
            else if (SortPhrase == "Annual Wage") // if we sort by annual wage
            {
                List = new ObservableCollection<GradeForAllViews>(List.OrderBy(item => item.AnnualWage));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Grade Name", "Hourly Wage", "Annual Wage", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Grade Name") // if we search by grade name
            {
                List = new ObservableCollection<GradeForAllViews>(List.Where(item => item.GradeName != null && item.GradeName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Hourly Wage") // if we search by hourly wage
            {
                // Try to parse the input as decimal
                decimal wage;
                if (decimal.TryParse(FindTextBox, out wage))
                {
                    List = new ObservableCollection<GradeForAllViews>(List.Where(item => item.HourlyWage != null && item.HourlyWage == wage));
                }
                else
                // Show an error message if parsing fails
                {
                    MessageBox.Show("Invalid input for Hourly Wage search.");
                }
            }
            else if (FindPhrase == "Annual Wage") // if we search by annual wage
            {
                decimal wage;
                if (decimal.TryParse(FindTextBox, out wage))
                {
                    List = new ObservableCollection<GradeForAllViews>(List.Where(item => item.AnnualWage != null && item.AnnualWage == wage));
                }
                else
                {
                    MessageBox.Show("Invalid input for Annual Wage search.");
                }
            }
            else if (FindPhrase == "Notes") // if we search by notes
            {
                List = new ObservableCollection<GradeForAllViews>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewGradeViewModel();
        }
        #endregion
    }
}