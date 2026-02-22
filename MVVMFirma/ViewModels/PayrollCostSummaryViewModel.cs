using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace MVVMFirma.ViewModels
{
    /// <summary>
    /// This class is the ViewModel for the Payroll Cost Summary Report.
    /// It contains properties and commands related to generating and displaying the payroll cost summary.
    /// </summary>
    public class PayrollCostSummaryViewModel : WorkspaceViewModel
    {
        #region DataBase
        private readonly BizConDbEntities bizConDbEntities;

        #endregion

        #region Constructor
        public PayrollCostSummaryViewModel()
        {
            base.DisplayName = "Payroll Summary Report";
            bizConDbEntities = new BizConDbEntities();
            FromDate = DateTime.Now;                        // default FromDate to today
            ToDate = DateTime.Now;                          // default ToDate to today
            PayrollCost = 0;

            DepartmentId = 0; // Default to "All Departments"
            PositionId = 0;   // Default to "All Positions"
            EmployeeId = 0;   // Default to "All Employees"

            // Initial notification of dependent properties
            OnPropertyChanged(() => IsPositionEnabled);
            //OnPropertyChanged(() => IsEmployeeEnabled);
            OnPropertyChanged(() => PositionsComboBoxItems);
            OnPropertyChanged(() => EmployeesComboBoxItems);
        }
        #endregion // End of constructor region

        #region Fields and Properities
        public bool IsPositionEnabled 
        {
            get
            {
                // We enable the position combobox only if a specific department is selected
                //return DepartmentId.HasValue && DepartmentId != 0;
                return DepartmentId > 0;
            }
        }

        private DateTime _FromDate;
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    OnPropertyChanged(() => FromDate);
                }
            }
        }

        private DateTime _ToDate;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    OnPropertyChanged(() => ToDate);
                }
            }
        }


        private int? _DepartmentId;
        public int? DepartmentId
        {
            get => _DepartmentId;
            set
            {
                if (_DepartmentId != value)
                {
                    _DepartmentId = value;
                    // We have to reset choices in dependent combo boxes
                    PositionId = 0;
                    EmployeeId = 0;

                    OnPropertyChanged(() => DepartmentId);
                    OnPropertyChanged(() => IsPositionEnabled); // notify that Position combo box enabled state may have change                                                 

                    OnPropertyChanged(() => PositionsComboBoxItems); // 
                    OnPropertyChanged(() => EmployeesComboBoxItems);
                }
            }
        }

        private int? _PositionId;
        public int? PositionId
        {
            get => _PositionId;
            set
            {
                if (_PositionId != value)
                { 
                    _PositionId = value;
                    // We have to reset choices in dependent combo boxes
                    EmployeeId = 0;

                    OnPropertyChanged(() => PositionId);
                    //OnPropertyChanged(() => IsEmployeeEnabled); // notify that Employee combo box enabled state may have changed
                    OnPropertyChanged(() => EmployeesComboBoxItems); // notify that Employee combo box items may have changed
                }
            }
        }

        private int? _EmployeeId;
        public int? EmployeeId
        {
            get
            {
                return _EmployeeId;
            }
            set
            {
                if (_EmployeeId != value)
                {
                    _EmployeeId = value;
                    OnPropertyChanged(() => EmployeeId);
                    //OnPropertyChanged(() => IsPositionEnabled);
                }
            }
        }

        // --- Combo Boxes ---
        public IQueryable<KeyAndValue> DepartmentsComboBoxItems
        {
            get
            {
                return new DepartmentB(bizConDbEntities).GetDepartmentsKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> PositionsComboBoxItems
        {
            get
            {
                return new PositionB(bizConDbEntities).GetPositionsKeyAndValueItems(DepartmentId);
            }
        }
        public IQueryable<KeyAndValue> EmployeesComboBoxItems
        {
            get 
            {
                return new EmployeeB(bizConDbEntities).GetEmployeesKeyAndValueItems(DepartmentId, PositionId);

            }
        }
        // -- Payroll Cost Summary Result --
        private decimal? _PayrollCost;
        public decimal? PayrollCost
        {
            get
            {
                return _PayrollCost;
            }
            set
            {
                if (_PayrollCost != value)
                {
                    _PayrollCost = value;
                    OnPropertyChanged(() => PayrollCost);
                }
            }
        }
        #endregion // End of field and properties region

        #region Commands
        private BaseCommand _CalculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                if (_CalculateCommand == null)
                    _CalculateCommand = new BaseCommand(calculatePayrollClick);
                return _CalculateCommand;
            }
        }

        private void calculatePayrollClick()
        {
            if (FromDate == null || ToDate == null)
                return;

            PayrollCost = new PayrollB(bizConDbEntities).ProductRevenueForPeriod(DepartmentId, PositionId, EmployeeId, FromDate, ToDate);

            if (PayrollCost == null)
            {
                MessageBox.Show(
                    "No payroll data found for selected criteria.",
                    "No data",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                PayrollCost = 0;
                return;
            }

            if (PayrollCost == 0)
            {
                MessageBox.Show(
                    "The payroll cost for the selected criteria is zero.",
                    "information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

        }
        #endregion // End of commands region
    }
}
