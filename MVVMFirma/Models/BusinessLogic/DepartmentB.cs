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
    /// This class handles business logic related to Department.
    /// </summary>
    public class DepartmentB : DataBaseClass
    {
        #region Constructor
        public DepartmentB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion

        #region Helpers
        // The following method retrieves a list of employees formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetDepartmentsKeyAndValueItems()
        {
            List<KeyAndValue> departments =
                (from Department department in bizConDbEntities.Department
                 where department.IsActive == true
                 select new KeyAndValue
                 {
                     Key = department.DepartmentId,
                     Value = department.DepartmentName
                 }
                 ).ToList();

            departments.Insert(0, new KeyAndValue 
            { 
                Key = 0, 
                Value = "All Departments" 
            });
            return departments.AsQueryable();
        }
        #endregion
    }
}
