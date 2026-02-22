using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    /// <summary>
    /// The RevenueB class handles business logic related to revenue calculations.
    /// It inherits from DataBaseClass to access the database context.
    /// </summary>
    public class RevenueB : DataBaseClass
    {
        #region Constructor
        public RevenueB(BizConDbEntities bizConDbEntities) 
            : base(bizConDbEntities)
        {
        }
        #endregion // End of constructor region

        #region Business Functions
        // The following function calculates the total revenue for a given product for a specified timeframe
        public decimal? ProductRevenueForPeriod(int productId, DateTime fromDate, DateTime toDate)
        {
            return
                (
                    from item in bizConDbEntities.InvoiceItem // for each invoice item in the InvoiceItem table
                    where
                        item.IsActive == true &&
                        item.Invoice.IsActive == true &&
                        item.Invoice.InvoiceDate >= fromDate &&
                        item.Invoice.InvoiceDate <= toDate &&
                        item.ProductId == productId // filter by the specified product ID
                    select (item.UnitPrice * item.Quantity) // calculate revenue for each item
                ).Sum(); // sum the total revenue for the specified product and timeframe
        }
        #endregion // End of business functions region
    }
}
