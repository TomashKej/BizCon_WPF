using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class CategoryB : DataBaseClass
    {
        #region Constructor
        public CategoryB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion
        #region helpers
        // The following method retrieves a list of products formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetCategoriesKeyAndValueItems()
        {
            return
                (
                    from categories in bizConDbEntities.Category // for each product in the Product table
                    where categories.IsActive == true // filter to include only active products
                    select new KeyAndValue // create a new KeyAndValue object
                    {
                        Key = categories.CategoryId,
                        Value = categories.CategoryName
                    }
                ).ToList().AsQueryable(); // convert the result to a list and then to IQueryable
        }
        #endregion
    }
}
