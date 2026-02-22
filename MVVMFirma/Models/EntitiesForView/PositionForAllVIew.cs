using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying position information in a summarized format.
    /// </summary>
    public class PositionForAllVIew
    {
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
        public string GradeName { get; set; }
        public decimal? Salary { get; set; }
        public string Notes { get; set; }
    }
}
