using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grocerly.Hybrid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
	{
        private UserViewModel viewModel; 
		public RegistrationPage()
		{
			InitializeComponent ();
            Init();
            viewModel = new UserViewModel();
		}
        
        async void Register(object sender, EventArgs e)
        {
            string role = "User";
            bool isVolunteer = Switch_Roles.IsToggled;
            if (isVolunteer)
            {
                role = "Volunteer";
            }

            bool success = await viewModel.TryRegister(Entry_Email.Text, Entry_Username.Text, Entry_Password.Text, role);

            if (success)
                await Navigation.PushAsync(new LoginPage());
            else
                error_label.IsVisible = true;
                
        }

        void Init()
        {
            BackgroundColor = Color.White;
            Lbl_Email.TextColor = Color.Black;
            Lbl_Username.TextColor = Color.Black;
            Lbl_Password.TextColor = Color.Black;
            ActivitySpinner.IsVisible = false;
            RegisterIcon.HeightRequest = 120;

            Entry_Email.Completed += (s, e) => Entry_Username.Focus();
            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
        }
	}
}