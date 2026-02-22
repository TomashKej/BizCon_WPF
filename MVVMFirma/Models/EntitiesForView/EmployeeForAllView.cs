using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    /// <summary>
    /// This class represents a view model for displaying employee information in a summarized format.
    /// </summary>
    public class EmployeeForAllView
    {
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
        public string GradeName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public string SiteName { get; set; }
        public string Notes { get; set; }
    }
}
