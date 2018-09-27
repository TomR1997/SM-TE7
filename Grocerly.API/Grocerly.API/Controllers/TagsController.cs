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
    [Route("api/Tags")]
    public class TagsController : Controller
    {
        private readonly GrocerlyContext _context;

        public TagsController(GrocerlyContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public IEnumerable<Tags> GetTags()
        {
            return _context.Tags;
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTags([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tags = await _context.Tags.SingleOrDefaultAsync(m => m.Id == id);

            if (tags == null)
            {
                return NotFound();
            }

            return Ok(tags);
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTags([FromRoute] Guid id, [FromBody] Tags tags)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tags.Id)
            {
                return BadRequest();
            }

            _context.Entry(tags).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagsExists(id))
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

        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> PostTags([FromBody] Tags tags)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tags.Add(tags);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTags", new { id = tags.Id }, tags);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTags([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tags = await _context.Tags.SingleOrDefaultAsync(m => m.Id == id);
            if (tags == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tags);
            await _context.SaveChangesAsync();

            return Ok(tags);
        }

        private bool TagsExists(Guid id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}