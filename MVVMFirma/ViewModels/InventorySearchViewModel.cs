using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.EntitiesForView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class InventorySearchViewModel : WorkspaceViewModel
    {
        #region DataBase
        private readonly BizConDbEntities bizConDbEntities;
        #endregion // End of DataBase region

        #region Constructor
        public InventorySearchViewModel()
        {
            base.DisplayName = "Inventory Search";
            bizConDbEntities = new BizConDbEntities();
            InventoryList = new ObservableCollection<ProductForAllView>();
        }
        #endregion // End of constructor region

        #region Fields and properties
        // The InventoryList property holds the collection of inventory details to be displayed in the UI.
        private ObservableCollection<ProductForAllView> _InventoryList;
        public ObservableCollection<ProductForAllView> InventoryList
        {
            get => _InventoryList;
            set
            {
                _InventoryList = value;
                OnPropertyChanged(() => InventoryList);
            }
        }

        /// <summary>
        /// The following properties are used to populate combo boxes for filtering.
        /// </summary>
        // --- Combo Boxes ---
        public IQueryable<KeyAndValue> ProductsComboBoxItems
        {
            get
            {
                return new ProductsB(bizConDbEntities).GetProductsKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> SitesComboBoxItems
        {
            get
            {
                return new SitesB(bizConDbEntities).GetSitesKeyAndValueItems(ProductId);
            }
        }
        public IQueryable<KeyAndValue> AislesComboBoxItems
        {
            get
            {
                return new AislesB(bizConDbEntities).GetAislesKeyAndValueItems();
            }
        }

        public IQueryable<KeyAndValue> LocationsComboBoxItems
        {
            get
            {
                return new LocationB(bizConDbEntities).GetLocationsKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> CategoriesComboBoxItems
        {
            get
            {
                return new CategoryB(bizConDbEntities).GetCategoriesKeyAndValueItems();
            }
        }

        public IQueryable<KeyAndValue> ClientsComboBoxItems
        {
            get
            {
                return new ClientB(bizConDbEntities).GetClientsKeyAndValueItems();
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
                    OnPropertyChanged(() => SitesComboBoxItems);
                }
            }
        }

        private int _SiteId;
        public int SiteId
        {
            get
            {
                return _SiteId;
            }
            set
            {
                if (_SiteId != value)
                {
                    _SiteId = value;
                    OnPropertyChanged(() => SiteId);
                }
            }
        }

        /// <summary>
        /// The following properties are used as search criteria. 
        /// </summary>
        private string _BatchNumber;
        public string BatchNumber
        {
            get => _BatchNumber;
            set { _BatchNumber = value; OnPropertyChanged(() => BatchNumber); }
        }

        private string _ClientId;
        public string ClientId
        {
            get => _ClientId;
            set { _ClientId = value; OnPropertyChanged(() => ClientId); }
        }

        private string _SkuNumber;
        public string SkuNumber
        {
            get => _SkuNumber;
            set { _SkuNumber = value; OnPropertyChanged(() => SkuNumber); }
        }

        private string _AisleNumber;
        public string AisleNumber
        {
            get => _AisleNumber;
            set { _AisleNumber = value; OnPropertyChanged(() => AisleNumber); }
        }

        private string _LocationNumber;
        public string LocationNumber
        {
            get => _LocationNumber;
            set { _LocationNumber = value; OnPropertyChanged(() => LocationNumber); }
        }

        /// <summary>
        /// The SelectedInventory property holds the currently selected inventory item from the list.
        /// </summary>
        private ProductForAllView _SelectedInventory;
        public ProductForAllView SelectedInventory
        {
            get => _SelectedInventory;
            set
            {
                _SelectedInventory = value;
                OnPropertyChanged(() => SelectedInventory);
            }
        }
        #endregion // End of fields and properties region

        #region Commands
        private BaseCommand _SearchInventoryCommand;
        public ICommand SearchInventoryCommand
        {
            get
            {
                if (_SearchInventoryCommand == null)
                    _SearchInventoryCommand = new BaseCommand(SearchInventoryClick);
                return _SearchInventoryCommand;
            }
        }

        // ----- The ClearAllCriteria method resets all search criteria fields to their default values.  ----- 
        private BaseCommand _ClearFiltersCommand;
        public ICommand ClearFiltersCommand
        {
            get
            {
                if (_ClearFiltersCommand == null)
                    _ClearFiltersCommand = new BaseCommand(ClearAllCriteria);
                return _ClearFiltersCommand;
            }
        }

        private BaseCommandWithParameter _EditInventoryCommand;
        public ICommand EditInventoryCommand
        {
            get
            {
                if (_EditInventoryCommand == null)
                    _EditInventoryCommand = new BaseCommandWithParameter((param) => EditInventory((ProductForAllView)param));
                return _EditInventoryCommand;
            }
        }

        private BaseCommandWithParameter _DeleteInventoryCommand;
        public ICommand DeleteInventoryCommand
        {
            get
            {
                if (_DeleteInventoryCommand == null)
                    _DeleteInventoryCommand = new BaseCommandWithParameter((param) => DeleteInventory((ProductForAllView)param));
                return _DeleteInventoryCommand;
            }
        }

        private BaseCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new BaseCommand(SaveInventory);
                return _SaveCommand;
            }
        }
        #endregion // End of commands region

        #region Methods/Helpers

        // ----- The SearchInventoryClick method is triggered when the search command is executed. It loads inventory data based on the search criteria. -----
        private void SearchInventoryClick()
        {
            LoadInventoryData();
        }

        // ----- The LoadInventoryData method retrieves inventory details based on the selected ProductId and SiteId. ----- 
        private void LoadInventoryData()
        {
            List<ProductForAllView> result = new InventoryB(bizConDbEntities).GetInventoryList
                (ProductId, SiteId, BatchNumber, SkuNumber, ClientId, AisleNumber, LocationNumber);

            InventoryList.Clear(); // Clear existing items before adding new ones
            foreach (ProductForAllView item in result)
            {
                InventoryList.Add(item);
            }

            if (InventoryList.Count == 0)
            {
                MessageBox.Show("No inventory records found matching the search criteria.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // ----- The ClearAllCriteria method resets all search criteria fields to their default values. ----- 
        private void ClearAllCriteria()
        {
            ProductId = 0;
            SiteId = 0;
            BatchNumber = string.Empty;
            ClientId = string.Empty;
            SkuNumber = string.Empty;
            AisleNumber = string.Empty;
            LocationNumber = string.Empty;
            InventoryList.Clear(); // Clear the inventory list when criteria are cleared
            OnPropertyChanged(() => SitesComboBoxItems);
        }

        private void SaveInventory()
        {
            if (SelectedInventory == null) return;
            // Validate the SelectedInventory fields as needed before saving
            // For example, check for required fields, valid data formats, etc.
            new InventoryB(bizConDbEntities).UpdateInventory(SelectedInventory);
            MessageBox.Show("Inventory item saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadInventoryData();
            SelectedInventory = null; // Clear the selection after saving
        }

        // ----- The EditInventory method is used to edit the selected inventory item. ----- 
        private void EditInventory(ProductForAllView item)
        {
            if (item == null) return;

            // We create a copy of the selected item to avoid direct modifications to the list item.
            SelectedInventory = new ProductForAllView
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Batch = item.Batch,
                Sku = item.Sku,
                CategoryName = item.CategoryName,
                ClientName = item.ClientName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                SiteName = item.SiteName,
                AisleName = item.AisleName,
                LocationName = item.LocationName
            };
        }

        // ----- The DeleteInventory method is used to delete the selected inventory item. ----- 
        private void DeleteInventory(ProductForAllView item)
        {
            if (item == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete the product? {item.ProductName}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                new InventoryB(bizConDbEntities).DeleteInventory(item.ProductId);
                LoadInventoryData();
            }
        }
        #endregion
    }
}
