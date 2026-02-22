using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    /// <summary>
    ///  This class handles business logic related to Products.
    /// </summary>
    public class ProductsB : DataBaseClass
    {
        #region Constructor
        public ProductsB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion
        #region helpers
        // The following method retrieves a list of products formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetProductsKeyAndValueItems()
        {
            return
                (
                    from product in bizConDbEntities.Product // for each product in the Product table
                    where product.IsActive == true // filter to include only active products
                    select new KeyAndValue // create a new KeyAndValue object
                    {
                        Key = product.ProductId,
                        Value= product.ProductName + " " + product.Batch // concatenate product name and batch for display
                    }
                ).ToList().AsQueryable(); // convert the result to a list and then to IQueryable
        }
        #endregion
    }
}
