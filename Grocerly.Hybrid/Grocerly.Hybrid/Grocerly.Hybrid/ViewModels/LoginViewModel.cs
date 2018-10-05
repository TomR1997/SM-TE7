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
        public User User { get; set; }
        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            Title = "Inloggen";
        }

        public async Task<bool> TryLogin(string username, string password){
           return await ExecuteLoginCommand(username, password);
        }

        async Task<bool> ExecuteLoginCommand(string username, string password)
        {

            IsBusy = true;

            try
            {
                User = await AuthService.LoginAsync(username, password);
                App.user = User;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
