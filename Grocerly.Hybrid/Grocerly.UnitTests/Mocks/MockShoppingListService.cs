using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.UnitTests.Mocks
{
    class MockShoppingListService : IShoppingListService
    {
        public Task<Product> AddProductToList(Guid product_id, Guid shoppingList_id)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingList> CreateShoppingList()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingListItem>> GetProductsForShoppingList(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingList> GetShoppingList(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingList>> GetShoppingListsForUser(Guid id, Status status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingList>> GetShoppingListsWithStatus(Status status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateShoppingList(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }
    }
}
