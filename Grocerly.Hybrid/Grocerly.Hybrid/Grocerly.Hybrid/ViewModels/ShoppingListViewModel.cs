﻿using Grocerly.Hybrid.Models;
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
        public ShoppingList ShoppingList { get; set; }

        public Command LoadListsAndProductsCommand { get; set; }

        public ShoppingListViewModel()
        {
            ShoppingLists = new ObservableCollection<ShoppingList>();
            ShoppingListItems = new ObservableCollection<ShoppingListItem>();
            Title = "Boodschappenlijst";

            LoadListsAndProductsCommand = new Command(async () => await GetProductsForShoppingList());
        }

        public async Task GetProductsForShoppingList()
        {
            var shoppingListId = await CheckAndCreateShoppingList();
            await GetProductsForShoppingList(shoppingListId);
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
                    i.Price = i.Price * i.Quantity;
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
                ShoppingLists.Clear();
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

        public async Task<bool> UpdateShoppingList(ShoppingList shoppingList)
        {
            IsBusy = true;

            try
            {
                await ShoppingListService.UpdateShoppingList(shoppingList);
                if (Application.Current.Properties.ContainsKey("ShoppingListId"))
                {
                    Application.Current.Properties.Remove("ShoppingListId");
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetShoppingList()
        {
            IsBusy = true;

            try
            {
                var shoppingListId = await CheckAndCreateShoppingList(); 
                ShoppingList = await ShoppingListService.GetShoppingList(shoppingListId);
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

        private async Task<Guid> CheckAndCreateShoppingList()
        {
            await GetShoppingListsForUser(App.user.Id, Status.Open);

            if (ShoppingLists.Count > 0)
            {
                Application.Current.Properties["ShoppingListId"] = ShoppingLists[0].Id;
                await Application.Current.SavePropertiesAsync();
            }

            ShoppingList shoppingList = new ShoppingList();

            if (Application.Current.Properties.ContainsKey("ShoppingListId"))
            {
                return (Guid)Application.Current.Properties["ShoppingListId"];
            }
            try
            {
                shoppingList = await CreateShoppingList();
                Application.Current.Properties["ShoppingListId"] = shoppingList.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return shoppingList.Id;
        }
    }
}
