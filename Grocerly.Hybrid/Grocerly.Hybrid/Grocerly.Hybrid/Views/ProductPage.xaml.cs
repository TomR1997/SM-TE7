using System;
using System.Collections.Generic;
using System.Globalization;
using Grocerly.Hybrid.ViewModels;
using Xamarin.Forms;

namespace Grocerly.Hybrid.Views
{
    public partial class ProductPage : ContentPage
    {
        ProductsViewModel viewModel;


        public ProductPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductsViewModel();

            productSearch.SearchButtonPressed += async (s, e) => await viewModel.SearchProducts(12, 1, productSearch.Text);


            ToolbarItems.Add(viewModel.ShoppingCart);
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.CalculatePriceCommand.Execute(null);

            if (viewModel.Products.Count == 0)
                viewModel.LoadProductsCommand.Execute(null);
        }

        async void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.currentSearch = productSearch.Text;
            await viewModel.SearchProducts(12, 1, productSearch.Text);
        }
        
    }
}
