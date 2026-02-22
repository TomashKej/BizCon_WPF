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
    public class AllCategoriesViewModel : WszystkieViewModel<CategoryForAllViews>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<CategoryForAllViews>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from category in bizConDbEntities.Category
                    select new CategoryForAllViews
                    {
                        CategoryName = category.CategoryName,
                        Notes = category.Notes
                    }
                );

        }
        #endregion
        #region Konstruktor
        public AllCategoriesViewModel()
            : base()
        {
            base.DisplayName = "Categories";
        }
        #endregion
        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Category Name" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Category Name") // jesli sortujemy po nazwie kategorii
            {
                List = new ObservableCollection<CategoryForAllViews>(List.OrderBy(item => item.CategoryName));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Ctegory Name", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Category Name") // jesli szukamy po nazwie kategorii
            {
                List = new ObservableCollection<CategoryForAllViews>(List.Where(item => item.CategoryName != null && item.CategoryName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes") // jesli szukamy po notatkach
            {
                List = new ObservableCollection<CategoryForAllViews>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewCategoryViewModel();
        }
        #endregion

    }
}