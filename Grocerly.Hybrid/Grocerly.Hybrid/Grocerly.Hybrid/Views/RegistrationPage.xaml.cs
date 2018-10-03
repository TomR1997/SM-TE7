using Grocerly.Hybrid.Models;
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
        private HttpClient client;
		public RegistrationPage()
		{
			InitializeComponent ();
            Init();
            client = new HttpClient();
		}
        
        async void Register(object sender, EventArgs e)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Email = Entry_Email.Text,
                Username = Entry_Username.Text,
                Password = Entry_Password.Text
            }), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://i315103core.venus.fhict.nl/api/users", content).ConfigureAwait(false);
            /*if (result.IsSuccessStatusCode)
            {
                DisplayAlert("Register", "Register successful", "Ok");
            }*/
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
            Entry_Password.Completed += (s, e) => Register(s, e);
        }
	}
}