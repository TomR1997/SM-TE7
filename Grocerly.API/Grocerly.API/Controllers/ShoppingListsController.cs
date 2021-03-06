using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grocerly.Database;
using Grocerly.Database.Pocos;
using Grocerly.API.Database.Pocos;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Grocerly.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/ShoppingLists")]
    public class ShoppingListsController : Controller
    {
        private readonly GrocerlyContext _context;

        public ShoppingListsController(GrocerlyContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingLists
        [HttpGet]
        public IEnumerable<ShoppingLists> GetShoppingLists(Status status = Status.Pending)
        {
            return _context.ShoppingLists;
        }

        // GET: api/ShoppingLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingLists([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingLists = await _context.ShoppingLists.SingleOrDefaultAsync(m => m.Id == id);

            if (shoppingLists == null)
            {
                return NotFound();
            }

            return Ok(shoppingLists);
        }

        // PUT: api/ShoppingLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingLists([FromRoute] Guid id, [FromBody] ShoppingLists shoppingLists)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingLists.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingLists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShoppingLists
        [HttpPost]
        public async Task<IActionResult> PostShoppingLists([FromBody] ShoppingLists shoppingLists, Guid user_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == user_id);

            if (users == null)
            {
                return NotFound();
            }
            shoppingLists.User = users;
            _context.ShoppingLists.Add(shoppingLists);
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                Debug.WriteLine(ex);
            }

            return CreatedAtAction("GetShoppingLists", new { id = shoppingLists.Id }, shoppingLists);
        }

        // DELETE: api/ShoppingLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingLists([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingLists = await _context.ShoppingLists.SingleOrDefaultAsync(m => m.Id == id);
            if (shoppingLists == null)
            {
                return NotFound();
            }

            _context.ShoppingLists.Remove(shoppingLists);
            await _context.SaveChangesAsync();

            return Ok(shoppingLists);
        }

        [HttpPost("{id_list}/products/{id_product}")]
        public async Task<IActionResult> AddProductToList ([FromRoute] Guid id_list, [FromRoute] Guid id_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingList = await _context.ShoppingLists.SingleOrDefaultAsync(m => m.Id == id_list);
            if (shoppingList == null)
            {
                return NotFound("Shoppinglist doesn't exist");
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id_product);
            if (product == null)
            {
                return NotFound("Product doesn't exist");
            }

            var shoppingListItem = await _context.ShoppinglistItems.SingleOrDefaultAsync(x => x.Id_Shoppinglist.Equals(shoppingList.Id) && x.Id_Product.Equals(product.Id));

            if(shoppingListItem != null)
            {
                shoppingListItem.Quantity++;
                _context.Entry(shoppingListItem).State = EntityState.Modified;
            }
            else
            {
                _context.ShoppinglistItems.Add(
                    new ShoppinglistItem
                    {
                        List = shoppingList,
                        Product = product,
                        Quantity = 1
                    });
            }

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id_list}/products/{id_product}")]
        public async Task<IActionResult> RemoveProductFromList([FromRoute] Guid id_list, [FromRoute] Guid id_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id_product);
            if (product == null)
            {
                return NotFound("Product doesn't exist");
            }

            var shoppingListItem = await _context.ShoppinglistItems.SingleOrDefaultAsync(m => m.Id_Shoppinglist.Equals(id_list) 
                && m.Id_Product.Equals(id_product));

            if(shoppingListItem == null)
            {
                return NotFound("Shoppinglist doesn't exist");
            }

            shoppingListItem.Quantity--;
            if (shoppingListItem.Quantity <= 0)
            {
                _context.Remove(shoppingListItem);
            }
            else
            {
                _context.Entry(shoppingListItem).State = EntityState.Modified;
            }


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/products")]
        public IActionResult GetProductsForList([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products =
                (from p in _context.Products
                 from sp in _context.ShoppinglistItems
                 where p.Id.Equals(sp.Id_Product) && sp.Id_Shoppinglist.Equals(id)
                 select new ProductsDTO
                 {
                     Id = sp.Product.Id,
                     Name = sp.Product.Name,
                     Price = sp.Product.Price,
                     CreationDate = sp.Product.CreationDate,
                     Volume = sp.Product.Volume,
                     Quantity = sp.Quantity
                 });

            return Ok(products);
        }

        private bool ShoppingListsExists(Guid id)
        {
            return _context.ShoppingLists.Any(e => e.Id == id);
        }
    }
}
