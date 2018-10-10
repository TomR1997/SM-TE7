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
        public Command RegisterCommand { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }

        public UserViewModel()
        {
            ShoppingLists = new List<ShoppingList>();
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

        public async Task<List<ShoppingList>> GetShoppingListsForUser(Guid id, Status status)
        {
            if (IsBusy)
                return ShoppingLists;

            IsBusy = true;

            try
            {
                var shoppingLists = await UserService.GetShoppingListsForUser(id, status);
                foreach(var s in shoppingLists)
                {
                    ShoppingLists.Add(s);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return ShoppingLists;
        }
    }
}
