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
        public UserService UserService => DependencyService.Get<UserService>();
        public User User { get; set; }

        public UserViewModel()
        {
            Title = "Registeren";
        }

        public async Task<bool> TryRegister(string email, string username, string password)
        {
            return await ExecuteRegisterCommand(email, username, password);
        }

        async Task<bool> ExecuteRegisterCommand(string email, string username, string password)
        {
            if (IsBusy)
                return false;

            IsBusy = true;

            try
            {
                User = await UserService.RegisterAsync(email, username, password);
                return true;
            }
            catch(Exception ex)
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
