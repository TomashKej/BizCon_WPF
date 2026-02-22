using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class ClientB : DataBaseClass
    {
        #region Constructor
        public ClientB(BizConDbEntities bizConDbEntities) : base(bizConDbEntities)
        {
        }
        #endregion
        #region helpers
        // The following method retrieves a list of clients formatted as key-value pairs for use in combo boxes.
        public IQueryable<KeyAndValue> GetClientsKeyAndValueItems()
        {
            return
                (
                    from clients in bizConDbEntities.Client // for each client in the Client table
                    where clients.IsActive == true // filter to include only active clients
                    select new KeyAndValue // create a new KeyAndValue object
                    {
                        Key = clients.ClientId,
                        Value = clients.ClientName
                    }
                ).ToList().AsQueryable(); // convert the result to a list and then to IQueryable
        }
        #endregion
    }
}
