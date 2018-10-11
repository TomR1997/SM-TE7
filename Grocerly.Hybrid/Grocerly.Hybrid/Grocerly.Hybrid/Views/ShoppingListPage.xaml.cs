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

            if (viewModel.ShoppingListItems.Count == 0)
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
    }
}
