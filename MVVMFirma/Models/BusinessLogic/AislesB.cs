using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class AislesB : DataBaseClass
    {
        #region Constructor
        public AislesB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion
        #region helpers
        // The following method retrieves a list of products formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetAislesKeyAndValueItems()
        {
            return
                (
                    from aisle in bizConDbEntities.Aisle // for each product in the Product table
                    where aisle.IsActive == true // filter to include only active products
                    select new KeyAndValue // create a new KeyAndValue object
                    {
                        Key = aisle.AisleId,
                        Value = aisle.AisleName
                    }
                ).ToList().AsQueryable(); // convert the result to a list and then to IQueryable
        }

        /// <summary>
        /// Returns only the aisles where a given product is available in inventory.
        /// </summary>
        public IQueryable<KeyAndValue> GetAislesKeyAndValueItems(int productId)
        {
            List<KeyAndValue> aisles = (
                from product in bizConDbEntities.Product
                where product.IsActive == true
                select new KeyAndValue
                {
                    Key = product.AisleId.Value,
                    Value = product.Aisle.AisleName
                }).Distinct().ToList();

            return aisles.AsQueryable();
        }
        #endregion
    }
}
