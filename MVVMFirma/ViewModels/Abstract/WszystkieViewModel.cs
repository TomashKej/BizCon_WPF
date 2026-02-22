using MVVMFirma.Helper;
using MVVMFirma.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels.Abstract
{

    // that class has an astract functions so it is abstract as well
    public abstract class WszystkieViewModel<T>: WorkspaceViewModel //T is a generic type parameter 
    {
        #region BazaDanych
        // --- That is the database object ---
        protected readonly BizConDbEntities bizConDbEntities;
        #endregion
        #region Command
        //komendy, to takie cos co podlacza się pod element widoku (np. przycisk) i ona wywoluje funkcje, czyli my ...
        //to jest komenda do ladowania obiektow z bazy, ona zostanie podpieta pod przycisk odswiez
        //_ oznacza, że dane pole będzie miało propertiesa
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null) _LoadCommand = new BaseCommand(Load); //that command will call the Load method defined below
                return _LoadCommand;
            }
        }
        #endregion
        #region Lista
        // --- That list will hold all items of type T ---
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) Load();//jezeli lista jest pusta to ją ladujemy metodą load
                return _List;
            }
            set
            {
                if (_List != value)
                {
                    _List = value;
                    OnPropertyChanged(() => List);//odswieza wyswietlanie listy obiektów 
                }
            }
        }
        #region AddCommand

        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                    _AddCommand = new BaseCommand(Add);
                return _AddCommand;
            }
        }
        private void Add()
        {
            AddRecordToDatabase(CreateNewViewModel());
        }
        protected abstract WorkspaceViewModel CreateNewViewModel();

        // the following code will be used to open a new tab with posibility to add a new item of type T

        public event Action<WorkspaceViewModel> RequestNewView;
        public void AddRecordToDatabase(WorkspaceViewModel viewModel)
        {
            RequestNewView?.Invoke(viewModel);
        }

        #endregion
        //...
        public abstract void Load();
        #endregion
        #region Konstruktor
        public WszystkieViewModel()
        {
            //tworzenie obiektu z bd
            bizConDbEntities = new BizConDbEntities();
        }
        #endregion

        #region Searching and Filtering
        // That is command attached to the sort button
        private BaseCommand _SortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null) _SortCommand = new BaseCommand(Sort);//This command will call the Sort method defined below
                return _SortCommand;
            }
        }
        // That is the field where we can type the sort phrase
        public string SortPhrase { get; set; }

        // That is property returning list of items for ComboBox used for sorting
        public List<string> SortComboBoxItems
        {
            get
            {
                return getComboBoxSortList();
            }
        }

        // That is command attached to the find button
        private BaseCommand _FindCommand;
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null) _FindCommand = new BaseCommand(Find);//This command will call the Find method defined below
                return _FindCommand;
            }
        }
        // That is the field where we can type the find phrase
        public string FindPhrase { get; set; }

        // That is property returning list of items for ComboBox used for finding
        public List<string> FindComboBoxItems
        {
            get
            {
                return getComboBoxFindList();
            }
        }

        // The field where we can type the name of the TextBox to be found
        public string FindTextBox { get; set; }

        public abstract void Sort();                                 // We will decide in abstract classes, how to sort the specific tables
        public abstract void Find();                                 // We will decide in specific tables how to find specific items
        public abstract List<string> getComboBoxSortList();          // Here we will decide in specific inherited classes after what we can sort
        public abstract List<string> getComboBoxFindList();          // Here we will decide in specific inherited classes after what we can find
        #endregion // End of Searching and Filtering Region
    }
}
