using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        // /Blog
        public IActionResult Index()
        {
            var posts = _context.BlogPosts
                .OrderByDescending(p => p.Id)
                .ToList();


            return View(posts);
        }

        // /Blog/Details/{id}
        public IActionResult Details(int id)
        {
            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
    }
}
