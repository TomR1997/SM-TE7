﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grocerly.Hybrid.Services;
using Grocerly.Hybrid.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Grocerly.Hybrid
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static string BaseApiUrl = "http://192.168.2.12:8000";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new MainPage();
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