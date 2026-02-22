using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    /// <summary>
    /// Business logic for calculating payroll costs.
    /// The constructor accepts a BizConDbEntities object to interact with the database.
    /// </summary>
    public class PayrollB : DataBaseClass
    {
        #region Constructor
        // Constructor that initializes the PayrollB class with a BizConDbEntities instance
        // : base(bizConDbEntities) calls the constructor of the base class DataBaseClass
        // it allows PayrollB to inherit database access functionality
        public PayrollB(BizConDbEntities bizConDbEntities)
            : base(bizConDbEntities)
        {
        }
        #endregion // End of constructor region

        #region Business Functions
        // The following function calculates the total revenue for a given product for a specified timeframe
        public decimal? ProductRevenueForPeriod(int? departmentId, int? positionId, int? employeeId, DateTime fromDate, DateTime toDate)
        {
            return
                (
                    from ep in bizConDbEntities.EmployeePayment
                    where
                        ep.IsActive == true &&
                        ep.PaymentDate >= fromDate &&
                        ep.PaymentDate <= toDate &&
                        (departmentId == null || departmentId == 0 || ep.Employee.DepartmentId == departmentId) &&
                        (positionId == null || positionId == 0 || ep.Employee.PositionId == positionId) &&
                        (employeeId == null || employeeId == 0 || ep.Employee.EmployeeId == employeeId)
                    select ep.TotalPay
                ).Sum();
        }
        #endregion // End of business functions region
    }
}
