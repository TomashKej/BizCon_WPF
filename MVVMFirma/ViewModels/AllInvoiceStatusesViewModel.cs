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
    public class AllInvoiceStatusesViewModel : WszystkieViewModel<InvoiceStatusForAllViews>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<InvoiceStatusForAllViews>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from invoiceStatus in bizConDbEntities.InvoiceStatus
                    select new InvoiceStatusForAllViews
                    {
                        InvoiceStatusName = invoiceStatus.InvoiceStatusName,
                        Notes = invoiceStatus.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllInvoiceStatusesViewModel()
            : base()
        {
            base.DisplayName = "Invoice Statuses";
        }
        #endregion

        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Invoice Status Name" };
        }
        // -- The following methods implement sorting --
        public override void Sort()
        {
            if (SortPhrase == "Invoice Status")
            { 
                List = new ObservableCollection<InvoiceStatusForAllViews>(List.OrderBy(item => item.InvoiceStatusName));
            }
        }
        // The following method decides by what we can find
        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Invoice Status Name", "Notes" };
        }
        // -- The following method implements finding --
        public override void Find()
        {
            if (FindPhrase == "Invoice Status Name")
            {
                List = new ObservableCollection<InvoiceStatusForAllViews>(List.Where(item => item.InvoiceStatusName != null && item.InvoiceStatusName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes")
            {
                List = new ObservableCollection<InvoiceStatusForAllViews>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }
        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewInvoiceStatusViewModel();
        }
        #endregion
    }
}