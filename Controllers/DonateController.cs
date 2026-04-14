using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using BlogProject.Models;
using Stripe.Checkout;

namespace BlogProject.Controllers
{
    public class DonateController : Controller
    {
        private readonly AppDbContext _context;

        public DonateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Donate
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Donate/CreateCheckoutSession
        [HttpPost]
        public IActionResult CreateCheckoutSession(string email, int amount)
        {
            var successUrl = $"{Request.Scheme}://{Request.Host}/Donate/Success";
            var cancelUrl = $"{Request.Scheme}://{Request.Host}/Donate/Cancel";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = amount * 100, // USD to cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donation"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Locale = "en"
            };

            var service = new SessionService();
            var session = service.Create(options);

            // Save donation info (optional)
            if (!string.IsNullOrEmpty(email))
            {
                _context.Donations.Add(new Donation
                {
                    Email = email,
                    DonatedAt = DateTime.UtcNow
                });
                _context.SaveChanges();
            }

            Response.Headers.Append("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Success()
        {
            ViewBag.Message = "Thank you for your donation!";
            return View();
        }

        public IActionResult Cancel()
        {
            ViewBag.Message = "Donation was canceled.";
            return View();
        }
    }
}
