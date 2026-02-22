using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.ViewModels.Abstract;
using System.Collections.ObjectModel;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllInvoicesViewModel : WszystkieViewModel<InvoiceForAllView>
    {
        #region Constructor
        public AllInvoicesViewModel()
            : base()
        {
            base.DisplayName = "Invoices";
        }
        #endregion
        #region List
        public override void Load()
        {
            List = new ObservableCollection<InvoiceForAllView>
                (
                    from invoice in bizConDbEntities.Invoice
                    where invoice.IsActive == true
                    select new InvoiceForAllView // dla kazdej aktywnej faktury tworzymy nowa faktureForAllView
                    {
                        InvoiceNumber = invoice.InvoiceNumber,
                        ClientName = invoice.Client.ClientName,
                        InvoiceStatusName = invoice.InvoiceStatus.InvoiceStatusName,
                        InvoiceDate = invoice.InvoiceDate,
                        DueDate = invoice.DueDate,
                        PaymentDate = invoice.PaymentDate ,
                        TotalAmount = invoice.TotalAmount,
                        NetAmount = invoice.NetAmount ?? 0,
                        TaxAmount = invoice.TaxAmount ?? 0,
                        PaymentMethodName = invoice.PaymentMethod.PaymentMethodName,
                        Notes = invoice.Notes
                    }
                );
        }
        #endregion
        #region SortingAndFiltering
        // The following method decides by what we can sort
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Invoice Number", "Client Name", "Invoice Status", 
                                      "Invoice Date", "Due Date", "Payment Date", "Total Amount", 
                                      "Net Amount", "Tax Amount", "Payment Method" };
        }
        // -- The following method implements sorting --
        public override void Sort()
        {
            if (SortPhrase == "Invoice Number") // if we sort by invoice number
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.InvoiceNumber));
            }
            else if (SortPhrase == "Client Name") // if we sort by client name
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.ClientName));
            }
            else if (SortPhrase == "Invoice Status") // if we sort by invoice status
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.InvoiceStatusName));
            }
            else if (SortPhrase == "Invoice Date") // if we sort by invoice date
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.InvoiceDate));
            }
            else if (SortPhrase == "Due Date") // if we sort by due date
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.DueDate));
            }
            else if (SortPhrase == "Total Amount") // if we sort by total amount
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.TotalAmount));
            }
            else if (SortPhrase == "Net Amount") // if we sort by net amount
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.NetAmount));
            }
            else if (SortPhrase == "Tax Amount") // if we sort by tax amount
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.TaxAmount));
            }
            else if (SortPhrase == "Payment Method") // if we sort by payment method
            {
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.PaymentMethodName));
            }
        }
        // The following method decides by what we can find
        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Invoice Number", "Client Name", "Invoice Status",
                                      "Invoice Date", "Due Date", "Payment Date", "Total Amount",
                                      "Net Amount", "Tax Amount", "Payment Method", "Notes" };
        }
        // -- The following method implements finding --
        public override void Find()
        {
            if (FindPhrase == "Invoice Number")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.InvoiceNumber != null && item.InvoiceNumber.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Client Name")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.ClientName != null && item.ClientName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Invoice Status")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.InvoiceStatusName != null && item.InvoiceStatusName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Invoice Date")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.InvoiceDate.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Due Date")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.DueDate.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Payment Date")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.PaymentDate != null && item.PaymentDate.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Total Amount")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.TotalAmount.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Net Amount")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.NetAmount.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Tax Amount")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.TaxAmount.ToString().StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Payment Method")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.PaymentMethodName != null && item.PaymentMethodName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes")
            {
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewInvoiceViewModel();
        }
        #endregion
    }
}
