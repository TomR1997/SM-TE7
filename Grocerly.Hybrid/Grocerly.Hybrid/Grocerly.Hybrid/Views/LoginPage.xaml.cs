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
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string Username = username.Text;
            string Password = password.Text;

            await viewModel.TryLogin(Username, Password);
           
            await Navigation.PopAsync();
        }
    }
}
