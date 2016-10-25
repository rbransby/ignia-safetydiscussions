using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafetyDiscussionApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace SafetyDiscussionApplication.Controllers
{        
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [Route("/{userId:int}")]
        public IActionResult Index(int userId)
        {
            return View("Index", userId);
        }
    }
}