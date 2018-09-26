using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.ServiceModel
{
    [Route("/shoppinglists/", "GET")]
    public class GetShoppingLists : IReturn<List<ShoppingListResponse>>
    {
    }

    [Route("/shoppinglists/{Id}", "GET")]
    public class GetShoppingList : IReturn<ShoppingListResponse>
    {
        public Guid Id { get; set; }
    }
    public class ShoppingListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
