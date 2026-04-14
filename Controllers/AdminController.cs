
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlogProject.Data;
using BlogProject.Models;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manage()
        {
            var posts = _context.BlogPosts.ToList();
            return View(posts);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Invalid username or password!";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPost post, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    ImageFile.CopyTo(stream);
                    post.ImagePath = "/images/" + fileName;
                }

                _context.BlogPosts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Manage");
            }

            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(BlogPost post, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                var existingPost = _context.BlogPosts.FirstOrDefault(p => p.Id == post.Id);
                if (existingPost == null) return NotFound();

                existingPost.Title = post.Title;
                existingPost.Content = post.Content;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    ImageFile.CopyTo(stream);
                    existingPost.ImagePath = "/images/" + fileName;
                }

                _context.SaveChanges();
                return RedirectToAction("Manage");
            }

            return View(post);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();

            _context.BlogPosts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public IActionResult Subscribers()
        {
            var subscribers = _context.Subscribers.OrderByDescending(s => s.SubscribedAt).ToList();
            return View(subscribers);
        }

        [HttpGet]
        public IActionResult Donations()
        {
            var donations = _context.Donations.OrderByDescending(d => d.DonatedAt).ToList();
            return View(donations);
        }
    }
}
