using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services;
using Xamarin.Forms;

namespace Grocerly.Hybrid.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public AuthService AuthService => DependencyService.Get<AuthService>();
        public bool LoginSucces { get; set; }
        public User User { get; set; }
        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            Title = "Inloggen";
        }

        public async Task TryLogin(string username, string password){
            await ExecuteLoginCommand(username, password);
        }

        async Task ExecuteLoginCommand(string username, string password)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                User = await AuthService.LoginAsync(username, password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                LoginSucces = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
