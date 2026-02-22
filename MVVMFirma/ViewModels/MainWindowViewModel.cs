using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Helper;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using MVVMFirma.Models;
using System.Windows.Threading;
using System.Windows.Input;
using MVVMFirma.Services;

namespace MVVMFirma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields and Properties

        private DispatcherTimer _timer;         // Timer to update the current date and time.
        private DateTime _currentDateTime;                      

        // private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        // --- Session Service --- //
        private UserAccount _currentUser;
        
        public UserAccount CurrentUser => _currentUser;
        public string CurrentUserName => _currentUser != null ? _currentUser.Username : "Guest";
        public string CurrentUserDepartment => 
            (_currentUser?.Employee?.Department != null) 
            ? _currentUser.Employee.Department.DepartmentName : "No Department";

            
        public DateTime CurrentDateTime
        { 
            get => _currentDateTime;
            set
            { 
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    OnPropertyChanged(() => CurrentDateTime);
                }
            }
        }

        private MainModule _selectedModule;
        public MainModule SelectedModule
        {
            get => _selectedModule;
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
                    OnPropertyChanged(() => SelectedModule);

                    LoadCommandsForModule(value);
                }
            }
        }
        #endregion // End of Fields and properties region

        #region Constructors
        // Constructor with parameters (Constructor need parameters when we use dependency injection)
        public MainWindowViewModel(UserAccount currentUser)
        {
            _currentUser = currentUser;
            CurrentDateTime = DateTime.Now;
            StartClock();

            LoadCommandsForModule(MainModule.AllModules);
        }

        // Default constructor
        public MainWindowViewModel()
            : this (null)
        {     
        }
        #endregion

        #region Commands
        public ObservableCollection<CommandViewModel> Commands { get; }
            = new ObservableCollection<CommandViewModel>();

        /* 
         * The following Commands are used by the top bar to switch application modules
         * It trigger reloading of the side bar menu 
        */

        public ICommand ShowAllModulesCommand =>
            new BaseCommand(() => SelectedModule = MainModule.AllModules);

        public ICommand ShowHumanResourcesCommand =>
            new BaseCommand(() => SelectedModule = MainModule.HumanResources);

        public ICommand ShowMasterDataCommand =>
            new BaseCommand(() => SelectedModule = MainModule.MasterData);

        public ICommand ShowFinanceCommand =>
            new BaseCommand(() => SelectedModule = MainModule.Finance);

        public ICommand ShowAdministrationCommand =>
            new BaseCommand(() => SelectedModule = MainModule.Administration);

        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += this.OnWorkspaceRequestClose;
                }  
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                { 
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
                }
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }

        private void OnWorkspaceRequestNewView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        #endregion // Workspaces

        #region Helpers
        // Updates the side menu based on the cutrrently selected app module
        private void LoadCommandsForModule(MainModule module)
        {
            Commands.Clear();                                                                                   // remove previous commands (from module)

            MainWindowMenuService menuService = new MainWindowMenuService(this);                                // Delegates menu logic to a dedicated service 
            ObservableCollection<CommandViewModel> newCommands = menuService.GetCommandsForModule(module);      // retrieves commands gor the selectred module

            foreach (CommandViewModel command in newCommands) { Commands.Add(command); }                        // adds the commands collections tto the side menu view
        }
        internal void CreateView( WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);//dodajemy zakladke do kolekcji zakladek
            this.SetActiveWorkspace(workspace);//aktywujemy zakladke (zeby byla wlaczona)
        }
        // ---- The following methods show various All...ViewModels ----
        internal void ShowAllEmployees()
        {
            AllEmployeesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllEmployeesViewModel)
                as AllEmployeesViewModel;
            if (workspace == null)
            {
                workspace = new AllEmployeesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        internal void ShowAllAddresses()
        {
            AllAddressesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllAddressesViewModel)
                as AllAddressesViewModel;
            if (workspace == null)
            {
                workspace = new AllAddressesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        internal void ShowAllTowar()
        {
            WszystkieTowaryViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszystkieTowaryViewModel)
                as WszystkieTowaryViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieTowaryViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllAisles()
        {
            AllAislesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllAislesViewModel)
                as AllAislesViewModel;
            if (workspace == null)
            {
                workspace = new AllAislesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllSites()
        {
            AllSitesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllSitesViewModel)
                as AllSitesViewModel;
            if (workspace == null)
            {
                workspace = new AllSitesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllDepartments()
        {
            AllDepartmentsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllDepartmentsViewModel)
                as AllDepartmentsViewModel;
            if (workspace == null)
            {
                workspace = new AllDepartmentsViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllGrades()
        {
            AllGradesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllGradesViewModel)
                as AllGradesViewModel;
            if (workspace == null)
            {
                workspace = new AllGradesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllPositions()
        {
            AllPositionsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllPositionsViewModel)
                as AllPositionsViewModel;
            if (workspace == null)
            {
                workspace = new AllPositionsViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllPaymentMethods()
        {
            AllPaymentMethodsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllPaymentMethodsViewModel)
                as AllPaymentMethodsViewModel;
            if (workspace == null)
            {
                workspace = new AllPaymentMethodsViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllManagers()
        {
            AllManagersViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllManagersViewModel)
                as AllManagersViewModel;
            if (workspace == null)
            {
                workspace = new AllManagersViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllCategories()
        {
            AllCategoriesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllCategoriesViewModel)
                as AllCategoriesViewModel;
            if (workspace == null)
            {
                workspace = new AllCategoriesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllClients()
        {
            AllClientsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllClientsViewModel)
                as AllClientsViewModel;
            if (workspace == null)
            {
                workspace = new AllClientsViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllClientTypes()
        {
            AllClientTypesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllClientTypesViewModel)
                as AllClientTypesViewModel;
            if (workspace == null)
            {
                workspace = new AllClientTypesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllLocations()
        {
            AllLocationsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllLocationsViewModel)
                as AllLocationsViewModel;
            if (workspace == null)
            {
                workspace = new AllLocationsViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllInvoices()
        {
            AllInvoicesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllInvoicesViewModel)
                as AllInvoicesViewModel;
            if (workspace == null)
            {
                workspace = new AllInvoicesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        internal void ShowAllInvoiceStatuses()
        {
            AllInvoiceStatusesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllInvoiceStatusesViewModel)
                as AllInvoiceStatusesViewModel;
            if (workspace == null)
            {
                workspace = new AllInvoiceStatusesViewModel();
                workspace.RequestNewView += OnWorkspaceRequestNewView;
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion

        #region Time and Date Method
        private void StartClock()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            _timer.Tick += (s, e) => CurrentDateTime = DateTime.Now;
            _timer.Start();
        }
        #endregion
    }
}
