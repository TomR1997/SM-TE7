using System;
using System.Collections.Generic;
using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.ViewModels;
using Xamarin.Forms;

namespace Grocerly.Hybrid.Views
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        UserViewModel userViewModel;
        ShoppingListViewModel shoppingListViewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
            userViewModel = new UserViewModel();
            shoppingListViewModel = new ShoppingListViewModel();

            username.Focused += (s, ev) => error_label.IsVisible = false;
            username.Completed += (s, ev) => password.Focus();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            bool isValid = await viewModel.TryLogin(username.Text, password.Text);



            if (isValid)
            {
                App.isLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();

                await shoppingListViewModel.GetShoppingListsForUser(App.user.Id, Status.Open);

                if (shoppingListViewModel.ShoppingLists.Count == 0)
                {
                   var shoppingList = await shoppingListViewModel.CreateShoppingList();
                   // Application.Current.Properties["ShoppingListId"] = shoppingList.Id;
                    await Application.Current.SavePropertiesAsync();
                }


            }
            else
                error_label.IsVisible = true;

        }
    }
}
