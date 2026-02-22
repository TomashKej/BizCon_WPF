using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace MVVMFirma.ViewModels
{
    public class AllClientsViewModel : WszystkieViewModel<ClientForAllView>
    {
        #region Lista
        public override void Load()
        {
            List = new ObservableCollection<ClientForAllView>
                (
                    //3.Wybieramy tylko okreslone kolumy towaru
                    from client in bizConDbEntities.Client
                    select new ClientForAllView 
                    {
                        ClientName = client.ClientName,
                        PhoneNumber = client.PhoneNumber,
                        EmailAddress = client.EmailAddress,
                        AddressLn1 = client.Address.AddressLine1,
                        City = client.Address.City,
                        PostCode = client.Address.PostCode,
                        County = client.Address.County,
                        Country = client.Address.Country,
                        ClientTypeName = client.ClientType.ClientType1,
                        Notes = client.Notes
                    }
                );
        }
        #endregion
        #region Konstruktor
        public AllClientsViewModel()
            : base()
        {
            base.DisplayName = "Clients";
        }
        #endregion
        #region SortingAndFiltering
        // decydujemy po czym sortowac
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Client Name", "Client Type", "Country", "County", "City", "Post Code" };
        }

        public override void Sort()
        {
            if (SortPhrase == "Client Name") // if we sort by client name
            {
                List = new ObservableCollection<ClientForAllView>(List.OrderBy(item => item.ClientName));
            }
            else if (SortPhrase == "Client Type") // if we sort by client type
            {
                List = new ObservableCollection<ClientForAllView>(List.OrderBy(item => item.ClientTypeName));
            }
            else if (SortPhrase == "Country") // if we sort by country
            {
                List = new ObservableCollection<ClientForAllView>(List.OrderBy(item => item.Country));
            }
            else if (SortPhrase == "County") // if we sort by county
            {
                List = new ObservableCollection<ClientForAllView>(List.OrderBy(item => item.County));
            }
            else if (SortPhrase == "City") // if we sort by city
            {
                List = new ObservableCollection<ClientForAllView>(List.OrderBy(item => item.City));
            }
            else if (SortPhrase == "Post Code") // if we sort by post code
            {
                List = new ObservableCollection<ClientForAllView>(List.OrderBy(item => item.PostCode));
            }
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "Client Name", "Client Type", "Country", 
                                      "County", "City", "Post Code", 
                                      "Phone Number", "Email", "Address", "Notes" };
        }

        public override void Find()
        {
            if (FindPhrase == "Client Name") // if we find by client name
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.ClientName != null && item.ClientName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Client Type") // if we find by client type
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.ClientTypeName != null && item.ClientTypeName.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Country") // if we find by country
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.Country != null && item.Country.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "County") // if we find by county
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.County != null && item.County.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "City") // if we find by city
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.City != null && item.City.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Post Code") // if we find by post code
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.PostCode != null && item.PostCode.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Phone Number") // if we find by phone number
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.PhoneNumber != null && item.PhoneNumber.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Email") // if we find by email
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.EmailAddress != null && item.EmailAddress.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Address") // if we find by address
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.AddressLn1 != null && item.AddressLn1.StartsWith(FindTextBox)));
            }
            else if (FindPhrase == "Notes") // if we find by notes
            {
                List = new ObservableCollection<ClientForAllView>(List.Where(item => item.Notes != null && item.Notes.StartsWith(FindTextBox)));
            }
        }

        protected override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewClientViewModel();
        }
        #endregion
    }
}
