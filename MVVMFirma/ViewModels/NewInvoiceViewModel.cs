using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;

namespace MVVMFirma.ViewModels
{
    public class NewInvoiceViewModel : JedenViewModel<Invoice>
    {
        #region Collections
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<InvoiceStatus> InvoiceStatuses { get; set; }
        public ObservableCollection<PaymentMethod> PaymentMethods { get; set; }
        #endregion

        #region Constructor
        public NewInvoiceViewModel()
            : base()
        {
            base.DisplayName = "Invoice";
            item = new Invoice();

            Clients = new ObservableCollection<Client>(
                bizConDbEntities.Client.Where(c => c.IsActive == true).ToList()
                );

            InvoiceStatuses = new ObservableCollection<InvoiceStatus>(
                bizConDbEntities.InvoiceStatus.Where(s => s.IsActive == true).ToList()
                );

            PaymentMethods = new ObservableCollection<PaymentMethod>(
                bizConDbEntities.PaymentMethod.Where(p => p.IsActive == true).ToList()
                );
        }
        #endregion

        #region Properties
        public string InvoiceNumber
        {
            get => item.InvoiceNumber;
            set { item.InvoiceNumber = value; OnPropertyChanged(() => InvoiceNumber); }
        }
        private int _selectedClientId;
        public int SelectedClientId
        {
            get => _selectedClientId;
            set 
            {
                _selectedClientId = value; 
                item.ClientId = value;
                OnPropertyChanged(() => SelectedClientId); 
            }
        }
        private int _selectedInvoiceStatusId;
        public int SelectedInvoiceStatusId
        {
            get => _selectedInvoiceStatusId;
            set 
            { 
                _selectedInvoiceStatusId = value; 
                item.StatusId = value;
                OnPropertyChanged(() => SelectedInvoiceStatusId); 
            }
        }
        public DateTime InvoiceDate
        {
            get => item.InvoiceDate;
            set { item.InvoiceDate = value; OnPropertyChanged(() => InvoiceDate); }
        }
        public DateTime DueDate
        {
            get => item.DueDate;
            set { item.DueDate = value; OnPropertyChanged(() => DueDate); }
        }
        public DateTime? PaymentDate
        {
            get => item.PaymentDate;
            set { item.PaymentDate = value; OnPropertyChanged(() => PaymentDate); }
        }
        public decimal TotalAmount
        {
            get => item.TotalAmount;
            set { item.TotalAmount = value; OnPropertyChanged(() => TotalAmount); }
        }
        public decimal? NetAmount
        {
            get => item.NetAmount ?? 0;
            set { item.NetAmount = value; OnPropertyChanged(() => NetAmount); }
        }
        public decimal? TaxAmount
        {
            get => item.TaxAmount ?? 0;
            set { item.TaxAmount = value; OnPropertyChanged(() => TaxAmount); }
        }
        private int? _selectedPaymentMethodId;
        public int? SelectedPaymentMethodId
        {
            get => _selectedPaymentMethodId;
            set 
            { 
                _selectedPaymentMethodId = value; 
                item.PaymentMethodId = value;
                OnPropertyChanged(() => SelectedPaymentMethodId); 
            }
        }
        public string Notes
        {
            get => item.Notes;
            set { item.Notes = value; OnPropertyChanged(() => Notes); }
        }
        #endregion

        #region Commands
        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(InvoiceNumber))
            {
                if (string.IsNullOrWhiteSpace(InvoiceNumber))
                    return "Invoice field canot be empty.";
            }
            else if (propertyName == nameof(SelectedClientId))
            {
                if (SelectedClientId <= 0)
                    return "Client field cannot be empty";
            }
            else if (propertyName == nameof(SelectedInvoiceStatusId))
            {
                if (SelectedInvoiceStatusId <= 0)
                    return "Client field cannot be empty.";
            }
            else if (propertyName == nameof(TotalAmount))
            {
                if (TotalAmount <= 0)
                    return "Total amount cannot be empty, and it must be greater than 0.";
            }
            else if (propertyName == nameof(NetAmount))
            {
                if (!NetAmount.HasValue)
                    return "Net amount cannot be empty.";
            }
            else if (propertyName == nameof(TaxAmount))
            {
                if (!TaxAmount.HasValue)
                    return "Tax amount cannot be empty.";
            }
            return String.Empty;
        }

        public override void Save()
        {
            item.IsActive = true;
            item.CreatedBy = "SYSTEM_TEST";
            item.CreatedAt = DateTime.Now;
            bizConDbEntities.Invoice.Add(item);
            bizConDbEntities.SaveChanges();
        }
        #endregion

    }
}
