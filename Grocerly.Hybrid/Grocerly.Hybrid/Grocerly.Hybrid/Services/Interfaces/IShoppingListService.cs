using Grocerly.Hybrid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.Hybrid.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<IEnumerable<ShoppingList>> GetShoppingListsWithStatus(Status status);
        Task<IEnumerable<ShoppingListItem>> GetProductsForShoppingList(Guid id);
        Task<ShoppingList> CreateShoppingList();
        Task<Product> AddProductToList(Guid product_id, Guid shoppingList_id);
        Task<IEnumerable<ShoppingList>> GetShoppingListsForUser(Guid id, Status status);
        Task<bool> UpdateShoppingList(ShoppingList shoppingList);
        Task<ShoppingList> GetShoppingList(Guid id);
    }
}
