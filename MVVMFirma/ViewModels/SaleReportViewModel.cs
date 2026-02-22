using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    /// <summary>
    /// This class is the ViewModel for the Sales Report.
    /// It contains properties for filtering the report data, such as date range and product ID.
    /// </summary>
    public class SaleReportViewModel : WorkspaceViewModel
    {
        #region DataBase
        private readonly BizConDbEntities bizConDbEntities;
        #endregion

        #region Constructor
        public SaleReportViewModel()
        {
            base.DisplayName = "Sales Report";
            bizConDbEntities = new BizConDbEntities();
            FromDate = DateTime.Now;                        // default FromDate to today
            ToDate = DateTime.Now;                          // default ToDate to today
            Revenue = 0;                                    // default Revenue to 0
        }
        #endregion // End of constructor region
        #region Fields and properties
        // For every field which is important for the report, create a property here
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
        private int _ProductId;
        public int ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                if (_ProductId != value)
                {
                    _ProductId = value;
                    OnPropertyChanged(() => ProductId);
                }
            }
        }
        // The following property provides the items for the products combo box
        public IQueryable<KeyAndValue> ProductsComboBoxItems
        {
            get
            {
                // Return the list of products for the combo box
                return new ProductsB(bizConDbEntities).GetProductsKeyAndValueItems();
            }
        }
        // The following property holds the calculated revenue for the selected product and date range
        private decimal? _Revenue;
        public decimal? Revenue
        {
            get
            {
                return _Revenue;
            }
            set
            {
                if (_Revenue != value)
                {
                    _Revenue = value;
                    OnPropertyChanged(() => Revenue);
                }
            }
        }
        #endregion // End of fields and properties region

        #region Commands 
        // We add a commands for every button in the view
        // The following command calls the calculateRevenueClick method when executed
        private BaseCommand _CalculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                if (_CalculateCommand == null) 
                    _CalculateCommand = new BaseCommand(calculateRevenueClick); 
                    return _CalculateCommand;
            }
        }
        private void calculateRevenueClick()
        {
            // This is calls the business logic to calculate the revenue for the selected product and date range from the business class RevenueB
            Revenue = new RevenueB(bizConDbEntities).ProductRevenueForPeriod(ProductId, FromDate, ToDate);
        }
        #endregion
    }
}
