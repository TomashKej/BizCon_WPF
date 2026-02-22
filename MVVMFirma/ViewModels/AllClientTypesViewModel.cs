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
    public class AllClientTypesViewModel : WszystkieViewModel<ClientTypeForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<ClientTypeForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from clientType in bizConDbEntities.ClientType
                    select new ClientTypeForAllView
                    {
                        ClientTypeName = clientType.ClientType1,
                        Notes = clientType.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllClientTypesViewModel()
            : base()
        {
            base.DisplayName = "Client Types";
        }
        #endregion

        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Client Type" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Client Type") // if we sort by client type
            {
                List = new ObservableCollection<ClientTypeForAllView>(List.OrderBy(item => item.ClientTypeName));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Client Type", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Client Type") // if we find by client type
            {
                List = new ObservableCollection<ClientTypeForAllView>(List.Where(item => item.ClientTypeName != null && item.ClientTypeName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes") // if we find by notes
            {
                List = new ObservableCollection<ClientTypeForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewClientTypeViewModel();
        }
        #endregion
    }
}