﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grocerly.Hybrid.Services;
using Grocerly.Hybrid.Views;
using Grocerly.Hybrid.Models;
using Newtonsoft.Json;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Grocerly.Hybrid
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static string BaseApiUrl = "https://i315103core.venus.fhict.nl";
        public static bool UseMockDataStore = true;

        public static bool isLoggedIn;
        public static User user;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            DependencyService.Register<AuthService>();
            DependencyService.Register<UserService>();
            DependencyService.Register<ProductService>();

            DependencyService.Register<ShoppingListService>();

            isLoggedIn = Properties.ContainsKey("jwt");

            if (isLoggedIn)
            {
                if (Current.Properties.ContainsKey("User"))
                {
                    var user = Current.Properties["User"];
                    App.user = JsonConvert.DeserializeObject<User>(user.ToString());
                }
                MainPage = new MainPage();
            }
            else
            {
                MainPage = (Page)new NavigationPage(new StartPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
