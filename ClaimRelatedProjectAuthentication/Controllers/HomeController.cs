using ClaimRelatedProjectAuthentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClaimRelatedProjectAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Authenticate()
        {
            var GrandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Manisha"),
                new Claim(ClaimTypes.Email,"manisha@gmail.com"),
                new Claim("Grandma.Says","Very nice manisha."),
            };
            var LicenseClaims = new List<Claim>() { new Claim(ClaimTypes.Name,"Manisha Nagarkoti"),
                new Claim("DrivingLicense","A+")
            };
            var ManishaIndentityAsGrandma = new ClaimsIdentity(GrandmaClaims, "Grandma");
            var ManishaIdentityAsDriverLicense = new ClaimsIdentity(LicenseClaims, "Government");
            
            var userPrincipal = new ClaimsPrincipal(new[] {ManishaIndentityAsGrandma,ManishaIdentityAsDriverLicense });
            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
