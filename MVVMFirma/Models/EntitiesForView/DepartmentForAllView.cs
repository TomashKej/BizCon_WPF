using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying department information in a summarized format.
    /// </summary>
    public class DepartmentForAllView
    {
        public string DepartmentName { get; set; }
        public string Notes { get; set; }
    }
}
