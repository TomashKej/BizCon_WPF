using MVVMFirma.Models.Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Core.Services
{
    /// <summary>
    /// Provides acces to curent user session information.
    /// </summary>
    public interface ISessionService
    {
        // Gets the current user session.
        UserSession CurrentSession { get; }
        // Starts a new user session.
        void StartSession(UserAccount user);
        // Ends the current user session.
        void EndSession();
    }
}
