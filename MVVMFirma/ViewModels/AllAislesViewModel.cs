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
    public class AllAislesViewModel : WszystkieViewModel<AisleForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<AisleForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from aisle in bizConDbEntities.Aisle
                    select new AisleForAllView
                    {
                        AisleName = aisle.AisleName,
                        SiteName = aisle.Site.SiteName,
                        Notes = aisle.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllAislesViewModel()
            : base()
        {
            base.DisplayName = "Aisles";
        }
        #endregion
        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Aisle", "Site"};
        }
        // --- The following method implements sorting ---
        public override void Sort()
        {
            if (SortPhrase == "Aisle") // if we sort by aisle
            {
                List = new ObservableCollection<AisleForAllView>(List.OrderBy(item => item.AisleName));
            }
            else if (SortPhrase == "Site") // if we sort by site
            {
                List = new ObservableCollection<AisleForAllView>(List.OrderBy(item => item.SiteName));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Aisle", "Site", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Aisle") // if we find by aisle
            {
                List = new ObservableCollection<AisleForAllView>(List.Where(item => item.AisleName != null && item.AisleName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Site") // if we find by site
            {
                List = new ObservableCollection<AisleForAllView>(List.Where(item => item.SiteName != null && item.SiteName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes") // if we find by notes
            {
                List = new ObservableCollection<AisleForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }
        #endregion
        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewAisleViewModel();
        }
    }
}