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
    public class WszystkieTowaryViewModel : WszystkieViewModel<ProductForAllView>
    {
        #region List
        public override void Load()
        {
            List = new ObservableCollection<ProductForAllView>
            (
                from product in bizConDbEntities.Product
                where product.IsActive == true
                select new ProductForAllView
                {
                    ProductName = product.ProductName,
                    Batch = product.Batch,
                    Sku = product.Sku,
                    CategoryName = product.Category.CategoryName,
                    ClientName = product.Client.ClientName,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity,
                    SiteName = product.Site.SiteName,
                    AisleName = product.Aisle.AisleName,
                    LocationName = product.Location.LocationName
                }
            );
        }
        #endregion // End of List Constructor

        #region Konstruktor
        public WszystkieTowaryViewModel()
            :base() 
        {
            base.DisplayName = "Products";
        }
        #endregion

        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Product Name", "Category", "Client", "Unit Price", "Quantity", "Site","Aisle", "Location" };
        }

       
        public override void Sort()
        {
           if (SortPhrase == "Product Name")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.ProductName));
            }
            else if (SortPhrase == "Category")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.CategoryName));
            }
            else if (SortPhrase == "Client")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.ClientName));
            }
            else if (SortPhrase == "Unit Price")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.UnitPrice));
            }
            else if (SortPhrase == "Quantity")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.Quantity));
            }
            else if (SortPhrase == "Site")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.SiteName));
            }
            else if (SortPhrase == "Aisle")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.AisleName));
            }
            else if (SortPhrase == "Location")
            {
                List = new ObservableCollection<ProductForAllView>(List.OrderBy(item => item.LocationName));
            }
        }
        // decydujemy po czym wyszukiwac
        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Product Name", "Category", "Client", "SKU", "Batch", "Site", "Aisle", "Location" };
        }

        public override void Find()
        {
            if (FindPhrase == "Product Name")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Category")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.CategoryName != null && item.CategoryName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Client")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.ClientName != null && item.ClientName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "SKU")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.Sku != null && item.Sku.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Batch")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.Batch != null && item.Batch.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Site")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.SiteName != null && item.SiteName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Aisle")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.AisleName != null && item.AisleName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Location")
            {
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.LocationName != null && item.LocationName.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NowyTowarViewModel();
        }
        #endregion


    }
}