using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class RemindPasswordViewModel : BaseViewModel
    {
        private readonly BizConDbEntities db = new BizConDbEntities();

        #region Properties

        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        #endregion
       
        #region Command

        private BaseCommand _RecoverPasswordCommand;
        public ICommand RecoverPasswordCommand
        {
            get
            {
                if (_RecoverPasswordCommand == null)
                    _RecoverPasswordCommand = new BaseCommand(RecoverPassword);

                return _RecoverPasswordCommand;
            }
        }

        #endregion

        private void RecoverPassword()
        {
            if (string.IsNullOrWhiteSpace(EmployeeNumber) || string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName) || !DateOfBirth.HasValue)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            RemindPasswordB remindPasswordB =
                new RemindPasswordB(new BizConDbEntities());

            string password = remindPasswordB.GetPassword(
                EmployeeNumber,
                FirstName,
                LastName,
                DateOfBirth.Value);

            if (password == null)
            {
                MessageBox.Show("Employee or user account not found.");
                return;
            }

            MessageBox.Show(
                "Your password is: " + password,
                "Password recovery",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
