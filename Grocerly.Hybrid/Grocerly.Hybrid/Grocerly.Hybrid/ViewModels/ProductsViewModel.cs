using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services;
using Xamarin.Forms;

namespace Grocerly.Hybrid.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        public ProductService DataStore => DependencyService.Get<ProductService>();
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadProductsCommand { get; set; }

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadProductsCommand());
        }

        public async Task SearchProducts (int rows, int page, string name)
        {
            await ExecuteLoadProductsCommand(rows, page, name);
        }

        async Task ExecuteLoadProductsCommand(int rows = 15, int page = 1, string name = "")
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();
                var products = await DataStore.GetProductsAsync(rows, page, name);
                foreach (var p in products )
                {
                    Products.Add(p);
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
        }
    }
}
