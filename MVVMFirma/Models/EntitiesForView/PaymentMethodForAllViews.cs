using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying payment method information in a summarized format.
    /// </summary>
    public class PaymentMethodForAllViews
    {
        public string PaymentMethodName { get; set; }
        public string Notes { get; set; }
    }
}
