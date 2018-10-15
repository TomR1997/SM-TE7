using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grocerly.Hybrid.ViewModels
{
    class VolunteerViewModel : BaseViewModel
    {
        public ShoppingListService ShoppingListService => DependencyService.Get<ShoppingListService>();
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; }
        
        public Command GetAvailableListsCommand { get; set; }

        public VolunteerViewModel()
        {

        }

        async Task GetAvailableShoppinglists()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ShoppingLists.Clear();
                var lists = await ShoppingListService.GetShoppingListsWithStatus(Status.Pending);
                foreach (var sl in lists)
                {
                    ShoppingLists.Add(sl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
