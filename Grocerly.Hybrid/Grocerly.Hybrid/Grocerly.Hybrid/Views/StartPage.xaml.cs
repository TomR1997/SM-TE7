using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Grocerly.Hybrid.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        async void LoginClick (object sender, EventArgs e){
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
