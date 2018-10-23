using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grocerly.Hybrid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VolunteerPage : ContentPage
    {
        VolunteerViewModel viewModel;

        public VolunteerPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new VolunteerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ShoppingLists.Count == 0)
                viewModel.GetAvailableListsCommand.Execute(null);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var shoppingList = e.Item as ShoppingList;
            Application.Current.Properties["ShoppingListId"] = shoppingList.Id;
            await Application.Current.SavePropertiesAsync();
        }
    }
}