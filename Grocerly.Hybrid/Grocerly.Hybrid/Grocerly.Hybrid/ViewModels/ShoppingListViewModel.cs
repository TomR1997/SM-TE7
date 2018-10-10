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

        public ShoppingListViewModel()
        {
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
                shoppingList = await ShoppingListService.CreateShoppingList();
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
    }
}
