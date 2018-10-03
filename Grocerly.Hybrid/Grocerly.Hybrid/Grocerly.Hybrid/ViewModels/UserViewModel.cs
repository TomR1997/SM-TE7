using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grocerly.Hybrid.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public UserService userService => DependencyService.Get<UserService>();
        public bool RegisterSuccess { get; set; }
        public User User { get; set; }
        public Command RegisterCommand { get; set; }

        public UserViewModel()
        {
            Title = "Registeren";
        }

        public async Task TryRegister(string email, string username, string password)
        {
            await ExecuteRegisterCommand(email, username, password);
        }

        async Task ExecuteRegisterCommand(string email, string username, string password)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                User = await userService.RegisterAsync(email, username, password);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                RegisterSuccess = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
