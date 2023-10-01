using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomAPI.Models;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomItemsController : ControllerBase
    {
        private readonly CustomContext _context;

        public CustomItemsController(CustomContext context)
        {
            _context = context;
        }

        // GET: api/CustomItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomItem>>> GetCustomItems()
        {
            if (_context.CustomItems == null)
            {
                return NotFound();
            }
            return await _context.CustomItems.ToListAsync();
        }

        // GET: api/CustomItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomItem>> GetCustomItem(long id)
        {
            if (_context.CustomItems == null)
            {
                return NotFound();
            }
            var customItem = await _context.CustomItems.FindAsync(id);

            if (customItem == null)
            {
                return NotFound();
            }

            return customItem;
        }

        // PUT: api/CustomItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomItem(long id, CustomItem customItem)
        {
            if (id != customItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(customItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomItemExists(id))
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

        // POST: api/CustomItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomItem>> PostCustomItem(CustomItem customItem)
        {
            //   if (_context.CustomItems == null)
            //   {
            //       return Problem("Entity set 'CustomContext.CustomItems'  is null.");
            //   }
            _context.CustomItems.Add(customItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomItem), new { id = customItem.Id }, customItem);
        }

        // DELETE: api/CustomItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomItem(long id)
        {
            if (_context.CustomItems == null)
            {
                return NotFound();
            }
            var customItem = await _context.CustomItems.FindAsync(id);
            if (customItem == null)
            {
                return NotFound();
            }

            _context.CustomItems.Remove(customItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomItemExists(long id)
        {
            return (_context.CustomItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
