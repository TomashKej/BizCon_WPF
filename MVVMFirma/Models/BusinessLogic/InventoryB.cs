using MVVMFirma.Models.EntitiesForView;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MVVMFirma.Models.BusinessLogic
{
    /// <summary>
    /// Business logic for retrieving inventory details.
    /// </summary>
    public class InventoryB : DataBaseClass
    {
        #region Constructor
        public InventoryB(BizConDbEntities bizConDbEntities)
            : base(bizConDbEntities)
        {
        }
        #endregion

        #region Business Functions
        public List<ProductForAllView> GetInventoryList(int productId, int siteId, string batch, string sku, string clientName, string aisle, string location)
        {
            return (
                from p in bizConDbEntities.Product
                where

                    p.IsActive == true &&
                    (productId == 0 || p.ProductId == productId) &&                                                             // that line is for filtering by productId
                    (siteId == 0 || (p.SiteId.HasValue && p.SiteId.Value == siteId)) &&                                         // that line is for filtering by siteId
                    (string.IsNullOrEmpty(batch) || p.Batch.Contains(batch)) &&                                                 // that line is for filtering by batch
                    (string.IsNullOrEmpty(sku) || p.Sku.Contains(sku)) &&                                                       // that line is for filtering by sku
                    (string.IsNullOrEmpty(clientName) || (p.Client != null && p.Client.ClientName.Contains(clientName))) &&     // that line is for filtering by clientName
                    (string.IsNullOrEmpty(aisle) || (p.Aisle != null && p.Aisle.AisleName.Contains(aisle))) &&                  // that line is for filtering by aisle
                    (string.IsNullOrEmpty(location) || (p.Location != null && p.Location.LocationName.Contains(location)))      // that line is for filtering by location

                select new ProductForAllView
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Batch = p.Batch,
                    Sku = p.Sku,
                    CategoryName = p.Category != null ? p.Category.CategoryName : "",
                    ClientName = p.Client != null ? p.Client.ClientName : "",
                    UnitPrice = p.UnitPrice,
                    Quantity = p.Quantity,
                    SiteName = p.Site != null ? p.Site.SiteName : "",
                    AisleName = p.Aisle != null ? p.Aisle.AisleName : "",
                    LocationName = p.Location != null ? p.Location.LocationName : ""

                }
            ).ToList();
        }

        public void DeleteInventory(int productId)
        {
            Product item = bizConDbEntities.Product.FirstOrDefault(p => p.ProductId == productId);
            if (item != null)
            {
                item.IsActive = false; // Soft delete 
                bizConDbEntities.SaveChanges();
            }
            #endregion
        }

        public void UpdateInventory(ProductForAllView item)
        {
            Product product = bizConDbEntities.Product.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null)
            {
                product.ProductName = item.ProductName;
                product.Batch = item.Batch;
                product.Sku = item.Sku;
                product.UnitPrice = item.UnitPrice;
                product.Quantity = item.Quantity;

                // for foreign keys, we need to get the related entity first
                product.Category = item.CategoryName != null ? bizConDbEntities.Category.FirstOrDefault(c => c.CategoryName == item.CategoryName) : null;
                product.Client = item.ClientName != null ? bizConDbEntities.Client.FirstOrDefault(c => c.ClientName == item.ClientName) : null;
                product.Site = item.SiteName != null ? bizConDbEntities.Site.FirstOrDefault(s => s.SiteName == item.SiteName) : null;
                product.Aisle = item.AisleName != null ? bizConDbEntities.Aisle.FirstOrDefault(a => a.AisleName == item.AisleName) : null;
                product.Location = item.LocationName != null ? bizConDbEntities.Location.FirstOrDefault(l => l.LocationName == item.LocationName) : null;

                bizConDbEntities.SaveChanges();
            }

        }
    }
}
