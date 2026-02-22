using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying grade information in a summarized format.
    /// </summary>
    public class GradeForAllViews
    {
        public string GradeName { get; set; }
        public decimal? HourlyWage { get; set; }
        public decimal? AnnualWage { get; set; }
        public string Notes { get; set; }
    }
}
