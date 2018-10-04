using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Grocerly.Hybrid.Models;
using Xamarin.Forms;

namespace Grocerly.Hybrid.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {

        public ObservableCollection<Product> Products { get; set; }
        public Command LoadProductsCommand { get; set; }

        public ProductsViewModel()
        {
            
        }

        async Task ExecuteLoadProductsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Products.Clear();
                //var products = await DataStore.GetItemsAsync(true);
                //foreach (var p in products )
                //{
                //    Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
