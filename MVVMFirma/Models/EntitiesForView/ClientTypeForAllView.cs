using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying client type information in a summarized format.
    /// </summary>
    public class ClientTypeForAllView
    {
        public string ClientTypeName { get; set; }
        public string Notes { get; set; }
    }
}
