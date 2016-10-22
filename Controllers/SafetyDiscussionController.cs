using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafetyDiscussionApplication.Data;
using Microsoft.EntityFrameworkCore;
using SafetyDiscussionApplication.Models;

namespace SafetyDiscussionApplication.Controllers
{
    [Route("api/[controller]")]
    public class SafetyDiscussionController : Controller
    {
        private ApplicationDbContext _context;
        public SafetyDiscussionController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet(Name="GetAll")]
        public async Task<IActionResult> Get()
        {
            var discussions = await _context.SafetyDiscussions
                                    .Include(sd => sd.Observer)
                                    .Include(sd => sd.Colleague)
                                    .ToListAsync();

            return Ok(discussions);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var discussion = await _context.SafetyDiscussions
                                    .Include(sd => sd.Observer)
                                    .Include(sd => sd.Colleague)
                                    .SingleOrDefaultAsync(sd => sd.Id == id);
            if (discussion == null)
                return NotFound();
            
            return Ok(discussion);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SafetyDiscussion safetyDiscussion)         
        {
            int maxId = _context.SafetyDiscussions.Select(sd => sd.Id).Max();
            safetyDiscussion.Id = ++maxId;
            _context.SafetyDiscussions.Add(safetyDiscussion);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetAll", new {id = safetyDiscussion.Id}, safetyDiscussion);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var itemToDelete = await _context.SafetyDiscussions.SingleOrDefaultAsync(sd => sd.Id == id);
            if (itemToDelete != null)
            {
                _context.SafetyDiscussions.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }       
}
