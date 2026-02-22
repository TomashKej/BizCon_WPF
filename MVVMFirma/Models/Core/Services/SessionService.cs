using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Core.Session;

namespace MVVMFirma.Models.Core.Services
{
    /// <summary>
    /// This class provides access to the current user session information.
    /// It implements the ISessionService interface.
    /// </summary>
    public class SessionService : ISessionService
    {
        // Starts a new user session.
        public UserSession CurrentSession { get; private set; }
        // Starts a new user session.
        public void StartSession(UserAccount user)
        {
            CurrentSession = new UserSession(user);
        }
        // Ends the current user session.
        public void EndSession()
        {
            CurrentSession = null;
        }
    }
}
