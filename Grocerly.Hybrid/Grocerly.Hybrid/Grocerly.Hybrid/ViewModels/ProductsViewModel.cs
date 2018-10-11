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
        public ShoppingListService ListStore => DependencyService.Get<ShoppingListService>();
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadProductsCommand { get; set; }
        public ICommand LoadMore { get; set; }

        public decimal CurrentPrice { get; set; }

        public int UniformColumns { get; set; }

        public string currentSearch = "";

        private Command<object> _ProductTapped;
        public Command<object> ProductTapped
        {
            get
            {
                return _ProductTapped = _ProductTapped ?? new Command<object>(async (x) => {
                    var item = x as Product;
                    await AddProductToShoppingList(item);
                });
            }
        }


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

        public async Task AddProductToShoppingList(Product product)
        {
            Guid shoppingListId = await CheckAndCreateShoppingList();

            try 
            {
                var newProduct = await ListStore.AddProductToList(product.Id, shoppingListId);
                CurrentPrice = decimal.Add(CurrentPrice, newProduct.Price);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task<Guid> CheckAndCreateShoppingList()
        {

            if (Application.Current.Properties.ContainsKey("ShoppingListId"))
            {
                return (Guid)Application.Current.Properties["ShoppingListId"];
            }
            
            ShoppingList shoppingList = await ListStore.CreateShoppingList();
            Application.Current.Properties["ShoppingListId"] = shoppingList.Id;
            
            return shoppingList.Id;
        }
      
    }
}
