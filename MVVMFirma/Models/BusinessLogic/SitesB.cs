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
    ///  This class handles business logic related to SItes.
    /// </summary>
    public class SitesB : DataBaseClass
    {
        #region Constructor
        public SitesB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion
        #region helpers
        // The following method retrieves a list of products formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetSitesKeyAndValueItems()
        {
            return
                (
                    from site in bizConDbEntities.Site // for each product in the Product table
                    where site.IsActive == true // filter to include only active products
                    select new KeyAndValue // create a new KeyAndValue object
                    {
                        Key = site.SiteId,
                        Value = site.SiteName
                    }
                ).ToList().AsQueryable(); // convert the result to a list and then to IQueryable
        }

        /// <summary>
        /// Returns only the sites associated with the specified product.
        /// </summary>
        public IQueryable<KeyAndValue> GetSitesKeyAndValueItems(int productId)
        {
            List<KeyAndValue> sites = (
                from product in bizConDbEntities.Product
                where product.IsActive == true
                select new KeyAndValue
                {
                    Key = product.SiteId.Value,
                    Value = product.Site.SiteName
                }).Distinct().ToList();

            return sites.AsQueryable();
        }
        #endregion
    }
}
