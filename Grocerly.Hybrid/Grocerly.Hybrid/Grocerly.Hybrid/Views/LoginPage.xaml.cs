using System;
using System.Collections.Generic;
using Grocerly.Hybrid.ViewModels;
using Xamarin.Forms;

namespace Grocerly.Hybrid.Views
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();

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
            }
            else
                error_label.IsVisible = true;

        }
    }
}
