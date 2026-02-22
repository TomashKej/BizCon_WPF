using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using MVVMFirma.ViewModels.Abstract;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Helper;

namespace MVVMFirma.ViewModels
{
    public class AllAddressesViewModel : WszystkieViewModel<AddressForAllView>
    {
        #region Constructor
        public AllAddressesViewModel()
            : base()
        {
            base.DisplayName = "Addresses";
        }
        #endregion // End of Constructor Region

        #region List
        public override void Load()
        {
            List = new ObservableCollection<AddressForAllView>
                (
                    from address in bizConDbEntities.Address
                    where address.IsActive == true
                    select new AddressForAllView
                    {
                        AddressLine1 = address.AddressLine1,
                        AddressLine2 = address.AddressLine2,
                        City = address.City,
                        PostCode = address.PostCode,
                        County = address.County,
                        Country = address.Country
                    }
                );

        }
        #endregion // End of List Region

        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Country", "City", "County", "Post Code" };
        }

        // --- The following method implements sorting ---
        public override void Sort()
        {
            if (SortPhrase == "Country") // if we sort by country
            {
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item=>item.Country));
            }
            else if (SortPhrase == "City") // if we sort by city
            {
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.City));
            }
            else if (SortPhrase == "County") // if we sort by county
            {
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.County));
            }
            else if (SortPhrase == "Post Code") // if we sort by post code
            {
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.PostCode));
            }

        }

        // The following method decides by what we can find
        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Country", "City", "County", "Post Code" };
        }

        // The following method implements finding
        public override void Find()
        {
            if (FindPhrase == "Country")    // if we find by country
            {
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.Country != null && item.Country.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "City") // if we find by city
            {
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.City != null && item.City.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "County") // if we find by county
            {
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.County != null && item.County.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Post Code") // if we find by post code
            {
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.PostCode != null && item.PostCode.StartsWith(FindTextBox)));
            }
        }


        #endregion

        #region AddingNew
        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewAddressViewModel();
        }
        #endregion
    }
}