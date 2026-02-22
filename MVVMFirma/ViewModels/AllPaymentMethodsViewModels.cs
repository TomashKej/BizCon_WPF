using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Models;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.ViewModels.Abstract;
using System.Windows;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllPaymentMethodsViewModel : WszystkieViewModel<PaymentMethodForAllViews>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<PaymentMethodForAllViews>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from paymentMethod in bizConDbEntities.PaymentMethod
                    select new PaymentMethodForAllViews
                    {
                        PaymentMethodName = paymentMethod.PaymentMethodName,
                        Notes = paymentMethod.Notes

                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllPaymentMethodsViewModel()
            : base()
        {
            base.DisplayName = "PaymentMethod";
        }
        #endregion
        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Payment Method Name" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Payment Method Name") // if we sort by payment method name
            {
                List = new ObservableCollection<PaymentMethodForAllViews>(List.OrderBy(item => item.PaymentMethodName));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Payment Method Name" };
        }

        public override void Find()
        {
            if (FindPhrase == "Payment Method Name") // if we search by payment method name
            {
                List = new ObservableCollection<PaymentMethodForAllViews>
                    (
                        List.Where(item => item.PaymentMethodName != null && item.PaymentMethodName.StartsWith(FindTextBox))
                    );
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewPaymentMethodViewModel();
        }

        #endregion

    }
}