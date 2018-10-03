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
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage()
		{
			InitializeComponent ();
            Init();
		}
        
        /*async*/ void Register(object sender, EventArgs e)
        {
            DisplayAlert("Register", "Register successful", "Ok");
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