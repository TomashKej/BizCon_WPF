using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.Core.Services;
using MVVMFirma.ViewModels.Abstract;
using MVVMFirma.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class LoginWindowViewModel : WorkspaceViewModel
    {
        #region DataBase
        private readonly BizConDbEntities bizConDbEntities = new BizConDbEntities();
        #endregion

        #region Constructor
        public LoginWindowViewModel()
        {
            base.DisplayName = "Login Window";
        }
        #endregion // End of Constructor region

        #region Fields and Properties
        private readonly ISessionService _sessionService = new SessionService();

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(() => Username);
                }
            }
        }

        #endregion // End of Fields and Properties region

        #region Commands
        private BaseCommandWithParameter _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                    _LoginCommand = new BaseCommandWithParameter((obj) => ExecuteLogin(obj));
                return _LoginCommand;
            }
        }

        private BaseCommand _ForgotPasswordCommand;
        public ICommand ForgotPasswordCommand
        {
            get
            {
                if (_ForgotPasswordCommand == null)
                    _ForgotPasswordCommand = new BaseCommand(OpenRemindPasswordWindow);
                return _ForgotPasswordCommand;
            }
        }

        #endregion // End of Commands region

        #region Methods
        private void ExecuteLogin(object parameter)
        {

            string twardyLogin = "TKaczmarek";
            string twardeHaslo = "Haslo123";

            // 1. Get the password from the PasswordBox
            PasswordBox passwordBox = parameter as PasswordBox;
            string password = passwordBox?.Password;

            // 2. Validate the credentials against the database
            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Please enter a username.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 3. Check credentials
            // We looking for the active user with provided username
            UserAccount user = bizConDbEntities.UserAccount
                .FirstOrDefault(u => u.Username == Username && u.IsActive == true);

            // ===== LOGIN TESTOWY / GUEST (BEZ BAZY DANYCH) =====
            if (Username == twardyLogin && password == twardeHaslo)
            {
                UserAccount guestUser = new UserAccount
                {
                    Username = twardyLogin
                };

                MainWindowViewModel mainViewModel = new MainWindowViewModel(guestUser);
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();

                OnRequestClose();
                return;
            }

            // 4. Verify password (For simplicity, we are using plain text comparison here. In a real application, use hashed passwords)
            if (user != null && user.PasswordHash == password)
            {
                // Successful login
                user.LastLogin = DateTime.Now;
                bizConDbEntities.SaveChanges();

                // The session management
                SessionService sessionService = new SessionService();
                sessionService.StartSession(user);

                MainWindowViewModel mainViewModel = new MainWindowViewModel(user);

                // Open the main window
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();

                // Close the login window
                OnRequestClose();
            }
            else
            {
                // Invalid credentials
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenRemindPasswordWindow()
        {
            RemindPasswordWindow window = new RemindPasswordWindow();
            window.Show();
        }

        #endregion // End of Methods region
    }
}
