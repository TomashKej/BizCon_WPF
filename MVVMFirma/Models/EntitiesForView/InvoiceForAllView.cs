using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying invoice information in a summarized format.
    /// </summary>
    public class InvoiceForAllView
    {
        public string InvoiceNumber { get; set; }
        public string ClientName { get; set; }
        public string InvoiceStatusName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string PaymentMethodName { get; set; }
        public string Notes { get; set; }

    }
}
