using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grocerly.Hybrid.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public ShoppingListService ShoppingListService => DependencyService.Get<ShoppingListService>();
        public ObservableCollection<ShoppingListItem> ShoppingListItems { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }

        public ShoppingListViewModel()
        {
            ShoppingLists = new List<ShoppingList>();
            ShoppingListItems = new ObservableCollection<ShoppingListItem>();
            Title = "Boodschappenlijst";
        }

        public async Task GetProductsForShoppingList(Guid id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ShoppingListItems.Clear();
                var items = await ShoppingListService.GetProductsForShoppingList(id);
                foreach(var i in items)
                {
                    ShoppingListItems.Add(i);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<ShoppingList> CreateShoppingList(string name, Status status)
        {
            ShoppingList shoppingList = new ShoppingList();

            if (IsBusy)
                return shoppingList;

            IsBusy = true;

            try
            {
                shoppingList = await ShoppingListService.CreateShoppingList(name, status);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return shoppingList;
        }


        public async Task<List<ShoppingList>> GetShoppingListsForUser(Guid id, Status status)
        {
            if (IsBusy)
                return ShoppingLists;

            IsBusy = true;

            try
            {
                var shoppingLists = await ShoppingListService.GetShoppingListsForUser(id, status);
                foreach (var s in shoppingLists)
                {
                    ShoppingLists.Add(s);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return ShoppingLists;
        }
    }
}
