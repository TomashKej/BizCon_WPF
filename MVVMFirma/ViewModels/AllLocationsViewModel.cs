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
using MVVMFirma.Views;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllLocationsViewModel : WszystkieViewModel<LocationForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<LocationForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from location in bizConDbEntities.Location
                    select new LocationForAllView
                    {
                        LocationName = location.LocationName,
                        SiteName = location.Site.SiteName,
                        AisleName = location.Aisle.AisleName,
                        Notes = location.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllLocationsViewModel()
            : base()
        {
            base.DisplayName = "Locations";
        }
        #endregion
        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Location Name", "Site", "Aisle" };
        }
        // --- The following method implements sorting ---
        public override void Sort()
        {
            if (SortPhrase == "Location Name") // if we sort by location name
            {
                List = new ObservableCollection<LocationForAllView>(List.OrderBy(item => item.LocationName));
            }
            else if (SortPhrase == "Site") // if we sort by site
            {
                List = new ObservableCollection<LocationForAllView>(List.OrderBy(item => item.SiteName));
            }
            else if (SortPhrase == "Aisle") // if we sort by aisle
            {
                List = new ObservableCollection<LocationForAllView>(List.OrderBy(item => item.AisleName));
            }
        }
        // The following method decides by what we can find
        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Location Name", "Site", "Aisle", "Notes" };
        }
        // --- The following method implements finding ---
        public override void Find()
        {
            if (FindPhrase == "Location Name") // if we find by location name
            {
                List = new ObservableCollection<LocationForAllView>(List.Where(item => item.LocationName != null && item.LocationName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Site") // if we find by site
            {
                List = new ObservableCollection<LocationForAllView>(List.Where(item => item.SiteName != null && item.SiteName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Aisle") // if we find by aisle
            {
                List = new ObservableCollection<LocationForAllView>(List.Where(item => item.AisleName != null && item.AisleName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes") // if we find by notes
            {
                List = new ObservableCollection<LocationForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewLocationViewModel();
        }
        #endregion

    }
}