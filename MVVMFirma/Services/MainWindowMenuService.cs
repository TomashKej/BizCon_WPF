using System;
using MVVMFirma.Helper;
using MVVMFirma.ViewModels;
using System.Collections.ObjectModel;
using MVVMFirma.Models;
namespace MVVMFirma.Services
{
    public class MainWindowMenuService
    {
        #region Properties
        // We create a view model property to call its methods
        public MainWindowViewModel _vm;
        #endregion // end of properties region

        #region Constructor
        // The folliwing constructor "inject" ViewModel. It allows us call methods on it
        public MainWindowMenuService(MainWindowViewModel viewModel)
        { 
            _vm = viewModel;
        }
        #endregion // end of constructor region

        #region API
        public ObservableCollection<CommandViewModel> GetCommandsForModule(MainModule module)
        { 
            ObservableCollection<CommandViewModel> commands = new ObservableCollection<CommandViewModel>();

            switch (module)
            {
                case MainModule.AllModules:
                    LoadAllCommands(commands);
                    break;
                case MainModule.HumanResources:
                    LoadHrCommands(commands);
                    break;
                case MainModule.MasterData:
                    LoadMasterDataCommands(commands);
                    break;
                case MainModule.Finance:
                    LoadFinanceCommands(commands);
                    break;
                case MainModule.Administration:
                    LoadAdministrationCommands(commands);
                    break;
            }
            return commands;
        }
        #endregion // end if API region

        #region Commands Helpers
        private void AddCommand(ObservableCollection<CommandViewModel> collection, string displayName, Action action)
        {
            collection.Add(new CommandViewModel(displayName, new BaseCommand(action)));
        }
        #endregion // end of Commands Helpers region

        #region Methods
        private void LoadHrCommands(ObservableCollection<CommandViewModel> collection) 
        {
            AddCommand(collection, "Employees", () => _vm.ShowAllEmployees());
            AddCommand(collection, "Employee", () => _vm.CreateView(new NewEmployeeViewModel()));
            AddCommand(collection, "Departments", () => _vm.ShowAllDepartments());
            AddCommand(collection, "Department", () => _vm.CreateView(new NewDepartmentViewModel()));
            AddCommand(collection, "Positions", () => _vm.ShowAllPositions());
            AddCommand(collection, "Position", () => _vm.CreateView(new NewPositionViewModel()));
            AddCommand(collection, "Managers", () => _vm.ShowAllManagers());
            AddCommand(collection, "Manager", () => _vm.CreateView(new NewManagerViewModel()));
        }

        private void LoadMasterDataCommands(ObservableCollection<CommandViewModel> collection)
        {
            AddCommand(collection, "Products", () => _vm.ShowAllTowar());
            AddCommand(collection, "Product", () => _vm.CreateView(new NowyTowarViewModel()));
            AddCommand(collection, "Categories", () => _vm.ShowAllCategories());
            AddCommand(collection, "Category", () => _vm.CreateView(new NewCategoryViewModel()));
            AddCommand(collection, "Clients", () => _vm.ShowAllClients());
            AddCommand(collection, "Client", () => _vm.CreateView(new NewClientViewModel()));
            AddCommand(collection, "Client Types", () => _vm.ShowAllClientTypes());
            AddCommand(collection, "Client Type", () => _vm.CreateView(new NewClientTypeViewModel()));
            AddCommand(collection, "Inventory", () => _vm.CreateView(new InventorySearchViewModel()));
        }

        private void LoadFinanceCommands(ObservableCollection<CommandViewModel> collection)
        {
            AddCommand(collection, "Grades", () => _vm.ShowAllGrades());
            AddCommand(collection, "Grade", () => _vm.CreateView(new NewGradeViewModel()));
            AddCommand(collection, "Payment Methods", () => _vm.ShowAllPaymentMethods());
            AddCommand(collection, "Payment Method", () => _vm.CreateView(new NewPaymentMethodViewModel()));
            AddCommand(collection, "Invoices", () => _vm.ShowAllInvoices());
            AddCommand(collection, "Invoice", () => _vm.CreateView(new NewInvoiceViewModel()));
            AddCommand(collection, "InvoiceStatuses", () => _vm.ShowAllInvoiceStatuses());
            AddCommand(collection, "Invoice Status", () => _vm.CreateView(new NewInvoiceStatusViewModel()));
            AddCommand(collection, "Sale Report", () => _vm.CreateView(new SaleReportViewModel()));
            AddCommand(collection, "Payroll Report", () => _vm.CreateView(new PayrollCostSummaryViewModel()));
        }

        private void LoadAdministrationCommands(ObservableCollection<CommandViewModel> collection)
        {
            AddCommand(collection, "Address", () => _vm.CreateView(new NewAddressViewModel()));
            AddCommand(collection, "Addresses", () => _vm.ShowAllAddresses());
            AddCommand(collection, "Aisles", () => _vm.ShowAllAisles());
            AddCommand(collection, "Aisle", () => _vm.CreateView(new NewAisleViewModel()));
            AddCommand(collection, "Sites", () => _vm.ShowAllSites());
            AddCommand(collection, "Site", () => _vm.CreateView(new NewSiteViewModel()));
            AddCommand(collection, "Locations", () => _vm.ShowAllLocations());
            AddCommand(collection, "Location", () => _vm.CreateView(new NewLocationViewModel()));
        }

        private void LoadAllCommands(ObservableCollection<CommandViewModel> collection)
        { 
            LoadHrCommands(collection);
            LoadMasterDataCommands(collection);
            LoadFinanceCommands(collection);
            LoadAdministrationCommands(collection);
        }
        #endregion // end of methods region
    }
}
