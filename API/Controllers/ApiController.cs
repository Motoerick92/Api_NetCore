using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ApiContext _context;

        public ApiController(ApiContext context)
        {
            _context = context;

            if(_context.ApiItems.Count() == 0)
            {
                _context.ApiItems.Add(new ApiItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
        //ActionResult retorna contenido al usuario
        //GET: api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiItem>>> GetApiItems()
        {
            return await _context.ApiItems.ToListAsync();
        }
        //GET: solo un elemento
        [HttpGet("{id}")]
        public async Task <ActionResult<ApiItem>> GetApiItem(long id)
        {
            var apiItem = await _context.ApiItems.FindAsync(id);

            if (apiItem == null)
            {
                return NotFound();
            }

            return apiItem;
        }

        //POST: api/todo
        [HttpPost]
        public async Task<ActionResult<ApiItem>> PostApiItem(ApiItem item)
        {
            _context.ApiItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApiItem), new { id = item.Id }, item);
            
        }

        //PUT  api/api/5 --Actualiza el elemento
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiItem(long id, ApiItem item)
        {
            if (id!= item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiItem(long id)
        {
            var apiItem = await _context.ApiItems.FindAsync(id);
            if(apiItem == null)
            {
                return NotFound();
            }
            _context.ApiItems.Remove(apiItem);

            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
