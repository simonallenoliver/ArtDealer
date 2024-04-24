using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArtDealer.Models;
using Microsoft.AspNetCore.Identity;


namespace ArtDealer.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    // register user route
    [HttpPost("users/register")]
    public IActionResult RegisterUser(User newUser)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        PasswordHasher<User> hasher = new();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();

        // this saves our newly created user's id to session so they can stay logged in
        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        return RedirectToAction("Posts","Post");
    }

    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser logAttempt)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        // first we need to look up the user in the database to see if they exist
        User? dbUser = _context.Users.FirstOrDefault(u => u.Email == logAttempt.LogEmail);
        if (dbUser == null)
        {
            ModelState.AddModelError("LogPassword","Invalid Credentials");
            return View("Index");
        }
        PasswordHasher<LogUser> hasher = new();
        PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(logAttempt, dbUser.Password, logAttempt.LogPassword);
        // the password verification tool we're using returns a 0 if the password is incorrect and a 1 if it's correct
        if (pwCompareResult == 0)
        {
            ModelState.AddModelError("LogPassword", "Invalid Credentials");
            return View("Index");
        }
        // succesful login sets user ID in session and redirects to success page
        HttpContext.Session.SetInt32("UserId", dbUser.UserId);
        return RedirectToAction("Posts","Post");
    }

    // this page used to display upon registration or login
    // we use session check here to prevent people who are not logged in from accessing the success page - the login for
    // the session check is in our controllers folder
    [SessionCheck]
    [HttpGet("users/success")]
    public IActionResult Success()
    {
        return View("Success");
    }

    // logout route
    [HttpPost("users/logout")]
    public RedirectToActionResult Logout()
    {
        // nuclear route HttpContext.Session.Clear
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
