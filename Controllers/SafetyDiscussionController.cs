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
    public class SafetyDiscussionController : Controller
    {
        private ApplicationDbContext _context;
        public SafetyDiscussionController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("[controller]/{userId:int}", Name="ViewSafetyDiscussionByUser")]
        public IActionResult Index(int userId)
        {
            return View("ListByUser", userId);
        }

        [HttpGet("api/[controller]",Name="GetAll")]
        public async Task<IActionResult> Get()
        {
            var discussions = await _context.SafetyDiscussions
                                    .Include(sd => sd.Observer)
                                    .Include(sd => sd.Colleague)
                                    .ToListAsync();

            return Ok(discussions);
        }

        [HttpGet("api/[controller]/{id:int}", Name="GetSafetyDiscussionById")]
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

        [HttpGet("api/[controller]/[action]/{userId:int}", Name="GetSafetyDiscussionsByObserver")]
        public async Task<IActionResult> GetByObserver(int userId)
        {
            var safetyDiscussions = await _context.SafetyDiscussions
                                            .Include(sd => sd.Observer)
                                            .Include(sd => sd.Colleague)
                                            .Where(sd => sd.ObserverUserId == userId)
                                            .ToListAsync();
            
            return Ok(safetyDiscussions);
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> Post([FromBody] SafetyDiscussion safetyDiscussion)         
        {
            int maxId = _context.SafetyDiscussions.Select(sd => sd.Id).Max();
            safetyDiscussion.Id = ++maxId;
            _context.SafetyDiscussions.Add(safetyDiscussion);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetSafetyDiscussionById", new {id = safetyDiscussion.Id}, safetyDiscussion);
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
