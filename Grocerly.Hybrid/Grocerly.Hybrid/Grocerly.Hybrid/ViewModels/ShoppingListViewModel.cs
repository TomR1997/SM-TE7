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
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; }

        public Command LoadListsAndProductsCommand { get; set; }

        public ShoppingListViewModel()
        {
            ShoppingLists = new ObservableCollection<ShoppingList>();
            ShoppingListItems = new ObservableCollection<ShoppingListItem>();
            Title = "Boodschappenlijst";

            LoadListsAndProductsCommand = new Command(async () => await GetProductsForShoppingList());
        }

        async Task GetProductsForShoppingList()
        {
            await GetShoppingListsForUser(App.user.Id, Status.Open);
            if (ShoppingLists.Count == 0)
            {
               // await CreateShoppingList();
            }
            else
            {
                //ShoppingList shoppingList = ShoppingLists[0];
                await GetProductsForShoppingList(new Guid("eb5886b9-e7e8-4351-a38f-f816ceaca36e"));
            }

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

        public async Task<ShoppingList> CreateShoppingList()
        {
            ShoppingList shoppingList = new ShoppingList();

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


        public async Task GetShoppingListsForUser(Guid id, Status status)
        {

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

            
        }
    }
}
