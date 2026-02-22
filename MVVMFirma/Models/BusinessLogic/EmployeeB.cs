using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    /// <summary>
    /// This class handles business logic related to Employees.
    /// </summary>
    public class EmployeeB : DataBaseClass
    {
        #region Constructor
        public EmployeeB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion

        #region Helpers
        // The following method retrieves a list of employees formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetEmployeesKeyAndValueItems(int? departmentId, int? positionId)
        {
            List<KeyAndValue> employees =
                (from e in bizConDbEntities.Employee
                where e.IsActive == true
                    && (departmentId == 0 || e.DepartmentId == departmentId)
                    && (positionId == 0 || e.PositionId == positionId)
                select new KeyAndValue
                {
                    Key = e.EmployeeId,
                    Value = e.FirstName + " " + e.LastName
                }).ToList();

            employees.Insert(0, new KeyAndValue
            {
                Key = 0,
                Value = "All Employees"
            });

            return employees.AsQueryable();
        }
        #endregion
    }
}
