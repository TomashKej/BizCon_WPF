using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.ViewModels
{
    public class NowaFakturaViewModel : WorkspaceViewModel //bo wszystkie zakładki dziedzicza po WorkspaceVM
    {
        public NowaFakturaViewModel()
        {
            base.DisplayName = "Faktura";//to jest ustawienie tytulu zakladki 
        }
    }
}
