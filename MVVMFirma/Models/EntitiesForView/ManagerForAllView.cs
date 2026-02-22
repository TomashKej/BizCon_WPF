using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying manager information in a summarized format.
    /// </summary>
    public class ManagerForAllView
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public string Notes { get; set; }
    }
}
