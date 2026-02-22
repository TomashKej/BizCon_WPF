using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying invoice status information in a summarized format.
    /// </summary>
    public class InvoiceStatusForAllViews
    {
        public string InvoiceStatusName { get; set; }
        public string Notes { get; set; }
    }
}
