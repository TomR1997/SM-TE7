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
        public List<Product> Products { get; set; }

        public ShoppingListPage()
        {
            InitializeComponent();
            viewModel = new ShoppingListViewModel();

            //Products = await viewModel.GetProductsForShoppingList(new Guid());

            Products = new List<Product>();
            for (int i = 0; i < 20; i++)
            {
                Products.Add(new Product { Name = "Item" + i, Price = 5, Quantity = "500g", CreationDate = DateTime.Now });
            }

            ShoppinglistListView.ItemsSource = Products;
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
