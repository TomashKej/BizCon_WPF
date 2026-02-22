using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Core.Session
{
    /// <summary>
    /// This class represents a user session in the application.
    /// It holds information about the logged-in user and the session start time.
    /// </summary>
    public class UserSession
    {
        #region Properties
        public UserAccount LoggedUser { get; }
        public DateTime LoginTime { get; }

        #endregion // End of properties region

        #region Constructor
        public UserSession(UserAccount loggedUser)
        {
            LoggedUser = loggedUser;
            LoginTime = DateTime.Now;
        }

        #endregion // End of constructor region
    }
}
