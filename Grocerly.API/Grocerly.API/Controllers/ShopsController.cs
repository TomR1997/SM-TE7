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
    [Route("api/Shops")]
    public class ShopsController : Controller
    {
        private readonly GrocerlyContext _context;

        public ShopsController(GrocerlyContext context)
        {
            _context = context;
        }

        // GET: api/Shops
        [HttpGet]
        public IEnumerable<Shops> GetShops()
        {
            return _context.Shops;
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShops([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shops = await _context.Shops.SingleOrDefaultAsync(m => m.Id == id);

            if (shops == null)
            {
                return NotFound();
            }

            return Ok(shops);
        }

        // PUT: api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShops([FromRoute] Guid id, [FromBody] Shops shops)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shops.Id)
            {
                return BadRequest();
            }

            _context.Entry(shops).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopsExists(id))
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

        // POST: api/Shops
        [HttpPost]
        public async Task<IActionResult> PostShops([FromBody] Shops shops)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shops.Add(shops);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShops", new { id = shops.Id }, shops);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShops([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shops = await _context.Shops.SingleOrDefaultAsync(m => m.Id == id);
            if (shops == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shops);
            await _context.SaveChangesAsync();

            return Ok(shops);
        }

        [HttpGet("{id}/Products")]
        public IActionResult GetProductsForShop([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products =
                (from p in _context.Products
                 from sp in _context.ShopProducts
                 where p.Id.Equals(sp.Id_Product) && sp.Id_Shop.Equals(id)
                 select sp.Product);


            return Ok(products);
        }

        private bool ShopsExists(Guid id)
        {
            return _context.Shops.Any(e => e.Id == id);
        }
    }
}