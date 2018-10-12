using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
        public Command CalculatePriceCommand { get; set; }

        public ToolbarItem ShoppingCart { get; set; }

        public decimal CurrentPrice { get; set; }

        public int UniformColumns { get; set; }

        public string currentSearch = "";

        Command<object> _ProductTapped;
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
            CalculatePriceCommand = new Command(async () => await CalculateCurrentPrice());

            ShoppingCart = new ToolbarItem
            {
                Text = SetPrice(CurrentPrice)
            };

            int page = 2;
            LoadMore = new Command(async () => {
                var newProducts = await DataStore.GetProductsAsync(12, page, currentSearch);
                page += 1;
                foreach (var p in newProducts)
                {
                    Products.Add(p);
                }
            });
        }
        
        public async Task CalculateCurrentPrice()
        {
            if (Application.Current.Properties.ContainsKey("ShoppingListId"))
            {
                CurrentPrice = 0;
                var id = Application.Current.Properties["ShoppingListId"];
                var products = await ListStore.GetProductsForShoppingList((Guid)id);

                foreach (var p in products)
                {
                    decimal price = p.Price;
                    if (p.Quantity > 1)
                        price = p.Price * p.Quantity;

                    CurrentPrice = Decimal.Add(CurrentPrice, price);
                }

                ShoppingCart.Text = SetPrice(CurrentPrice);
            }
        }

        public async Task SearchProducts (int rows, int page, string name)
        {
            await ExecuteLoadProductsCommand(rows, page, name);
        }

        async Task ExecuteLoadProductsCommand(int rows = 12, int page = 1, string name = "")
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
                ShoppingCart.Text = SetPrice(CurrentPrice);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task<Guid> CheckAndCreateShoppingList()
        {
            ShoppingList shoppingList = new ShoppingList();

            if (Application.Current.Properties.ContainsKey("ShoppingListId"))
            {
                return (Guid)Application.Current.Properties["ShoppingListId"];
            }
            try
            {
                shoppingList = await ListStore.CreateShoppingList();
                Application.Current.Properties["ShoppingListId"] = shoppingList.Id;
                await Application.Current.SavePropertiesAsync();
            }catch(Exception ex){
                Debug.WriteLine(ex);
            }
            
            return shoppingList.Id;
        }

        string SetPrice(decimal price)
        {
            return price.ToString("C2");
        }
      
    }
}
