using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grocerly.Database;
using Grocerly.Database.Pocos;

namespace Grocerly.API.Controllers
{
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
        public IEnumerable<ShoppingLists> GetShoppingLists()
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
        public async Task<IActionResult> PostShoppingLists([FromBody] ShoppingLists shoppingLists)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShoppingLists.Add(shoppingLists);
            await _context.SaveChangesAsync();

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

            _context.ShoppinglistItems.Add(
                new ShoppinglistItem
                {
                    List = shoppingList,
                    Product = product
                });

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ShoppingListsExists(Guid id)
        {
            return _context.ShoppingLists.Any(e => e.Id == id);
        }
    }
}