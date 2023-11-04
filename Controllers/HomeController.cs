using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DebuggingChallenge.Models;

namespace DebuggingChallenge.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        [Route("/user/create")]
        public IActionResult CreateUser(User newUser)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Username", newUser.Name);
                if (!string.IsNullOrEmpty(newUser.Location))
                {
                    HttpContext.Session.SetString("Location", newUser.Location);
                }
                else
                {
                    HttpContext.Session.SetString("Location", "Undisclosed");
                }
                return RedirectToAction("Privacy");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("/generator")]
        public IActionResult Generator()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Index");
            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Passcode")))
            {
                GeneratePasscode();
            }
            return View("Privacy");
        }

        [HttpPost("/reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/generate/new")]
        public IActionResult GenerateNew()
        {   
            Console.WriteLine("entrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr generate/new");
            GeneratePasscode();
            return RedirectToAction("Privacy");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void GeneratePasscode()

        {
            Console.WriteLine("entrrrrrrrrrrrre al void");
            string passcode = "";
            string CharOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string NumOptions = "0123456789";
            Random rand = new Random();
            for (int i = 0; i < 15; i++)
            {
                int odds = rand.Next(2);
                if (odds == 0)
                {
                    passcode += CharOptions[rand.Next(CharOptions.Length)];
                }
                else
                {
                    passcode += NumOptions[rand.Next(NumOptions.Length)];
                }
            }
            HttpContext.Session.SetString("Passcode", passcode);
        }
    
}
