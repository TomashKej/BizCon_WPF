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
    public class PositionB : DataBaseClass
    {
        #region Constructor
        public PositionB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion

        #region Helpers
        // The following method retrieves a list of employees formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetPositionsKeyAndValueItems(int? departmentId)
        {
            List<KeyAndValue> positions = new List<KeyAndValue>();

            // If no specific department is selected, return all positions
            if (!departmentId.HasValue || departmentId.Value == 0)
                return positions.AsQueryable();
            /* 
             * Positions are resolved based on the Employee table because not every position
             * is directly assigned to a department, while each employee always is.
            */
            positions =
                (
                    from e in bizConDbEntities.Employee
                    where e.IsActive == true
                        && e.DepartmentId == departmentId
                        && e.Position != null
                    select new KeyAndValue
                    { 
                        Key = e.Position.PositionId,
                        Value = e.Position.PositionName
                    }
                ).Distinct().OrderBy(p=>p.Value).ToList();
            positions.Insert(0, new KeyAndValue
            {
                Key = 0,
                Value = "All Positions"
            });

            return positions.AsQueryable();
        }
        #endregion
    }
}
