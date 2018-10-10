using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public ICommand LoadMore { get; set; }

        public int UniformColumns { get; set; }

        public string currentSearch = "";
         

        public ProductsViewModel()
        {
            UniformColumns = Device.Idiom == TargetIdiom.Phone ? 2 : 3;

            Products = new ObservableCollection<Product>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadProductsCommand());

            int page = 2;
            this.LoadMore = new Command(async () => {
                var newProducts = await DataStore.GetProductsAsync(12, page, currentSearch);
                page += 1;
                foreach (var p in newProducts)
                {
                    Products.Add(p);
                }
            });
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
