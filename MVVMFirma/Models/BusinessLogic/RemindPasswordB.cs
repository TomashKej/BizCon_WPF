using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class RemindPasswordB : DataBaseClass
    {
        public RemindPasswordB(BizConDbEntities bizConDbEntities) 
            : base(bizConDbEntities)
        {
        }

        public string GetPassword(
            string employeeNumber,
            string firstName,
            string lastName,
            DateTime dateOfBirth)
        {
            Employee employee =
                bizConDbEntities.Employee.FirstOrDefault(e =>
                    e.EmployeeNumber == employeeNumber &&
                    e.FirstName == firstName &&
                    e.LastName == lastName &&
                    e.DateOfBirth == dateOfBirth);

            if (employee == null)
                return null;

            UserAccount userAccount =
                bizConDbEntities.UserAccount
                    .FirstOrDefault(u => u.EmployeeId == employee.EmployeeId);

            if (userAccount == null)
                return null;

            return userAccount.PasswordHash;
        }
    }
}
