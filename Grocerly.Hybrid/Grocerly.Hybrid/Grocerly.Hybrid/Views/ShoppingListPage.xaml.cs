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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadListsAndProductsCommand.Execute(null);
        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void RequestShoppingList(object sender, EventArgs e)
        {
            await viewModel.GetShoppingList();
            if (viewModel.ShoppingList != null)
            {
                viewModel.ShoppingList.Status = Status.Pending;
                var updated = await viewModel.UpdateShoppingList(viewModel.ShoppingList);
                if (!updated)
                    error_label.IsVisible = true;
                else
                    await viewModel.GetProductsForShoppingList();
            }
            else
                error_label.IsVisible = true;
        }
    }
}
