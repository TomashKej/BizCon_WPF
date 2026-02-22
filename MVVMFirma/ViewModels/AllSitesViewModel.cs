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
    public class AllSitesViewModel : WszystkieViewModel<SiteForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<SiteForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from site in bizConDbEntities.Site
                    select new SiteForAllView
                    {
                        SiteName = site.SiteName,
                        AddressLn1 = site.Address.AddressLine1,
                        City = site.Address.City,
                        PostCode = site.Address.PostCode,
                        Notes = site.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllSitesViewModel()
            : base()
        {
            base.DisplayName = "Sites";
        }
        #endregion
        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<String> { "Site Name", "City", "Post Code" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Site Name")
            {
                List = new ObservableCollection<SiteForAllView>(List.OrderBy(item => item.SiteName));
            }
            else if (SortPhrase == "City")
            {
                List = new ObservableCollection<SiteForAllView>(List.OrderBy(item => item.City));
            }
            else if (SortPhrase == "Post Code")
            {
                List = new ObservableCollection<SiteForAllView>(List.OrderBy(item => item.PostCode));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Site Name", "City", "Post Code", "Notes"};
        }

        public override void Find()
        {
            if (FindPhrase == "Site Name")
            {
                List = new ObservableCollection<SiteForAllView>(List.Where(item => item.SiteName != null && item.SiteName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "City")
            {
                List = new ObservableCollection<SiteForAllView>(List.Where(item => item.City != null && item.City.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Post Code")
            {
                List = new ObservableCollection<SiteForAllView>(List.Where(item => item.PostCode != null && item.PostCode.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes")
            {
                List = new ObservableCollection<SiteForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }
        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewSiteViewModel();
        }
        #endregion

    }
}