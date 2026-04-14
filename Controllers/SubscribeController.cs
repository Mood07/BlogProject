using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;

        public SubscribeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Subscribe
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Subscribe
        [HttpPost]
        public IActionResult Index(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var subscriber = new Subscriber
                {
                    Email = email,
                    SubscribedAt = DateTime.UtcNow
                };

                _context.Subscribers.Add(subscriber);
                _context.SaveChanges();

                ViewBag.Message = "Subscription successful. Thank you!";
            }
            else
            {
                ViewBag.Message = "Please enter a valid email address.";
            }

            return View();
        }
    }
}
