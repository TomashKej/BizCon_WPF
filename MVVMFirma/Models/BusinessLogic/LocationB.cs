using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class LocationB : DataBaseClass
    {
        #region Constructor
        public LocationB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion
        #region helpers
        // The following method retrieves a list of products formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetLocationsKeyAndValueItems()
        {
            return
                (
                    from location in bizConDbEntities.Location // for each product in the Product table
                    where location.IsActive == true // filter to include only active products
                    select new KeyAndValue // create a new KeyAndValue object
                    {
                        Key = location.LocationId,
                        Value = location.LocationName
                    }
                ).ToList().AsQueryable(); // convert the result to a list and then to IQueryable
        }

        /// <summary>
        /// Returns only the aisles where a given product is available in inventory.
        /// </summary>
        public IQueryable<KeyAndValue> GetLocationsKeyAndValueItems(int productId)
        {
            List<KeyAndValue> locations = (
                from product in bizConDbEntities.Product
                where product.IsActive == true
                select new KeyAndValue
                {
                    Key = product.LocationId.Value,
                    Value = product.Location.LocationName
                }).Distinct().ToList();

            return locations.AsQueryable();
        }
        #endregion
    }
}