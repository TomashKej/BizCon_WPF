using MVVMFirma.Helper;
using MVVMFirma.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels.Abstract
{
    public abstract class JedenViewModel<T> : WorkspaceViewModel, IDataErrorInfo //T to jest typ, ktory bedzie dodawany, np. Towar
    {

        #region Data Base
        protected BizConDbEntities bizConDbEntities;
        protected T item;
        #endregion // End of Data Base region

        #region Constructor
        public JedenViewModel()
        {
            bizConDbEntities = new BizConDbEntities();
        }
        #endregion // End of Constructor region

        #region Commands
        // IDataErrorInfo implementation
        public String Error => string.Empty;
        public String this[string columnName] => ValidateProperty(columnName);
        protected virtual string ValidateProperty(string propertyName)
        {
            return String.Empty;
        }

        // SaveAndClose Command
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null) _SaveAndCloseCommand = new BaseCommand(saveAndClose);//ta komenda wywola metode saveAndClose, ktora jest zdefiniowana nizej
                return _SaveAndCloseCommand;
            }
        }
        public abstract void Save();
        private void saveAndClose()
        {
            try
            {
                // The following method checks all properties for validation errors using reflection.
                PropertyInfo[] propertyInfo = this.GetType().GetProperties();
                foreach (PropertyInfo property in propertyInfo)
                {

                    if (!string.IsNullOrWhiteSpace(ValidateProperty(property.Name)))
                    {
                        ShowMessageBox("Some data is incorrect or missing. Please check the input fields.");
                        return;
                    }
                }

                Save();
                //zamykamy zakladke przez metode z WorkspaceViewModel
                OnRequestClose();//koniecznie zmien w WorkspaceViewModel na protected tę metode
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ShowMessageBox("Error while saving data: " + ex.Message);
            }
        }

        #endregion // End of Commands region

    }
}
