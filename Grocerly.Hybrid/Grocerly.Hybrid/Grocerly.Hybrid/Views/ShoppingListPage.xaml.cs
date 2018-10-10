using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grocerly.Hybrid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPage : ContentPage
    {
        private ShoppingListViewModel viewModel;

        public ShoppingListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ShoppingListViewModel();
            GetProductsForShoppingList();
        }

        async void GetProductsForShoppingList()
        {
            //var shoppingLists = await viewModel.GetShoppingListsForUser(App.user.Id, Status.Open);
            //if (shoppingLists.Count() <= 0)
            //{
            //    await viewModel.CreateShoppingList("New shoppinglist", Status.Open);
            //}
            //else
            //{
            //    ShoppingList shoppingList = shoppingLists.Single();
            //    await viewModel.GetProductsForShoppingList(shoppingList.Id);
            //}

            //await viewModel.GetProductsForShoppingList(new Guid("c0c5870b-636e-4a52-a723-6cc598e24e6c"));

        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
