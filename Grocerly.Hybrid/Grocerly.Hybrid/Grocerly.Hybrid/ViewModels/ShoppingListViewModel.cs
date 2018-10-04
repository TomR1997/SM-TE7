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
    public class ShoppingListViewModel : BaseViewModel
    {
        public ShoppingListService ShoppingListService => DependencyService.Get<ShoppingListService>();
        public IEnumerable<Product> Products { get; set; }
        public Command RegisterCommand { get; set; }

        public ShoppingListViewModel()
        {
            Title = "Boodschappenlijst";
        }

        public async Task<IEnumerable<Product>> GetProductsForShoppingList(Guid id)
        {
            if (IsBusy)
                return null;

            IsBusy = true;

            try
            {
                Products = await ShoppingListService.GetProductsForShoppingList(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return Products;
        }
    }
}
