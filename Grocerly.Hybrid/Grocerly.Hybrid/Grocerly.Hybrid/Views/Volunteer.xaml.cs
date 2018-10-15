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
    public partial class Volunteer : ContentPage
    {
        VolunteerViewModel viewModel;

        public Volunteer()
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
    }
}