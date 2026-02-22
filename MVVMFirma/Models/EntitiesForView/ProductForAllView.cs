using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class is a placeholder for Product view entity.
    /// </summary>
    public class ProductForAllView
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Batch { get; set; }
        public string Sku { get; set; }
        public string CategoryName { get; set; }
        public string ClientName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string SiteName { get; set; }
        public string AisleName { get; set; }
        public string LocationName { get; set; }
    }
}
