using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class DataBaseClass
    {
        #region DataBase
        protected BizConDbEntities bizConDbEntities;

        #endregion
        #region Constructor
        public DataBaseClass(BizConDbEntities bizConDbEntities)
        {
            this.bizConDbEntities = bizConDbEntities;
        }

        #endregion
    }
}
