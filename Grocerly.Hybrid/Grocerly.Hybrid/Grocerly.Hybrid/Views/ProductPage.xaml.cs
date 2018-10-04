using System;
using System.Collections.Generic;
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Products.Count == 0)
                viewModel.LoadProductsCommand.Execute(null);
        }

        async void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            await viewModel.SearchProducts(12, 1, productSearch.Text);
        }
    }
}
