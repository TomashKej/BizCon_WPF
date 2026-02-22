using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class InventoryDetailsForView
    {
        public int InventoryId { get; set; }
        public string BatchNumber { get; set; }   // Product.Batch
        public string ClientName { get; set; }
        public string SkuNumber { get; set; }     // Product.Sku
        public string AisleNumber { get; set; }
        public string LocationNumber { get; set; }
        public decimal? Quantity { get; set; }    // Inventory.Quantity
    }
}
