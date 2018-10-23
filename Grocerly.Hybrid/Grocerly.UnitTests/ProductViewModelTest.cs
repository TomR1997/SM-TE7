using Grocerly.Hybrid.ViewModels;
using Grocerly.UnitTests.Mocks;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Grocerly.UnitTests
{
    public class ProductViewModelTest
    {
        ProductsViewModel viewModel;

        [OneTimeSetUp]
        public void Setup()
        {
            viewModel = new ProductsViewModel(
                     new MockProductService(), new MockShoppingListService()
                     );
                    
        }

        [Test]
        public async Task LoadProductsTest()
        {
            await viewModel.SearchProducts(15, 1, "");

            Assert.NotZero(viewModel.Products.Count);
        }
    }
}
