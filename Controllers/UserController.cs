using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafetyDiscussionApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace SafetyDiscussionApplication.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Get()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }
    }       
}
