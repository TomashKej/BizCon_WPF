using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// this class represents a view model for displaying site information in a summarized format.
    /// </summary>
    public class SiteForAllView
    {
        public string SiteName { get; set; }
        public string AddressLn1 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Notes { get; set; }
    }
}
